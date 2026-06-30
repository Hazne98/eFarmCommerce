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

public class RecenzijaService
    : BaseCRUDService<RecenzijaResponse, RecenzijaSearchObject, Recenzija, RecenzijaInsertRequest, RecenzijaUpdateRequest>,
      IRecenzijaService
{
    public RecenzijaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<Recenzija> AddFilter(IQueryable<Recenzija> query, RecenzijaSearchObject search)
    {
        query = query
            .Include(x => x.Korisnik)
            .Include(x => x.Proizvod)
            .Include(x => x.Oprema);

        if (search.KorisnikId.HasValue)
            query = query.Where(x => x.KorisnikId == search.KorisnikId);

        if (search.ProizvodId.HasValue)
            query = query.Where(x => x.ProizvodId == search.ProizvodId);

        if (search.OpremaId.HasValue)
            query = query.Where(x => x.OpremaId == search.OpremaId);

        if (search.Ocjena.HasValue)
            query = query.Where(x => x.Ocjena == search.Ocjena);

        return query;
    }

    public override async Task<RecenzijaResponse> InsertAsync(RecenzijaInsertRequest request)
    {
        if (request.Ocjena < 1 || request.Ocjena > 5)
            throw new ClientException("Ocjena mora biti između 1 i 5.");

        if (request.ProizvodId == null && request.OpremaId == null)
            throw new ClientException("Recenzija mora biti vezana za proizvod ili opremu.");

        if (request.ProizvodId != null && request.OpremaId != null)
            throw new ClientException("Recenzija ne može biti vezana i za proizvod i za opremu.");

        if (request.ProizvodId.HasValue)
        {
            var postoji = await Context.Recenzijas.AnyAsync(x =>
                x.KorisnikId == request.KorisnikId &&
                x.ProizvodId == request.ProizvodId);

            if (postoji)
                throw new ClientException("Već ste ocijenili ovaj proizvod.");
        }

        if (request.OpremaId.HasValue)
        {
            var postoji = await Context.Recenzijas.AnyAsync(x =>
                x.KorisnikId == request.KorisnikId &&
                x.OpremaId == request.OpremaId);

            if (postoji)
                throw new ClientException("Već ste ocijenili ovu opremu.");
        }

        var entity = Mapper.Map<Recenzija>(request);
        entity.Datum = DateTime.UtcNow;

        Context.Recenzijas.Add(entity);
        await Context.SaveChangesAsync();

        var created = await Context.Recenzijas
            .Include(x => x.Korisnik)
            .Include(x => x.Proizvod)
            .Include(x => x.Oprema)
            .FirstAsync(x => x.RecenzijaId == entity.RecenzijaId);

        return Mapper.Map<RecenzijaResponse>(created);
    }
}