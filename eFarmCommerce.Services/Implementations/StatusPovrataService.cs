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

public class StatusPovrataService
    : BaseCRUDService<
        StatusPovrataResponse,
        StatusPovrataSearchObject,
        StatusPovratum,
        StatusPovrataInsertRequest,
        StatusPovrataUpdateRequest>,
      IStatusPovrataService
{
    public StatusPovrataService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<StatusPovratum> AddFilter(IQueryable<StatusPovratum> query, StatusPovrataSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<StatusPovrataResponse> InsertAsync(StatusPovrataInsertRequest request)
    {
        var exists = await Context.Set<StatusPovratum>()
            .AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Zapis sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<StatusPovrataResponse?> UpdateAsync(int id, StatusPovrataUpdateRequest request)
    {
        var exists = await Context.Set<StatusPovratum>()
            .AnyAsync(x => EF.Property<int>(x, "StatusPovrataId") != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Zapis sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}
