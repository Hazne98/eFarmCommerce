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

public class KorpaService
    : BaseCRUDService<KorpaResponse, KorpaSearchObject, Korpa, KorpaInsertRequest, KorpaUpdateRequest>,
      IKorpaService
{
    public KorpaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Korpa> AddFilter(IQueryable<Korpa> query, KorpaSearchObject search)
    {
        query = query.Include(x => x.Korisnik);

        if (search.KorisnikId.HasValue)
            query = query.Where(x => x.KorisnikId == search.KorisnikId.Value);

        return query;
    }

    public override async Task<KorpaResponse> InsertAsync(KorpaInsertRequest request)
    {
        var korisnikExists = await Context.Korisniks
            .AnyAsync(x => x.KorisnikId == request.KorisnikId);

        if (!korisnikExists)
            throw new ClientException("Korisnik ne postoji.");

        var korpaExists = await Context.Korpas
            .AnyAsync(x => x.KorisnikId == request.KorisnikId);

        if (korpaExists)
            throw new ClientException("Korisnik već ima kreiranu korpu.");

        var entity = Mapper.Map<Korpa>(request);
        entity.DatumKreiranja = DateTime.UtcNow;

        Context.Korpas.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<KorpaResponse>(entity);
    }
}