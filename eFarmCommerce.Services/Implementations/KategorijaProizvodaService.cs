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

public class KategorijaProizvodaService
    : BaseCRUDService<KategorijaProizvodaResponse, KategorijaProizvodaSearchObject, KategorijaProizvodum, KategorijaProizvodaInsertRequest, KategorijaProizvodaUpdateRequest>,
      IKategorijaProizvodaService
{
    public KategorijaProizvodaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<KategorijaProizvodum> AddFilter(IQueryable<KategorijaProizvodum> query, KategorijaProizvodaSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        return query;
    }

    public override async Task<KategorijaProizvodaResponse> InsertAsync(KategorijaProizvodaInsertRequest request)
    {
        var exists = await Context.KategorijaProizvoda
            .AnyAsync(x => x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Kategorija proizvoda sa istim nazivom već postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<KategorijaProizvodaResponse?> UpdateAsync(int id, KategorijaProizvodaUpdateRequest request)
    {
        var exists = await Context.KategorijaProizvoda
            .AnyAsync(x => x.KategorijaProizvodaId != id && x.Naziv == request.Naziv);

        if (exists)
            throw new ClientException("Kategorija proizvoda sa istim nazivom već postoji.");

        return await base.UpdateAsync(id, request);
    }
}