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

public class GradService
    : BaseCRUDService<GradResponse, GradSearchObject, Grad, GradInsertRequest, GradUpdateRequest>,
      IGradService
{
    public GradService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Grad> AddFilter(IQueryable<Grad> query, GradSearchObject search)
    {
        query = query.Include(x => x.Drzava);

        if (!string.IsNullOrWhiteSpace(search.Naziv))
            query = query.Where(x => x.Naziv.Contains(search.Naziv));

        if (search.DrzavaId.HasValue)
            query = query.Where(x => x.DrzavaId == search.DrzavaId.Value);

        if (!string.IsNullOrWhiteSpace(search.DrzavaNaziv))
            query = query.Where(x => x.Drzava.Naziv.Contains(search.DrzavaNaziv));

        return query;
    }

    public override async Task<GradResponse> InsertAsync(GradInsertRequest request)
    {
        await ValidateAsync(request.Naziv, request.DrzavaId, null);

        return await base.InsertAsync(request);
    }

    public override async Task<GradResponse?> UpdateAsync(int id, GradUpdateRequest request)
    {
        await ValidateAsync(request.Naziv, request.DrzavaId, id);

        return await base.UpdateAsync(id, request);
    }

    private async Task ValidateAsync(string naziv, int drzavaId, int? currentId)
    {
        var drzavaExists = await Context.Drzavas
            .AnyAsync(x => x.DrzavaId == drzavaId);

        if (!drzavaExists)
            throw new ClientException("Odabrana država ne postoji.");

        var duplicateQuery = Context.Grads
            .Where(x => x.Naziv == naziv && x.DrzavaId == drzavaId);

        if (currentId.HasValue)
            duplicateQuery = duplicateQuery.Where(x => x.GradId != currentId.Value);

        var duplicateExists = await duplicateQuery.AnyAsync();

        if (duplicateExists)
            throw new ClientException("Grad sa istim nazivom već postoji u odabranoj državi.");
    }
}