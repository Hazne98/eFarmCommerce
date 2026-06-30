using eFarmCommerce.Model.Exceptions;
using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Database.Entities;
using eFarmCommerce.Services.Interfaces;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace eFarmCommerce.Services.Implementations;

public class FavoritService
    : BaseCRUDService<FavoritResponse, FavoritSearchObject, Favorit, FavoritInsertRequest, FavoritUpdateRequest>,
      IFavoritService
{
    public FavoritService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Favorit> AddFilter(IQueryable<Favorit> query, FavoritSearchObject search)
    {
        query = query
            .Include(x => x.Korisnik)
            .Include(x => x.Proizvod)
            .Include(x => x.Oprema);

        if (search.KorisnikId.HasValue)
            query = query.Where(x => x.KorisnikId == search.KorisnikId.Value);

        if (search.ProizvodId.HasValue)
            query = query.Where(x => x.ProizvodId == search.ProizvodId.Value);

        if (search.OpremaId.HasValue)
            query = query.Where(x => x.OpremaId == search.OpremaId.Value);

        return query;
    }

    public override async Task<FavoritResponse> InsertAsync(FavoritInsertRequest request)
    {
        if (request.ProizvodId == null && request.OpremaId == null)
            throw new ClientException("Favorit mora biti vezan za proizvod ili opremu.");

        if (request.ProizvodId != null && request.OpremaId != null)
            throw new ClientException("Favorit ne može biti vezan i za proizvod i za opremu.");

        var korisnikExists = await Context.Korisniks.AnyAsync(x => x.KorisnikId == request.KorisnikId);
        if (!korisnikExists)
            throw new ClientException("Korisnik ne postoji.");

        if (request.ProizvodId.HasValue)
        {
            var proizvodExists = await Context.Proizvods.AnyAsync(x => x.ProizvodId == request.ProizvodId.Value && x.Aktivan);
            if (!proizvodExists)
                throw new ClientException("Proizvod ne postoji ili nije aktivan.");

            var duplicate = await Context.Favorits.AnyAsync(x =>
                x.KorisnikId == request.KorisnikId &&
                x.ProizvodId == request.ProizvodId.Value);

            if (duplicate)
                throw new ClientException("Proizvod je već dodan u favorite.");
        }

        if (request.OpremaId.HasValue)
        {
            var opremaExists = await Context.Opremas.AnyAsync(x => x.OpremaId == request.OpremaId.Value && x.Aktivan);
            if (!opremaExists)
                throw new ClientException("Oprema ne postoji ili nije aktivna.");

            var duplicate = await Context.Favorits.AnyAsync(x =>
                x.KorisnikId == request.KorisnikId &&
                x.OpremaId == request.OpremaId.Value);

            if (duplicate)
                throw new ClientException("Oprema je već dodana u favorite.");
        }

        var entity = Mapper.Map<Favorit>(request);
        entity.DatumDodavanja = DateTime.UtcNow;

        Context.Favorits.Add(entity);
        await Context.SaveChangesAsync();

        var created = await Context.Favorits
            .Include(x => x.Korisnik)
            .Include(x => x.Proizvod)
            .Include(x => x.Oprema)
            .FirstAsync(x => x.FavoritId == entity.FavoritId);

        return Mapper.Map<FavoritResponse>(created);
    }
}