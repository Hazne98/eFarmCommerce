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

public class PovratRefundService
    : BaseCRUDService<PovratRefundResponse, PovratRefundSearchObject, PovratRefund, PovratRefundInsertRequest, PovratRefundUpdateRequest>,
      IPovratRefundService
{
    public PovratRefundService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<PovratRefund> AddFilter(IQueryable<PovratRefund> query, PovratRefundSearchObject search)
    {
        query = query
            .Include(x => x.Placanje)
            .Include(x => x.StatusPovrata);

        if (search.PlacanjeId.HasValue)
            query = query.Where(x => x.PlacanjeId == search.PlacanjeId.Value);

        if (search.StatusPovrataId.HasValue)
            query = query.Where(x => x.StatusPovrataId == search.StatusPovrataId.Value);

        if (search.DatumZahtjevaOd.HasValue)
            query = query.Where(x => x.DatumZahtjeva >= search.DatumZahtjevaOd.Value);

        if (search.DatumZahtjevaDo.HasValue)
            query = query.Where(x => x.DatumZahtjeva <= search.DatumZahtjevaDo.Value);

        if (search.IznosOd.HasValue)
            query = query.Where(x => x.Iznos >= search.IznosOd.Value);

        if (search.IznosDo.HasValue)
            query = query.Where(x => x.Iznos <= search.IznosDo.Value);

        return query;
    }

    public override async Task<PovratRefundResponse> InsertAsync(PovratRefundInsertRequest request)
    {
        var placanje = await Context.Placanjes.FindAsync(request.PlacanjeId);
        if (placanje == null)
            throw new ClientException("Plaćanje ne postoji.");

        var statusExists = await Context.StatusPovrata.AnyAsync(x => x.StatusPovrataId == request.StatusPovrataId);
        if (!statusExists)
            throw new ClientException("Status povrata ne postoji.");

        if (request.Iznos <= 0)
            throw new ClientException("Iznos povrata mora biti veći od 0.");

        if (request.Iznos > placanje.Iznos)
            throw new ClientException("Iznos povrata ne može biti veći od iznosa plaćanja.");

        var entity = Mapper.Map<PovratRefund>(request);
        entity.DatumZahtjeva = DateTime.UtcNow;

        Context.PovratRefunds.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<PovratRefundResponse>(entity);
    }

    public override async Task<PovratRefundResponse?> UpdateAsync(int id, PovratRefundUpdateRequest request)
    {
        var entity = await Context.PovratRefunds.FindAsync(id);

        if (entity == null)
            return default;

        var statusExists = await Context.StatusPovrata.AnyAsync(x => x.StatusPovrataId == request.StatusPovrataId);
        if (!statusExists)
            throw new ClientException("Status povrata ne postoji.");

        if (request.Iznos <= 0)
            throw new ClientException("Iznos povrata mora biti veći od 0.");

        entity.StatusPovrataId = request.StatusPovrataId;
        entity.Iznos = request.Iznos;
        entity.Razlog = request.Razlog;
        entity.RefundTransakcijaId = request.RefundTransakcijaId;
        entity.DatumObrade = request.DatumObrade ?? DateTime.UtcNow;

        await Context.SaveChangesAsync();

        return Mapper.Map<PovratRefundResponse>(entity);
    }
}