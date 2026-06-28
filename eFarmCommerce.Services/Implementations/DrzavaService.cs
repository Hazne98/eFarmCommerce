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

public class DrzavaService
    : BaseCRUDService<DrzavaResponse, DrzavaSearchObject, Drzava, DrzavaInsertRequest, DrzavaUpdateRequest>,
      IDrzavaService
{
    public DrzavaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Drzava> AddFilter(IQueryable<Drzava> query, DrzavaSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<DrzavaResponse> InsertAsync(DrzavaInsertRequest request)
    {
        var exists = await Context.Drzavas.AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Država sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<DrzavaResponse?> UpdateAsync(int id, DrzavaUpdateRequest request)
    {
        var exists = await Context.Drzavas
            .AnyAsync(x => x.DrzavaId != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Država sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}