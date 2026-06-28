using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Database.Entities;
using eFarmCommerce.Services.Interfaces;
using FluentValidation;
using MapsterMapper;

namespace eFarmCommerce.Services.Implementations;

public class ProizvodService
    : BaseCRUDService<ProizvodResponse, ProizvodSearchObject, Proizvod, ProizvodInsertRequest, ProizvodUpdateRequest>,
      IProizvodService
{
    public ProizvodService(
     eFarmCommerceDbContext context,
     IMapper mapper,
     IValidator<ProizvodInsertRequest> insertValidator,
     IValidator<ProizvodUpdateRequest> updateValidator)
     : base(context, mapper, insertValidator, updateValidator)
    {
    }

    protected override IQueryable<Proizvod> AddFilter(IQueryable<Proizvod> query, ProizvodSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        if (search.KategorijaProizvodaId.HasValue)
            query = query.Where(x => x.KategorijaProizvodaId == search.KategorijaProizvodaId.Value);

        if (search.ProizvodjacId.HasValue)
            query = query.Where(x => x.ProizvodjacId == search.ProizvodjacId.Value);

        if (search.Aktivan.HasValue)
            query = query.Where(x => x.Aktivan == search.Aktivan.Value);

        return query;
    }
}