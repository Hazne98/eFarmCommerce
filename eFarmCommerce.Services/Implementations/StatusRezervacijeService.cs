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

public class StatusRezervacijeService
    : BaseCRUDService<
        StatusRezervacijeResponse,
        StatusRezervacijeSearchObject,
        StatusRezervacije,
        StatusRezervacijeInsertRequest,
        StatusRezervacijeUpdateRequest>,
      IStatusRezervacijeService
{
    public StatusRezervacijeService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<StatusRezervacije> AddFilter(IQueryable<StatusRezervacije> query, StatusRezervacijeSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<StatusRezervacijeResponse> InsertAsync(StatusRezervacijeInsertRequest request)
    {
        var exists = await Context.Set<StatusRezervacije>()
            .AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Zapis sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<StatusRezervacijeResponse?> UpdateAsync(int id, StatusRezervacijeUpdateRequest request)
    {
        var exists = await Context.Set<StatusRezervacije>()
            .AnyAsync(x => EF.Property<int>(x, "StatusRezervacijeId") != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Zapis sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}
