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

public class NarudzbaService
    : BaseCRUDService<NarudzbaResponse, NarudzbaSearchObject, Narudzba, NarudzbaInsertRequest, NarudzbaUpdateRequest>,
      INarudzbaService
{
    public NarudzbaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Narudzba> AddFilter(IQueryable<Narudzba> query, NarudzbaSearchObject search)
    {
        query = query
            .Include(x => x.Korisnik)
            .Include(x => x.StatusNarudzbe);

        if (search.KorisnikId.HasValue)
            query = query.Where(x => x.KorisnikId == search.KorisnikId.Value);

        if (search.StatusNarudzbeId.HasValue)
            query = query.Where(x => x.StatusNarudzbeId == search.StatusNarudzbeId.Value);

        if (search.DatumOd.HasValue)
            query = query.Where(x => x.DatumNarudzbe >= search.DatumOd.Value);

        if (search.DatumDo.HasValue)
            query = query.Where(x => x.DatumNarudzbe <= search.DatumDo.Value);

        if (search.CijenaOd.HasValue)
            query = query.Where(x => x.UkupnaCijena >= search.CijenaOd.Value);

        if (search.CijenaDo.HasValue)
            query = query.Where(x => x.UkupnaCijena <= search.CijenaDo.Value);

        return query;
    }

    public override async Task<NarudzbaResponse> InsertAsync(NarudzbaInsertRequest request)
    {
        var korisnikExists = await Context.Korisniks.AnyAsync(x => x.KorisnikId == request.KorisnikId);

        if (!korisnikExists)
            throw new ClientException("Korisnik ne postoji.");

        var statusExists = await Context.StatusNarudzbes.AnyAsync(x => x.StatusNarudzbeId == request.StatusNarudzbeId);

        if (!statusExists)
            throw new ClientException("Status narudžbe ne postoji.");

        var entity = Mapper.Map<Narudzba>(request);

        entity.DatumNarudzbe = DateTime.UtcNow;
        entity.UkupnaCijena = 0;

        Context.Narudzbas.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<NarudzbaResponse>(entity);
    }

    public override async Task<NarudzbaResponse?> UpdateAsync(int id, NarudzbaUpdateRequest request)
    {
        var entity = await Context.Narudzbas.FindAsync(id);

        if (entity == null)
            return default;

        var statusExists = await Context.StatusNarudzbes.AnyAsync(x => x.StatusNarudzbeId == request.StatusNarudzbeId);

        if (!statusExists)
            throw new ClientException("Status narudžbe ne postoji.");

        entity.StatusNarudzbeId = request.StatusNarudzbeId;
        entity.AdresaDostave = request.AdresaDostave;
        entity.OdobrioKorisnikId = request.OdobrioKorisnikId;
        entity.RazlogOtkazivanja = request.RazlogOtkazivanja;
        entity.DatumPromjeneStatusa = DateTime.UtcNow;

        await Context.SaveChangesAsync();

        return Mapper.Map<NarudzbaResponse>(entity);
    }
}