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

public class NacinPlacanjaService
    : BaseCRUDService<
        NacinPlacanjaResponse,
        NacinPlacanjaSearchObject,
        NacinPlacanja,
        NacinPlacanjaInsertRequest,
        NacinPlacanjaUpdateRequest>,
      INacinPlacanjaService
{
    public NacinPlacanjaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<NacinPlacanja> AddFilter(IQueryable<NacinPlacanja> query, NacinPlacanjaSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<NacinPlacanjaResponse> InsertAsync(NacinPlacanjaInsertRequest request)
    {
        var exists = await Context.Set<NacinPlacanja>()
            .AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Zapis sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<NacinPlacanjaResponse?> UpdateAsync(int id, NacinPlacanjaUpdateRequest request)
    {
        var exists = await Context.Set<NacinPlacanja>()
            .AnyAsync(x => EF.Property<int>(x, "NacinPlacanjaId") != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Zapis sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}
