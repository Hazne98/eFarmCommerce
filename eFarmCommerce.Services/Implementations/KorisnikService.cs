using eFarmCommerce.Common.Services.Crypto;
using eFarmCommerce.Model.Exceptions;
using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Database.Entities;
using eFarmCommerce.Services.Interfaces;
using FluentValidation;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace eFarmCommerce.Services.Implementations;

public class KorisnikService
    : BaseCRUDService<
        KorisnikResponse,
        KorisnikSearchObject,
        Korisnik,
        KorisnikInsertRequest,
        KorisnikUpdateRequest>,
      IKorisnikService
{
    private readonly ICryptoService _cryptoService;
    private readonly IValidator<KorisnikInsertRequest> _insertValidator;
    private readonly IValidator<KorisnikUpdateRequest> _updateValidator;

    public KorisnikService(
    eFarmCommerceDbContext context,
    IMapper mapper,
    ICryptoService cryptoService,
    IValidator<KorisnikInsertRequest> insertValidator,
    IValidator<KorisnikUpdateRequest> updateValidator)
    : base(context, mapper, insertValidator, updateValidator)
    {
        _cryptoService = cryptoService;
        _insertValidator = insertValidator;
        _updateValidator = updateValidator;
    }

    protected override IQueryable<Korisnik> AddFilter(IQueryable<Korisnik> query, KorisnikSearchObject search)
    {
        query = query
            .Include(x => x.Grad)
            .Include(x => x.Uloga);

        if (!string.IsNullOrWhiteSpace(search.Ime))
            query = query.Where(x => x.Ime.Contains(search.Ime));

        if (!string.IsNullOrWhiteSpace(search.Prezime))
            query = query.Where(x => x.Prezime.Contains(search.Prezime));

        if (!string.IsNullOrWhiteSpace(search.KorisnickoIme))
            query = query.Where(x => x.KorisnickoIme.Contains(search.KorisnickoIme));

        if (!string.IsNullOrWhiteSpace(search.Email))
            query = query.Where(x => x.Email.Contains(search.Email));

        if (search.GradId.HasValue)
            query = query.Where(x => x.GradId == search.GradId.Value);

        if (search.UlogaId.HasValue)
            query = query.Where(x => x.UlogaId == search.UlogaId.Value);

        if (search.Aktivan.HasValue)
            query = query.Where(x => x.Aktivan == search.Aktivan.Value);

        return query;
    }

    public override async Task<KorisnikResponse> InsertAsync(KorisnikInsertRequest request)
    {
        await ValidateInsertAsync(request);

        var entity = Mapper.Map<Korisnik>(request);

        var salt = _cryptoService.GenerateSalt();
        var hash = _cryptoService.GenerateHash(request.Lozinka, salt);

        entity.LozinkaSalt = salt;
        entity.LozinkaHash = hash;
        entity.DatumRegistracije = DateTime.UtcNow;

        Context.Korisniks.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<KorisnikResponse>(entity);
    }

    public override async Task<KorisnikResponse?> UpdateAsync(int id, KorisnikUpdateRequest request)
    {
        await ValidateUpdateAsync(id, request);

        var entity = await Context.Korisniks.FindAsync(id);

        if (entity == null)
            return default;

        Mapper.Map(request, entity);

        await Context.SaveChangesAsync();

        return Mapper.Map<KorisnikResponse>(entity);
    }

    public async Task ChangePasswordAsync(int korisnikId, KorisnikPasswordChangeRequest request)
    {
        var entity = await Context.Korisniks.FindAsync(korisnikId);

        if (entity == null)
            throw new ClientException("Korisnik nije pronađen.", 404);

        var validPassword = _cryptoService.VerifyPassword(
            request.StaraLozinka,
            entity.LozinkaSalt ?? string.Empty,
            entity.LozinkaHash);

        if (!validPassword)
            throw new ClientException("Stara lozinka nije ispravna.");

        var newSalt = _cryptoService.GenerateSalt();
        var newHash = _cryptoService.GenerateHash(request.NovaLozinka, newSalt);

        entity.LozinkaSalt = newSalt;
        entity.LozinkaHash = newHash;

        await Context.SaveChangesAsync();
    }

    private async Task ValidateInsertAsync(KorisnikInsertRequest request)
    {
        var usernameExists = await Context.Korisniks
            .AnyAsync(x => x.KorisnickoIme == request.KorisnickoIme);

        if (usernameExists)
            throw new ClientException("Korisničko ime je već zauzeto.");

        var emailExists = await Context.Korisniks
            .AnyAsync(x => x.Email == request.Email);

        if (emailExists)
            throw new ClientException("Email je već zauzet.");

        await ValidateForeignKeysAsync(request.GradId, request.UlogaId);
    }

    private async Task ValidateUpdateAsync(int id, KorisnikUpdateRequest request)
    {
        var usernameExists = await Context.Korisniks
            .AnyAsync(x => x.KorisnikId != id && x.KorisnickoIme == request.KorisnickoIme);

        if (usernameExists)
            throw new ClientException("Korisničko ime je već zauzeto.");

        var emailExists = await Context.Korisniks
            .AnyAsync(x => x.KorisnikId != id && x.Email == request.Email);

        if (emailExists)
            throw new ClientException("Email je već zauzet.");

        await ValidateForeignKeysAsync(request.GradId, request.UlogaId);
    }

    private async Task ValidateForeignKeysAsync(int gradId, int ulogaId)
    {
        var gradExists = await Context.Grads.AnyAsync(x => x.GradId == gradId);

        if (!gradExists)
            throw new ClientException("Odabrani grad ne postoji.");

        var ulogaExists = await Context.Ulogas.AnyAsync(x => x.UlogaId == ulogaId);

        if (!ulogaExists)
            throw new ClientException("Odabrana uloga ne postoji.");
    }
}