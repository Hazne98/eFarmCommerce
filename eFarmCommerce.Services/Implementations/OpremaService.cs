using eFarmCommerce.Model.Exceptions;
using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Database.Entities;
using eFarmCommerce.Services.Interfaces;
using FluentValidation;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace eFarmCommerce.Services.Implementations;

public class OpremaService
    : BaseCRUDService<OpremaResponse, OpremaSearchObject, Oprema, OpremaInsertRequest, OpremaUpdateRequest>,
      IOpremaService
{
    public OpremaService(
     eFarmCommerceDbContext context,
     IMapper mapper,
     IValidator<OpremaInsertRequest> insertValidator,
     IValidator<OpremaUpdateRequest> updateValidator)
     : base(context, mapper, insertValidator, updateValidator)
    {
    }

    protected override IQueryable<Oprema> AddFilter(IQueryable<Oprema> query, OpremaSearchObject search)
    {
        query = query.Include(x => x.KategorijaOpreme)
                     .Include(x => x.Proizvodjac);

        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        if (search.KategorijaOpremeId.HasValue)
            query = query.Where(x => x.KategorijaOpremeId == search.KategorijaOpremeId.Value);

        if (search.ProizvodjacId.HasValue)
            query = query.Where(x => x.ProizvodjacId == search.ProizvodjacId.Value);

        if (search.CijenaOd.HasValue)
            query = query.Where(x => x.CijenaPoDanu >= search.CijenaOd.Value);

        if (search.CijenaDo.HasValue)
            query = query.Where(x => x.CijenaPoDanu <= search.CijenaDo.Value);

        if (search.Aktivan.HasValue)
            query = query.Where(x => x.Aktivan == search.Aktivan.Value);

        return query;
    }

    public override async Task<OpremaResponse> InsertAsync(OpremaInsertRequest request)
    {
        var kategorijaExists = await Context.KategorijaOpremes
            .AnyAsync(x => x.KategorijaOpremeId == request.KategorijaOpremeId);

        if (!kategorijaExists)
            throw new ClientException("Odabrana kategorija opreme ne postoji.");

        var proizvodjacExists = await Context.Proizvodjacs
            .AnyAsync(x => x.ProizvodjacId == request.ProizvodjacId);

        if (!proizvodjacExists)
            throw new ClientException("Odabrani proizvođač ne postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<OpremaResponse?> UpdateAsync(int id, OpremaUpdateRequest request)
    {
        var kategorijaExists = await Context.KategorijaOpremes
            .AnyAsync(x => x.KategorijaOpremeId == request.KategorijaOpremeId);

        if (!kategorijaExists)
            throw new ClientException("Odabrana kategorija opreme ne postoji.");

        var proizvodjacExists = await Context.Proizvodjacs
            .AnyAsync(x => x.ProizvodjacId == request.ProizvodjacId);

        if (!proizvodjacExists)
            throw new ClientException("Odabrani proizvođač ne postoji.");

        return await base.UpdateAsync(id, request);
    }
}