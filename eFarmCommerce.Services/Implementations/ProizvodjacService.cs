using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Model.SearchObjects;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Database.Entities;
using eFarmCommerce.Services.Interfaces;
using FluentValidation;
using MapsterMapper;

namespace eFarmCommerce.Services.Implementations;

public class ProizvodjacService
    : BaseCRUDService<ProizvodjacResponse, ProizvodjacSearchObject, Proizvodjac, ProizvodjacInsertRequest, ProizvodjacUpdateRequest>,
      IProizvodjacService
{
    public ProizvodjacService(
    eFarmCommerceDbContext context,
    IMapper mapper,
    IValidator<ProizvodjacInsertRequest> insertValidator,
    IValidator<ProizvodjacUpdateRequest> updateValidator)
    : base(context, mapper, insertValidator, updateValidator)
    {
    }

    protected override IQueryable<Proizvodjac> AddFilter(IQueryable<Proizvodjac> query, ProizvodjacSearchObject search)
    {
        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        if (search.GradId.HasValue)
            query = query.Where(x => x.GradId == search.GradId.Value);

        if (search.Aktivan.HasValue)
            query = query.Where(x => x.Aktivan == search.Aktivan.Value);

        return query;
    }
}