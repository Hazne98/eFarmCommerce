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

public class UlogaService
    : BaseCRUDService<UlogaResponse, UlogaSearchObject, Uloga, UlogaInsertRequest, UlogaUpdateRequest>,
      IUlogaService
{
    public UlogaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Uloga> AddFilter(IQueryable<Uloga> query, UlogaSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<UlogaResponse> InsertAsync(UlogaInsertRequest request)
    {
        var exists = await Context.Ulogas.AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Uloga sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<UlogaResponse?> UpdateAsync(int id, UlogaUpdateRequest request)
    {
        var exists = await Context.Ulogas
            .AnyAsync(x => x.UlogaId != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Uloga sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}