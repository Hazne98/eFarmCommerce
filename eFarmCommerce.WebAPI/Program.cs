using eFarmCommerce.Common.Services.Crypto;
using eFarmCommerce.Model.Requests;
using eFarmCommerce.Model.Responses;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Database.Entities;
using eFarmCommerce.Services.Implementations;
using eFarmCommerce.Services.Interfaces;
using eFarmCommerce.Services.Validators;
using eFarmCommerce.WebAPI.Filters;
using eFarmCommerce.WebAPI.Services;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthenticatedUserAccessor, HttpAuthenticatedUserAccessor>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<eFarmCommerceDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddMapster();
TypeAdapterConfig<Korisnik, KorisnikResponse>.NewConfig()
    .Map(dest => dest.GradNaziv, src => src.Grad.Naziv)
    .Map(dest => dest.UlogaNaziv, src => src.Uloga.Naziv);

TypeAdapterConfig<Korpa, KorpaResponse>.NewConfig()
    .Map(dest => dest.KorisnikImePrezime, src => src.Korisnik.Ime + " " + src.Korisnik.Prezime);

TypeAdapterConfig<Narudzba, NarudzbaResponse>.NewConfig()
    .Map(dest => dest.KorisnikImePrezime, src => src.Korisnik.Ime + " " + src.Korisnik.Prezime)
    .Map(dest => dest.StatusNarudzbeNaziv, src => src.StatusNarudzbe.Naziv);

TypeAdapterConfig<NarudzbaStavka, NarudzbaStavkaResponse>.NewConfig()
    .Map(dest => dest.ProizvodNaziv, src => src.Proizvod.Naziv);

TypeAdapterConfig<Rezervacija, RezervacijaResponse>.NewConfig()
    .Map(dest => dest.KorisnikImePrezime, src => src.Korisnik.Ime + " " + src.Korisnik.Prezime)
    .Map(dest => dest.StatusRezervacijeNaziv, src => src.StatusRezervacije.Naziv);

TypeAdapterConfig<RezervacijaStavka, RezervacijaStavkaResponse>.NewConfig()
    .Map(dest => dest.OpremaNaziv, src => src.OpremaLokacija.Oprema.Naziv)
    .Map(dest => dest.LokacijaNaziv, src => src.OpremaLokacija.Lokacija.Naziv)
    .Map(dest => dest.LokacijaAdresa, src => src.OpremaLokacija.Lokacija.Adresa);

TypeAdapterConfig<OpremaLokacija, OpremaLokacijaResponse>.NewConfig()
    .Map(dest => dest.OpremaNaziv, src => src.Oprema.Naziv)
    .Map(dest => dest.LokacijaNaziv, src => src.Lokacija.Naziv)
    .Map(dest => dest.LokacijaAdresa, src => src.Lokacija.Adresa);


var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddHttpClient<IPayPalService, PayPalService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PayPal:BaseUrl"]!);
});
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddScoped<ICryptoService, CryptoService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IKorisnikService, KorisnikService>();

builder.Services.AddScoped<IDrzavaService, DrzavaService>();
builder.Services.AddScoped<IGradService, GradService>();
builder.Services.AddScoped<IUlogaService, UlogaService>();

builder.Services.AddScoped<IKategorijaProizvodaService, KategorijaProizvodaService>();
builder.Services.AddScoped<IKategorijaOpremeService, KategorijaOpremeService>();

builder.Services.AddScoped<IStatusNarudzbeService, StatusNarudzbeService>();
builder.Services.AddScoped<IStatusRezervacijeService, StatusRezervacijeService>();
builder.Services.AddScoped<IStatusPlacanjaService, StatusPlacanjaService>();
builder.Services.AddScoped<IStatusPovrataService, StatusPovrataService>();
builder.Services.AddScoped<INacinPlacanjaService, NacinPlacanjaService>();

builder.Services.AddScoped<IProizvodjacService, ProizvodjacService>();
builder.Services.AddScoped<IProizvodService, ProizvodService>();
builder.Services.AddScoped<IOpremaService, OpremaService>();
builder.Services.AddScoped<ILokacijaService, LokacijaService>();
builder.Services.AddScoped<IOpremaLokacijaService, OpremaLokacijaService>();
builder.Services.AddScoped<IRezervacijaService, RezervacijaService>();
builder.Services.AddScoped<IRezervacijaStavkaService, RezervacijaStavkaService>();

builder.Services.AddScoped<INarudzbaService, NarudzbaService>();
builder.Services.AddScoped<INarudzbaStavkaService, NarudzbaStavkaService>();

builder.Services.AddScoped<IKorpaService, KorpaService>();
builder.Services.AddScoped<IKorpaStavkaService, KorpaStavkaService>();

builder.Services.AddScoped<IPlacanjeService, PlacanjeService>();
builder.Services.AddScoped<IPovratRefundService, PovratRefundService>();

builder.Services.AddScoped<IValidator<OpremaInsertRequest>, OpremaInsertValidator>();
builder.Services.AddScoped<IValidator<OpremaUpdateRequest>, OpremaUpdateValidator>();

builder.Services.AddScoped<IValidator<ProizvodjacInsertRequest>, ProizvodjacInsertValidator>();
builder.Services.AddScoped<IValidator<ProizvodjacUpdateRequest>, ProizvodjacUpdateValidator>();

builder.Services.AddScoped<IValidator<KorisnikInsertRequest>, KorisnikInsertValidator>();
builder.Services.AddScoped<IValidator<KorisnikUpdateRequest>, KorisnikUpdateValidator>();

builder.Services.AddScoped<IValidator<NarudzbaInsertRequest>, NarudzbaInsertValidator>();
builder.Services.AddScoped<IValidator<RezervacijaInsertRequest>, RezervacijaInsertValidator>();

builder.Services.AddScoped<IRecenzijaService, RecenzijaService>();
builder.Services.AddScoped<IHistorijaPretrageService, HistorijaPretrageService>();
builder.Services.AddScoped<INotifikacijaService, NotifikacijaService>();
builder.Services.AddScoped<IFavoritService, FavoritService>();

builder.Services.AddOpenApi();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"] ?? string.Empty;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddScoped<IValidator<ProizvodInsertRequest>, ProizvodInsertValidator>();
builder.Services.AddScoped<IValidator<ProizvodUpdateRequest>, ProizvodUpdateValidator>();

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();