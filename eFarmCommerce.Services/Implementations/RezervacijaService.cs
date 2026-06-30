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

public class RezervacijaService
    : BaseCRUDService<RezervacijaResponse, RezervacijaSearchObject, Rezervacija, RezervacijaInsertRequest, RezervacijaUpdateRequest>,
      IRezervacijaService
{
    public RezervacijaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Rezervacija> AddFilter(IQueryable<Rezervacija> query, RezervacijaSearchObject search)
    {
        query = query
            .Include(x => x.Korisnik)
            .Include(x => x.StatusRezervacije);

        if (search.KorisnikId.HasValue)
            query = query.Where(x => x.KorisnikId == search.KorisnikId.Value);

        if (search.StatusRezervacijeId.HasValue)
            query = query.Where(x => x.StatusRezervacijeId == search.StatusRezervacijeId.Value);

        if (search.DatumPocetkaOd.HasValue)
            query = query.Where(x => x.DatumPocetka >= search.DatumPocetkaOd.Value);

        if (search.DatumPocetkaDo.HasValue)
            query = query.Where(x => x.DatumPocetka <= search.DatumPocetkaDo.Value);

        if (search.DatumZavrsetkaOd.HasValue)
            query = query.Where(x => x.DatumZavrsetka >= search.DatumZavrsetkaOd.Value);

        if (search.DatumZavrsetkaDo.HasValue)
            query = query.Where(x => x.DatumZavrsetka <= search.DatumZavrsetkaDo.Value);

        if (search.CijenaOd.HasValue)
            query = query.Where(x => x.UkupnaCijena >= search.CijenaOd.Value);

        if (search.CijenaDo.HasValue)
            query = query.Where(x => x.UkupnaCijena <= search.CijenaDo.Value);

        return query;
    }

    public override async Task<RezervacijaResponse> InsertAsync(RezervacijaInsertRequest request)
    {
        if (request.DatumPocetka >= request.DatumZavrsetka)
            throw new ClientException("Datum početka mora biti prije datuma završetka.");

        var korisnikExists = await Context.Korisniks.AnyAsync(x => x.KorisnikId == request.KorisnikId);
        if (!korisnikExists)
            throw new ClientException("Korisnik ne postoji.");

        var statusExists = await Context.StatusRezervacijes.AnyAsync(x => x.StatusRezervacijeId == request.StatusRezervacijeId);
        if (!statusExists)
            throw new ClientException("Status rezervacije ne postoji.");

        var entity = Mapper.Map<Rezervacija>(request);
        entity.DatumKreiranja = DateTime.UtcNow;
        entity.UkupnaCijena = 0;

        Context.Rezervacijas.Add(entity);
        await Context.SaveChangesAsync();

        var created = await Context.Rezervacijas
            .Include(x => x.Korisnik)
            .Include(x => x.StatusRezervacije)
            .FirstAsync(x => x.RezervacijaId == entity.RezervacijaId);

        return Mapper.Map<RezervacijaResponse>(created);
    }

    public override async Task<RezervacijaResponse?> UpdateAsync(int id, RezervacijaUpdateRequest request)
    {
        if (request.DatumPocetka >= request.DatumZavrsetka)
            throw new ClientException("Datum početka mora biti prije datuma završetka.");

        var entity = await Context.Rezervacijas.FindAsync(id);

        if (entity == null)
            return default;

        var statusExists = await Context.StatusRezervacijes.AnyAsync(x => x.StatusRezervacijeId == request.StatusRezervacijeId);
        if (!statusExists)
            throw new ClientException("Status rezervacije ne postoji.");

        entity.StatusRezervacijeId = request.StatusRezervacijeId;
        entity.DatumPocetka = request.DatumPocetka;
        entity.DatumZavrsetka = request.DatumZavrsetka;
        entity.OdobrioKorisnikId = request.OdobrioKorisnikId;
        entity.RazlogOtkazivanja = request.RazlogOtkazivanja;
        entity.DatumPromjeneStatusa = DateTime.UtcNow;

        await Context.SaveChangesAsync();

        return Mapper.Map<RezervacijaResponse>(entity);
    }
}