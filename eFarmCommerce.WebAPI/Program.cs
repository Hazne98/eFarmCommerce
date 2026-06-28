using eFarmCommerce.Common.Services.Crypto;
using eFarmCommerce.Services.Database;
using eFarmCommerce.Services.Implementations;
using eFarmCommerce.Services.Interfaces;
using eFarmCommerce.WebAPI.Filters;
using eFarmCommerce.WebAPI.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using eFarmCommerce.Model.Requests;
using eFarmCommerce.Services.Validators;
using FluentValidation;

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

var config = TypeAdapterConfig.GlobalSettings;
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

builder.Services.AddScoped<INarudzbaService, NarudzbaService>();
builder.Services.AddScoped<INarudzbaStavkaService, NarudzbaStavkaService>();

builder.Services.AddScoped<IKorpaService, KorpaService>();
builder.Services.AddScoped<IKorpaStavkaService, KorpaStavkaService>();

builder.Services.AddScoped<IValidator<OpremaInsertRequest>, OpremaInsertValidator>();
builder.Services.AddScoped<IValidator<OpremaUpdateRequest>, OpremaUpdateValidator>();

builder.Services.AddScoped<IValidator<ProizvodjacInsertRequest>, ProizvodjacInsertValidator>();
builder.Services.AddScoped<IValidator<ProizvodjacUpdateRequest>, ProizvodjacUpdateValidator>();

builder.Services.AddScoped<IValidator<KorisnikInsertRequest>, KorisnikInsertValidator>();
builder.Services.AddScoped<IValidator<KorisnikUpdateRequest>, KorisnikUpdateValidator>();

builder.Services.AddScoped<IValidator<NarudzbaInsertRequest>, NarudzbaInsertValidator>();
builder.Services.AddScoped<IValidator<RezervacijaInsertRequest>, RezervacijaInsertValidator>();

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