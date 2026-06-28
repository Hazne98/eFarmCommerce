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

public class KorpaStavkaService
    : BaseCRUDService<KorpaStavkaResponse, KorpaStavkaSearchObject, KorpaStavka, KorpaStavkaInsertRequest, KorpaStavkaUpdateRequest>,
      IKorpaStavkaService
{
    public KorpaStavkaService(eFarmCommerceDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    protected override IQueryable<KorpaStavka> AddFilter(IQueryable<KorpaStavka> query, KorpaStavkaSearchObject search)
    {
        query = query.Include(x => x.Proizvod);

        if (search.KorpaId.HasValue)
            query = query.Where(x => x.KorpaId == search.KorpaId.Value);

        if (search.ProizvodId.HasValue)
            query = query.Where(x => x.ProizvodId == search.ProizvodId.Value);

        if (!string.IsNullOrWhiteSpace(search.ProizvodNaziv))
            query = query.Where(x => x.Proizvod.Naziv.Contains(search.ProizvodNaziv));

        return query;
    }

    public override async Task<KorpaStavkaResponse> InsertAsync(KorpaStavkaInsertRequest request)
    {
        var korpaExists = await Context.Korpas.AnyAsync(x => x.KorpaId == request.KorpaId);

        if (!korpaExists)
            throw new ClientException("Korpa ne postoji.");

        var proizvod = await Context.Proizvods.FindAsync(request.ProizvodId);

        if (proizvod == null)
            throw new ClientException("Proizvod ne postoji.");

        if (proizvod.KolicinaNaStanju < request.Kolicina)
            throw new ClientException("Nema dovoljno proizvoda na stanju.");

        var existingItem = await Context.KorpaStavkas
            .FirstOrDefaultAsync(x => x.KorpaId == request.KorpaId && x.ProizvodId == request.ProizvodId);

        if (existingItem != null)
        {
            existingItem.Kolicina += request.Kolicina;

            if (existingItem.Kolicina > proizvod.KolicinaNaStanju)
                throw new ClientException("Nema dovoljno proizvoda na stanju.");

            await Context.SaveChangesAsync();

            return Mapper.Map<KorpaStavkaResponse>(existingItem);
        }

        return await base.InsertAsync(request);
    }

    public override async Task<KorpaStavkaResponse?> UpdateAsync(int id, KorpaStavkaUpdateRequest request)
    {
        var proizvod = await Context.Proizvods.FindAsync(request.ProizvodId);

        if (proizvod == null)
            throw new ClientException("Proizvod ne postoji.");

        if (proizvod.KolicinaNaStanju < request.Kolicina)
            throw new ClientException("Nema dovoljno proizvoda na stanju.");

        return await base.UpdateAsync(id, request);
    }
}