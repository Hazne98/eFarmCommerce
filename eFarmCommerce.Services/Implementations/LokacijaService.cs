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

public class LokacijaService
    : BaseCRUDService<LokacijaResponse, LokacijaSearchObject, Lokacija, LokacijaInsertRequest, LokacijaUpdateRequest>,
      ILokacijaService
{
    public LokacijaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Lokacija> AddFilter(IQueryable<Lokacija> query, LokacijaSearchObject search)
    {
        query = query.Include(x => x.Grad);

        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        if (!string.IsNullOrWhiteSpace(search.Adresa))
            query = query.Where(x => x.Adresa.Contains(search.Adresa));

        if (search.GradId.HasValue)
            query = query.Where(x => x.GradId == search.GradId.Value);

        if (search.Aktivan.HasValue)
            query = query.Where(x => x.Aktivan == search.Aktivan.Value);

        return query;
    }

    public override async Task<LokacijaResponse> InsertAsync(LokacijaInsertRequest request)
    {
        var gradExists = await Context.Grads.AnyAsync(x => x.GradId == request.GradId);

        if (!gradExists)
            throw new ClientException("Odabrani grad ne postoji.");

        return await base.InsertAsync(request);
    }

    public override async Task<LokacijaResponse?> UpdateAsync(int id, LokacijaUpdateRequest request)
    {
        var gradExists = await Context.Grads.AnyAsync(x => x.GradId == request.GradId);

        if (!gradExists)
            throw new ClientException("Odabrani grad ne postoji.");

        return await base.UpdateAsync(id, request);
    }
}