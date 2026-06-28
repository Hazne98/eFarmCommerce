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

public class OpremaLokacijaService
    : BaseCRUDService<OpremaLokacijaResponse, OpremaLokacijaSearchObject, OpremaLokacija, OpremaLokacijaInsertRequest, OpremaLokacijaUpdateRequest>,
      IOpremaLokacijaService
{
    public OpremaLokacijaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<OpremaLokacija> AddFilter(IQueryable<OpremaLokacija> query, OpremaLokacijaSearchObject search)
    {
        query = query
            .Include(x => x.Oprema)
            .Include(x => x.Lokacija);

        if (search.OpremaId.HasValue)
            query = query.Where(x => x.OpremaId == search.OpremaId.Value);

        if (search.LokacijaId.HasValue)
            query = query.Where(x => x.LokacijaId == search.LokacijaId.Value);

        if (!string.IsNullOrWhiteSpace(search.OpremaNaziv))
            query = query.Where(x => x.Oprema.Naziv.Contains(search.OpremaNaziv));

        if (!string.IsNullOrWhiteSpace(search.LokacijaNaziv))
            query = query.Where(x => x.Lokacija.Naziv.Contains(search.LokacijaNaziv));

        if (search.SamoDostupno)
            query = query.Where(x => x.KolicinaDostupna > 0);

        return query;
    }

    public override async Task<OpremaLokacijaResponse> InsertAsync(OpremaLokacijaInsertRequest request)
    {
        await ValidateAsync(request.OpremaId, request.LokacijaId, null);

        return await base.InsertAsync(request);
    }

    public override async Task<OpremaLokacijaResponse?> UpdateAsync(int id, OpremaLokacijaUpdateRequest request)
    {
        await ValidateAsync(request.OpremaId, request.LokacijaId, id);

        return await base.UpdateAsync(id, request);
    }

    private async Task ValidateAsync(int opremaId, int lokacijaId, int? currentId)
    {
        var opremaExists = await Context.Opremas.AnyAsync(x => x.OpremaId == opremaId);

        if (!opremaExists)
            throw new ClientException("Odabrana oprema ne postoji.");

        var lokacijaExists = await Context.Lokacijas.AnyAsync(x => x.LokacijaId == lokacijaId);

        if (!lokacijaExists)
            throw new ClientException("Odabrana lokacija ne postoji.");

        var duplicateQuery = Context.OpremaLokacijas
            .Where(x => x.OpremaId == opremaId && x.LokacijaId == lokacijaId);

        if (currentId.HasValue)
            duplicateQuery = duplicateQuery.Where(x => x.OpremaLokacijaId != currentId.Value);

        var duplicateExists = await duplicateQuery.AnyAsync();

        if (duplicateExists)
            throw new ClientException("Ova oprema je već dodana na odabranu lokaciju.");
    }
}