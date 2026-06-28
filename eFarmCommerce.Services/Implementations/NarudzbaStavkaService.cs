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

public class NarudzbaStavkaService
    : BaseCRUDService<NarudzbaStavkaResponse, NarudzbaStavkaSearchObject, NarudzbaStavka, NarudzbaStavkaInsertRequest, NarudzbaStavkaUpdateRequest>,
      INarudzbaStavkaService
{
    public NarudzbaStavkaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<NarudzbaStavka> AddFilter(IQueryable<NarudzbaStavka> query, NarudzbaStavkaSearchObject search)
    {
        query = query.Include(x => x.Proizvod);

        if (search.NarudzbaId.HasValue)
            query = query.Where(x => x.NarudzbaId == search.NarudzbaId.Value);

        if (search.ProizvodId.HasValue)
            query = query.Where(x => x.ProizvodId == search.ProizvodId.Value);

        if (!string.IsNullOrWhiteSpace(search.ProizvodNaziv))
            query = query.Where(x => x.Proizvod.Naziv.Contains(search.ProizvodNaziv));

        return query;
    }

    public override async Task<NarudzbaStavkaResponse> InsertAsync(NarudzbaStavkaInsertRequest request)
    {
        var narudzbaExists = await Context.Narudzbas.AnyAsync(x => x.NarudzbaId == request.NarudzbaId);
        if (!narudzbaExists)
            throw new ClientException("Narudžba ne postoji.");

        var proizvod = await Context.Proizvods.FindAsync(request.ProizvodId);
        if (proizvod == null)
            throw new ClientException("Proizvod ne postoji.");

        if (proizvod.KolicinaNaStanju < request.Kolicina)
            throw new ClientException("Nema dovoljno proizvoda na stanju.");

        proizvod.KolicinaNaStanju -= request.Kolicina;

        var response = await base.InsertAsync(request);

        await RecalculateOrderTotalAsync(request.NarudzbaId);

        return response;
    }

    private async Task RecalculateOrderTotalAsync(int narudzbaId)
    {
        var total = await Context.NarudzbaStavkas
            .Where(x => x.NarudzbaId == narudzbaId)
            .SumAsync(x => x.Kolicina * x.Cijena);

        var narudzba = await Context.Narudzbas.FindAsync(narudzbaId);

        if (narudzba != null)
        {
            narudzba.UkupnaCijena = total;
            await Context.SaveChangesAsync();
        }
    }
}