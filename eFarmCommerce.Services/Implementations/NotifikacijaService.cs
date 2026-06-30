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

public class NotifikacijaService
    : BaseCRUDService<NotifikacijaResponse, NotifikacijaSearchObject, Notifikacija, NotifikacijaInsertRequest, NotifikacijaUpdateRequest>,
      INotifikacijaService
{
    public NotifikacijaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Notifikacija> AddFilter(IQueryable<Notifikacija> query, NotifikacijaSearchObject search)
    {
        if (search.KorisnikId.HasValue)
            query = query.Where(x => x.KorisnikId == search.KorisnikId.Value);

        if (search.Procitana.HasValue)
            query = query.Where(x => x.Procitana == search.Procitana.Value);

        if (search.DatumOd.HasValue)
            query = query.Where(x => x.DatumKreiranja >= search.DatumOd.Value);

        if (search.DatumDo.HasValue)
            query = query.Where(x => x.DatumKreiranja <= search.DatumDo.Value);

        return query;
    }

    public override async Task<NotifikacijaResponse> InsertAsync(NotifikacijaInsertRequest request)
    {
        var korisnikExists = await Context.Korisniks.AnyAsync(x => x.KorisnikId == request.KorisnikId);

        if (!korisnikExists)
            throw new ClientException("Korisnik ne postoji.");

        var entity = Mapper.Map<Notifikacija>(request);
        entity.Procitana = false;
        entity.DatumKreiranja = DateTime.UtcNow;

        Context.Notifikacijas.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<NotifikacijaResponse>(entity);
    }

    public override async Task<NotifikacijaResponse?> UpdateAsync(int id, NotifikacijaUpdateRequest request)
    {
        var entity = await Context.Notifikacijas.FindAsync(id);

        if (entity == null)
            return default;

        entity.Procitana = request.Procitana;

        await Context.SaveChangesAsync();

        return Mapper.Map<NotifikacijaResponse>(entity);
    }
}