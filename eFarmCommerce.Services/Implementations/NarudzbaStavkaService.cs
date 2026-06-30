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
        query = query
            .Include(x => x.Narudzba)
            .Include(x => x.Proizvod);

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

        var proizvod = await Context.Proizvods
            .FirstOrDefaultAsync(x => x.ProizvodId == request.ProizvodId && x.Aktivan);

        if (proizvod == null)
            throw new ClientException("Proizvod ne postoji ili nije aktivan.");

        if (request.Kolicina <= 0)
            throw new ClientException("Količina mora biti veća od 0.");

        if (proizvod.KolicinaNaStanju < request.Kolicina)
            throw new ClientException("Nema dovoljno proizvoda na stanju.");

        proizvod.KolicinaNaStanju -= request.Kolicina;

        var entity = Mapper.Map<NarudzbaStavka>(request);
        entity.Cijena = proizvod.Cijena;

        Context.NarudzbaStavkas.Add(entity);
        await Context.SaveChangesAsync();

        await RecalculateOrderTotalAsync(request.NarudzbaId);

        return Mapper.Map<NarudzbaStavkaResponse>(entity);
    }

    public override async Task<NarudzbaStavkaResponse?> UpdateAsync(int id, NarudzbaStavkaUpdateRequest request)
    {
        var entity = await Context.NarudzbaStavkas.FindAsync(id);

        if (entity == null)
            return default;

        var proizvod = await Context.Proizvods
            .FirstOrDefaultAsync(x => x.ProizvodId == request.ProizvodId && x.Aktivan);

        if (proizvod == null)
            throw new ClientException("Proizvod ne postoji ili nije aktivan.");

        var razlika = request.Kolicina - entity.Kolicina;

        if (razlika > 0 && proizvod.KolicinaNaStanju < razlika)
            throw new ClientException("Nema dovoljno proizvoda na stanju.");

        proizvod.KolicinaNaStanju -= razlika;

        entity.ProizvodId = request.ProizvodId;
        entity.Kolicina = request.Kolicina;
        entity.Cijena = proizvod.Cijena;

        await Context.SaveChangesAsync();

        await RecalculateOrderTotalAsync(entity.NarudzbaId);

        return Mapper.Map<NarudzbaStavkaResponse>(entity);
    }

    public override async Task<bool> DeleteAsync(int id)
    {
        var entity = await Context.NarudzbaStavkas.FindAsync(id);

        if (entity == null)
            return false;

        var proizvod = await Context.Proizvods.FindAsync(entity.ProizvodId);

        if (proizvod != null)
            proizvod.KolicinaNaStanju += entity.Kolicina;

        var narudzbaId = entity.NarudzbaId;

        Context.NarudzbaStavkas.Remove(entity);
        await Context.SaveChangesAsync();

        await RecalculateOrderTotalAsync(narudzbaId);

        return true;
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