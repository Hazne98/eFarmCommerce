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

public class KategorijaOpremeService
    : BaseCRUDService<KategorijaOpremeResponse, KategorijaOpremeSearchObject, KategorijaOpreme, KategorijaOpremeInsertRequest, KategorijaOpremeUpdateRequest>,
      IKategorijaOpremeService
{
    public KategorijaOpremeService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<KategorijaOpreme> AddFilter(IQueryable<KategorijaOpreme> query, KategorijaOpremeSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<KategorijaOpremeResponse> InsertAsync(KategorijaOpremeInsertRequest request)
    {
        var exists = await Context.KategorijaOpremes
            .AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Kategorija opreme sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<KategorijaOpremeResponse?> UpdateAsync(int id, KategorijaOpremeUpdateRequest request)
    {
        var exists = await Context.KategorijaOpremes
            .AnyAsync(x => x.KategorijaOpremeId != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Kategorija opreme sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}