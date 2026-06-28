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

public class StatusPlacanjaService
    : BaseCRUDService<
        StatusPlacanjaResponse,
        StatusPlacanjaSearchObject,
        StatusPlacanja,
        StatusPlacanjaInsertRequest,
        StatusPlacanjaUpdateRequest>,
      IStatusPlacanjaService
{
    public StatusPlacanjaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<StatusPlacanja> AddFilter(IQueryable<StatusPlacanja> query, StatusPlacanjaSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<StatusPlacanjaResponse> InsertAsync(StatusPlacanjaInsertRequest request)
    {
        var exists = await Context.Set<StatusPlacanja>()
            .AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Zapis sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<StatusPlacanjaResponse?> UpdateAsync(int id, StatusPlacanjaUpdateRequest request)
    {
        var exists = await Context.Set<StatusPlacanja>()
            .AnyAsync(x => EF.Property<int>(x, "StatusPlacanjaId") != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Zapis sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}
