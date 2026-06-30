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

public class HistorijaPretrageService
    : BaseCRUDService<HistorijaPretrageResponse, HistorijaPretrageSearchObject, HistorijaPretrage, HistorijaPretrageInsertRequest, HistorijaPretrageUpdateRequest>,
      IHistorijaPretrageService
{
    public HistorijaPretrageService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<HistorijaPretrage> AddFilter(IQueryable<HistorijaPretrage> query, HistorijaPretrageSearchObject search)
    {
        if (search.KorisnikId.HasValue)
            query = query.Where(x => x.KorisnikId == search.KorisnikId.Value);

        if (!string.IsNullOrWhiteSpace(search.Pojam))
            query = query.Where(x => x.Pojam.Contains(search.Pojam));

        if (!string.IsNullOrWhiteSpace(search.Tip))
            query = query.Where(x => x.Tip.Contains(search.Tip));

        if (search.DatumOd.HasValue)
            query = query.Where(x => x.DatumPretrage >= search.DatumOd.Value);

        if (search.DatumDo.HasValue)
            query = query.Where(x => x.DatumPretrage <= search.DatumDo.Value);

        return query;
    }

    public override async Task<HistorijaPretrageResponse> InsertAsync(HistorijaPretrageInsertRequest request)
    {
        var korisnikExists = await Context.Korisniks.AnyAsync(x => x.KorisnikId == request.KorisnikId);

        if (!korisnikExists)
            throw new ClientException("Korisnik ne postoji.");

        var entity = Mapper.Map<HistorijaPretrage>(request);
        entity.DatumPretrage = DateTime.UtcNow;

        Context.HistorijaPretrages.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<HistorijaPretrageResponse>(entity);
    }
}