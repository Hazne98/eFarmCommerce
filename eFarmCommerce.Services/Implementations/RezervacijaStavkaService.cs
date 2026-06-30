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

public class RezervacijaStavkaService
    : BaseCRUDService<RezervacijaStavkaResponse, RezervacijaStavkaSearchObject, RezervacijaStavka, RezervacijaStavkaInsertRequest, RezervacijaStavkaUpdateRequest>,
      IRezervacijaStavkaService
{
    public RezervacijaStavkaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<RezervacijaStavka> AddFilter(IQueryable<RezervacijaStavka> query, RezervacijaStavkaSearchObject search)
    {
        query = query
            .Include(x => x.OpremaLokacija)
                .ThenInclude(x => x.Oprema)
            .Include(x => x.OpremaLokacija)
                .ThenInclude(x => x.Lokacija);

        if (search.RezervacijaId.HasValue)
            query = query.Where(x => x.RezervacijaId == search.RezervacijaId.Value);

        if (search.OpremaLokacijaId.HasValue)
            query = query.Where(x => x.OpremaLokacijaId == search.OpremaLokacijaId.Value);

        if (search.OpremaId.HasValue)
            query = query.Where(x => x.OpremaLokacija.OpremaId == search.OpremaId.Value);

        if (search.LokacijaId.HasValue)
            query = query.Where(x => x.OpremaLokacija.LokacijaId == search.LokacijaId.Value);

        if (!string.IsNullOrWhiteSpace(search.OpremaNaziv))
            query = query.Where(x => x.OpremaLokacija.Oprema.Naziv.Contains(search.OpremaNaziv));

        if (!string.IsNullOrWhiteSpace(search.LokacijaNaziv))
            query = query.Where(x => x.OpremaLokacija.Lokacija.Naziv.Contains(search.LokacijaNaziv));

        return query;
    }

    public override async Task<RezervacijaStavkaResponse> InsertAsync(RezervacijaStavkaInsertRequest request)
    {
        var rezervacija = await Context.Rezervacijas.FindAsync(request.RezervacijaId);
        if (rezervacija == null)
            throw new ClientException("Rezervacija ne postoji.");

        var opremaLokacija = await Context.OpremaLokacijas
            .Include(x => x.Oprema)
            .FirstOrDefaultAsync(x => x.OpremaLokacijaId == request.OpremaLokacijaId);

        if (opremaLokacija == null)
            throw new ClientException("Oprema na lokaciji ne postoji.");

        if (request.Kolicina <= 0)
            throw new ClientException("Količina mora biti veća od 0.");

        if (opremaLokacija.KolicinaDostupna < request.Kolicina)
            throw new ClientException("Nema dovoljno dostupne opreme na lokaciji.");

        opremaLokacija.KolicinaDostupna -= request.Kolicina;

        var entity = Mapper.Map<RezervacijaStavka>(request);
        entity.CijenaPoDanu = opremaLokacija.Oprema.CijenaPoDanu;

        Context.RezervacijaStavkas.Add(entity);
        await Context.SaveChangesAsync();

        await RecalculateRezervacijaTotalAsync(request.RezervacijaId);

        return Mapper.Map<RezervacijaStavkaResponse>(entity);
    }

    public override async Task<bool> DeleteAsync(int id)
    {
        var entity = await Context.RezervacijaStavkas.FindAsync(id);

        if (entity == null)
            return false;

        var opremaLokacija = await Context.OpremaLokacijas.FindAsync(entity.OpremaLokacijaId);

        if (opremaLokacija != null)
            opremaLokacija.KolicinaDostupna += entity.Kolicina;

        var rezervacijaId = entity.RezervacijaId;

        Context.RezervacijaStavkas.Remove(entity);
        await Context.SaveChangesAsync();

        await RecalculateRezervacijaTotalAsync(rezervacijaId);

        return true;
    }

    private async Task RecalculateRezervacijaTotalAsync(int rezervacijaId)
    {
        var rezervacija = await Context.Rezervacijas.FindAsync(rezervacijaId);
        if (rezervacija == null)
            return;

        var brojDana = Math.Max(1, (rezervacija.DatumZavrsetka.Date - rezervacija.DatumPocetka.Date).Days);

        var total = await Context.RezervacijaStavkas
            .Where(x => x.RezervacijaId == rezervacijaId)
            .SumAsync(x => x.Kolicina * x.CijenaPoDanu * brojDana);

        rezervacija.UkupnaCijena = total;

        await Context.SaveChangesAsync();
    }
}