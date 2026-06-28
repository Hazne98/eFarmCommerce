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

public class StatusNarudzbeService
    : BaseCRUDService<StatusNarudzbeResponse, StatusNarudzbeSearchObject, StatusNarudzbe, StatusNarudzbeInsertRequest, StatusNarudzbeUpdateRequest>,
      IStatusNarudzbeService
{
    public StatusNarudzbeService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<StatusNarudzbe> AddFilter(IQueryable<StatusNarudzbe> query, StatusNarudzbeSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<StatusNarudzbeResponse> InsertAsync(StatusNarudzbeInsertRequest request)
    {
        var exists = await Context.StatusNarudzbes.AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Status narudžbe sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<StatusNarudzbeResponse?> UpdateAsync(int id, StatusNarudzbeUpdateRequest request)
    {
        var exists = await Context.StatusNarudzbes
            .AnyAsync(x => x.StatusNarudzbeId != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Status narudžbe sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}