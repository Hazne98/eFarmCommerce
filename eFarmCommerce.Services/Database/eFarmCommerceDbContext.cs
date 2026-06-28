using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using eFarmCommerce.Services.Database.Entities;

namespace eFarmCommerce.Services.Database;

public partial class eFarmCommerceDbContext : DbContext
{
    public eFarmCommerceDbContext()
    {
    }

    public eFarmCommerceDbContext(DbContextOptions<eFarmCommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Drzava> Drzavas { get; set; }

    public virtual DbSet<Favorit> Favorits { get; set; }

    public virtual DbSet<Grad> Grads { get; set; }

    public virtual DbSet<HistorijaPretrage> HistorijaPretrages { get; set; }

    public virtual DbSet<KategorijaOpreme> KategorijaOpremes { get; set; }

    public virtual DbSet<KategorijaProizvodum> KategorijaProizvoda { get; set; }

    public virtual DbSet<Korisnik> Korisniks { get; set; }

    public virtual DbSet<Korpa> Korpas { get; set; }

    public virtual DbSet<KorpaStavka> KorpaStavkas { get; set; }

    public virtual DbSet<Lokacija> Lokacijas { get; set; }

    public virtual DbSet<NacinPlacanja> NacinPlacanjas { get; set; }

    public virtual DbSet<Narudzba> Narudzbas { get; set; }

    public virtual DbSet<NarudzbaStavka> NarudzbaStavkas { get; set; }

    public virtual DbSet<Notifikacija> Notifikacijas { get; set; }

    public virtual DbSet<Obavijest> Obavijests { get; set; }

    public virtual DbSet<Oprema> Opremas { get; set; }

    public virtual DbSet<OpremaLokacija> OpremaLokacijas { get; set; }

    public virtual DbSet<Placanje> Placanjes { get; set; }

    public virtual DbSet<PovratRefund> PovratRefunds { get; set; }

    public virtual DbSet<PreporukaLog> PreporukaLogs { get; set; }

    public virtual DbSet<Proizvod> Proizvods { get; set; }

    public virtual DbSet<Proizvodjac> Proizvodjacs { get; set; }

    public virtual DbSet<Promocija> Promocijas { get; set; }

    public virtual DbSet<PromocijaOprema> PromocijaOpremas { get; set; }

    public virtual DbSet<PromocijaProizvod> PromocijaProizvods { get; set; }

    public virtual DbSet<Recenzija> Recenzijas { get; set; }

    public virtual DbSet<Rezervacija> Rezervacijas { get; set; }

    public virtual DbSet<RezervacijaStavka> RezervacijaStavkas { get; set; }

    public virtual DbSet<StatusNarudzbe> StatusNarudzbes { get; set; }

    public virtual DbSet<StatusPlacanja> StatusPlacanjas { get; set; }

    public virtual DbSet<StatusPovratum> StatusPovrata { get; set; }

    public virtual DbSet<StatusRezervacije> StatusRezervacijes { get; set; }

    public virtual DbSet<Uloga> Ulogas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=170174;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drzava>(entity =>
        {
            entity.HasKey(e => e.DrzavaId).HasName("PK__Drzava__89CED84677496502");

            entity.ToTable("Drzava");

            entity.HasIndex(e => e.Naziv, "UQ__Drzava__603E8146132CBF53").IsUnique();

            entity.Property(e => e.DrzavaId).HasColumnName("DrzavaID");
            entity.Property(e => e.Naziv).HasMaxLength(100);
        });

        modelBuilder.Entity<Favorit>(entity =>
        {
            entity.HasKey(e => e.FavoritId).HasName("PK__Favorit__C32DB22CEFAA8E4B");

            entity.ToTable("Favorit");

            entity.HasIndex(e => new { e.KorisnikId, e.OpremaId }, "UQ_Favorit_Korisnik_Oprema")
                .IsUnique()
                .HasFilter("([OpremaID] IS NOT NULL)");

            entity.HasIndex(e => new { e.KorisnikId, e.ProizvodId }, "UQ_Favorit_Korisnik_Proizvod")
                .IsUnique()
                .HasFilter("([ProizvodID] IS NOT NULL)");

            entity.Property(e => e.FavoritId).HasColumnName("FavoritID");
            entity.Property(e => e.DatumDodavanja).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.OpremaId).HasColumnName("OpremaID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Favorits)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorit_Korisnik");

            entity.HasOne(d => d.Oprema).WithMany(p => p.Favorits)
                .HasForeignKey(d => d.OpremaId)
                .HasConstraintName("FK_Favorit_Oprema");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.Favorits)
                .HasForeignKey(d => d.ProizvodId)
                .HasConstraintName("FK_Favorit_Proizvod");
        });

        modelBuilder.Entity<Grad>(entity =>
        {
            entity.HasKey(e => e.GradId).HasName("PK__Grad__B0F3C9848BCB26ED");

            entity.ToTable("Grad");

            entity.Property(e => e.GradId).HasColumnName("GradID");
            entity.Property(e => e.DrzavaId).HasColumnName("DrzavaID");
            entity.Property(e => e.Naziv).HasMaxLength(100);

            entity.HasOne(d => d.Drzava).WithMany(p => p.Grads)
                .HasForeignKey(d => d.DrzavaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grad_Drzava");
        });

        modelBuilder.Entity<HistorijaPretrage>(entity =>
        {
            entity.HasKey(e => e.HistorijaPretrageId).HasName("PK__Historij__23666CFCFA665B29");

            entity.ToTable("HistorijaPretrage");

            entity.Property(e => e.HistorijaPretrageId).HasColumnName("HistorijaPretrageID");
            entity.Property(e => e.DatumPretrage).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.Pojam).HasMaxLength(150);
            entity.Property(e => e.Tip).HasMaxLength(50);

            entity.HasOne(d => d.Korisnik).WithMany(p => p.HistorijaPretrages)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorijaPretrage_Korisnik");
        });

        modelBuilder.Entity<KategorijaOpreme>(entity =>
        {
            entity.HasKey(e => e.KategorijaOpremeId).HasName("PK__Kategori__1E716FFA8410FA1A");

            entity.ToTable("KategorijaOpreme");

            entity.HasIndex(e => e.Naziv, "UQ__Kategori__603E8146ED8C7BA8").IsUnique();

            entity.Property(e => e.KategorijaOpremeId).HasColumnName("KategorijaOpremeID");
            entity.Property(e => e.Naziv).HasMaxLength(100);
        });

        modelBuilder.Entity<KategorijaProizvodum>(entity =>
        {
            entity.HasKey(e => e.KategorijaProizvodaId).HasName("PK__Kategori__82EDFB00BE0C5416");

            entity.HasIndex(e => e.Naziv, "UQ__Kategori__603E81468A78067D").IsUnique();

            entity.Property(e => e.KategorijaProizvodaId).HasColumnName("KategorijaProizvodaID");
            entity.Property(e => e.Naziv).HasMaxLength(100);
        });

        modelBuilder.Entity<Korisnik>(entity =>
        {
            entity.HasKey(e => e.KorisnikId).HasName("PK__Korisnik__80B06D616E1440AB");

            entity.ToTable("Korisnik");

            entity.HasIndex(e => e.KorisnickoIme, "UQ__Korisnik__992E6F9209B7BC93").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Korisnik__A9D1053432B7A31A").IsUnique();

            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.Adresa).HasMaxLength(255);
            entity.Property(e => e.Aktivan).HasDefaultValue(true);
            entity.Property(e => e.DatumRegistracije).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.GradId).HasColumnName("GradID");
            entity.Property(e => e.Ime).HasMaxLength(100);
            entity.Property(e => e.KorisnickoIme).HasMaxLength(100);
            entity.Property(e => e.LozinkaHash).HasMaxLength(500);
            entity.Property(e => e.LozinkaSalt).HasMaxLength(500);
            entity.Property(e => e.Prezime).HasMaxLength(100);
            entity.Property(e => e.Telefon).HasMaxLength(30);
            entity.Property(e => e.UlogaId).HasColumnName("UlogaID");

            entity.HasOne(d => d.Grad).WithMany(p => p.Korisniks)
                .HasForeignKey(d => d.GradId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korisnik_Grad");

            entity.HasOne(d => d.Uloga).WithMany(p => p.Korisniks)
                .HasForeignKey(d => d.UlogaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korisnik_Uloga");
        });

        modelBuilder.Entity<Korpa>(entity =>
        {
            entity.HasKey(e => e.KorpaId).HasName("PK__Korpa__C298DFB37A277104");

            entity.ToTable("Korpa");

            entity.HasIndex(e => e.KorisnikId, "UQ__Korpa__80B06D60A30B439C").IsUnique();

            entity.Property(e => e.KorpaId).HasColumnName("KorpaID");
            entity.Property(e => e.DatumKreiranja).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

            entity.HasOne(d => d.Korisnik).WithOne(p => p.Korpa)
                .HasForeignKey<Korpa>(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korpa_Korisnik");
        });

        modelBuilder.Entity<KorpaStavka>(entity =>
        {
            entity.HasKey(e => e.KorpaStavkaId).HasName("PK__KorpaSta__0CCAC36739CEB7C4");

            entity.ToTable("KorpaStavka");

            entity.HasIndex(e => new { e.KorpaId, e.ProizvodId }, "UQ_KorpaStavka").IsUnique();

            entity.Property(e => e.KorpaStavkaId).HasColumnName("KorpaStavkaID");
            entity.Property(e => e.DatumDodavanja).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorpaId).HasColumnName("KorpaID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

            entity.HasOne(d => d.Korpa).WithMany(p => p.KorpaStavkas)
                .HasForeignKey(d => d.KorpaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KorpaStavka_Korpa");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.KorpaStavkas)
                .HasForeignKey(d => d.ProizvodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KorpaStavka_Proizvod");
        });

        modelBuilder.Entity<Lokacija>(entity =>
        {
            entity.HasKey(e => e.LokacijaId).HasName("PK__Lokacija__49DE2C2AB792548A");

            entity.ToTable("Lokacija");

            entity.Property(e => e.LokacijaId).HasColumnName("LokacijaID");
            entity.Property(e => e.Adresa).HasMaxLength(255);
            entity.Property(e => e.Aktivan).HasDefaultValue(true);
            entity.Property(e => e.GradId).HasColumnName("GradID");
            entity.Property(e => e.Naziv).HasMaxLength(150);

            entity.HasOne(d => d.Grad).WithMany(p => p.Lokacijas)
                .HasForeignKey(d => d.GradId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lokacija_Grad");
        });

        modelBuilder.Entity<NacinPlacanja>(entity =>
        {
            entity.HasKey(e => e.NacinPlacanjaId).HasName("PK__NacinPla__AD0C4709B092559F");

            entity.ToTable("NacinPlacanja");

            entity.HasIndex(e => e.Naziv, "UQ__NacinPla__603E81467F3F3D1A").IsUnique();

            entity.Property(e => e.NacinPlacanjaId).HasColumnName("NacinPlacanjaID");
            entity.Property(e => e.Naziv).HasMaxLength(50);
        });

        modelBuilder.Entity<Narudzba>(entity =>
        {
            entity.HasKey(e => e.NarudzbaId).HasName("PK__Narudzba__FBEC135700BA6B3C");

            entity.ToTable("Narudzba");

            entity.Property(e => e.NarudzbaId).HasColumnName("NarudzbaID");
            entity.Property(e => e.AdresaDostave).HasMaxLength(255);
            entity.Property(e => e.DatumNarudzbe).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.OdobrioKorisnikId).HasColumnName("OdobrioKorisnikID");
            entity.Property(e => e.RazlogOtkazivanja).HasMaxLength(500);
            entity.Property(e => e.StatusNarudzbeId).HasColumnName("StatusNarudzbeID");
            entity.Property(e => e.UkupnaCijena).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.NarudzbaKorisniks)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Narudzba_Korisnik");

            entity.HasOne(d => d.OdobrioKorisnik).WithMany(p => p.NarudzbaOdobrioKorisniks)
                .HasForeignKey(d => d.OdobrioKorisnikId)
                .HasConstraintName("FK_Narudzba_Odobrio");

            entity.HasOne(d => d.StatusNarudzbe).WithMany(p => p.Narudzbas)
                .HasForeignKey(d => d.StatusNarudzbeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Narudzba_Status");
        });

        modelBuilder.Entity<NarudzbaStavka>(entity =>
        {
            entity.HasKey(e => e.NarudzbaStavkaId).HasName("PK__Narudzba__7AC08D98CB66B429");

            entity.ToTable("NarudzbaStavka");

            entity.Property(e => e.NarudzbaStavkaId).HasColumnName("NarudzbaStavkaID");
            entity.Property(e => e.Cijena).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NarudzbaId).HasColumnName("NarudzbaID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

            entity.HasOne(d => d.Narudzba).WithMany(p => p.NarudzbaStavkas)
                .HasForeignKey(d => d.NarudzbaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NarudzbaStavka_Narudzba");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.NarudzbaStavkas)
                .HasForeignKey(d => d.ProizvodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NarudzbaStavka_Proizvod");
        });

        modelBuilder.Entity<Notifikacija>(entity =>
        {
            entity.HasKey(e => e.NotifikacijaId).HasName("PK__Notifika__595D01C346191EAF");

            entity.ToTable("Notifikacija");

            entity.Property(e => e.NotifikacijaId).HasColumnName("NotifikacijaID");
            entity.Property(e => e.DatumKreiranja).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.Naslov).HasMaxLength(150);

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Notifikacijas)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifikacija_Korisnik");
        });

        modelBuilder.Entity<Obavijest>(entity =>
        {
            entity.HasKey(e => e.ObavijestId).HasName("PK__Obavijes__99D330C0DEF4E929");

            entity.ToTable("Obavijest");

            entity.Property(e => e.ObavijestId).HasColumnName("ObavijestID");
            entity.Property(e => e.Aktivna).HasDefaultValue(true);
            entity.Property(e => e.DatumObjave).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KreiraoKorisnikId).HasColumnName("KreiraoKorisnikID");
            entity.Property(e => e.Naslov).HasMaxLength(150);

            entity.HasOne(d => d.KreiraoKorisnik).WithMany(p => p.Obavijests)
                .HasForeignKey(d => d.KreiraoKorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Obavijest_Korisnik");
        });

        modelBuilder.Entity<Oprema>(entity =>
        {
            entity.HasKey(e => e.OpremaId).HasName("PK__Oprema__5C2EDCF172671775");

            entity.ToTable("Oprema");

            entity.Property(e => e.OpremaId).HasColumnName("OpremaID");
            entity.Property(e => e.Aktivan).HasDefaultValue(true);
            entity.Property(e => e.CijenaPoDanu).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.KategorijaOpremeId).HasColumnName("KategorijaOpremeID");
            entity.Property(e => e.Naziv).HasMaxLength(150);
            entity.Property(e => e.ProizvodjacId).HasColumnName("ProizvodjacID");

            entity.HasOne(d => d.KategorijaOpreme).WithMany(p => p.Opremas)
                .HasForeignKey(d => d.KategorijaOpremeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Oprema_Kategorija");

            entity.HasOne(d => d.Proizvodjac).WithMany(p => p.Opremas)
                .HasForeignKey(d => d.ProizvodjacId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Oprema_Proizvodjac");
        });

        modelBuilder.Entity<OpremaLokacija>(entity =>
        {
            entity.HasKey(e => e.OpremaLokacijaId).HasName("PK__OpremaLo__1CA7BA85C5CC2C75");

            entity.ToTable("OpremaLokacija");

            entity.HasIndex(e => new { e.OpremaId, e.LokacijaId }, "UQ_OpremaLokacija").IsUnique();

            entity.Property(e => e.OpremaLokacijaId).HasColumnName("OpremaLokacijaID");
            entity.Property(e => e.LokacijaId).HasColumnName("LokacijaID");
            entity.Property(e => e.OpremaId).HasColumnName("OpremaID");

            entity.HasOne(d => d.Lokacija).WithMany(p => p.OpremaLokacijas)
                .HasForeignKey(d => d.LokacijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OpremaLokacija_Lokacija");

            entity.HasOne(d => d.Oprema).WithMany(p => p.OpremaLokacijas)
                .HasForeignKey(d => d.OpremaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OpremaLokacija_Oprema");
        });

        modelBuilder.Entity<Placanje>(entity =>
        {
            entity.HasKey(e => e.PlacanjeId).HasName("PK__Placanje__DDE16D8C70834431");

            entity.ToTable("Placanje");

            entity.Property(e => e.PlacanjeId).HasColumnName("PlacanjeID");
            entity.Property(e => e.DatumKreiranja).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Iznos).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.NacinPlacanjaId).HasColumnName("NacinPlacanjaID");
            entity.Property(e => e.NarudzbaId).HasColumnName("NarudzbaID");
            entity.Property(e => e.RezervacijaId).HasColumnName("RezervacijaID");
            entity.Property(e => e.StatusPlacanjaId).HasColumnName("StatusPlacanjaID");
            entity.Property(e => e.TransakcijaId)
                .HasMaxLength(200)
                .HasColumnName("TransakcijaID");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Placanjes)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Placanje_Korisnik");

            entity.HasOne(d => d.NacinPlacanja).WithMany(p => p.Placanjes)
                .HasForeignKey(d => d.NacinPlacanjaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Placanje_Nacin");

            entity.HasOne(d => d.Narudzba).WithMany(p => p.Placanjes)
                .HasForeignKey(d => d.NarudzbaId)
                .HasConstraintName("FK_Placanje_Narudzba");

            entity.HasOne(d => d.Rezervacija).WithMany(p => p.Placanjes)
                .HasForeignKey(d => d.RezervacijaId)
                .HasConstraintName("FK_Placanje_Rezervacija");

            entity.HasOne(d => d.StatusPlacanja).WithMany(p => p.Placanjes)
                .HasForeignKey(d => d.StatusPlacanjaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Placanje_Status");
        });

        modelBuilder.Entity<PovratRefund>(entity =>
        {
            entity.HasKey(e => e.PovratRefundId).HasName("PK__PovratRe__792F1643148DD5EF");

            entity.ToTable("PovratRefund");

            entity.Property(e => e.PovratRefundId).HasColumnName("PovratRefundID");
            entity.Property(e => e.DatumZahtjeva).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Iznos).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PlacanjeId).HasColumnName("PlacanjeID");
            entity.Property(e => e.Razlog).HasMaxLength(500);
            entity.Property(e => e.RefundTransakcijaId)
                .HasMaxLength(200)
                .HasColumnName("RefundTransakcijaID");
            entity.Property(e => e.StatusPovrataId).HasColumnName("StatusPovrataID");

            entity.HasOne(d => d.Placanje).WithMany(p => p.PovratRefunds)
                .HasForeignKey(d => d.PlacanjeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PovratRefund_Placanje");

            entity.HasOne(d => d.StatusPovrata).WithMany(p => p.PovratRefunds)
                .HasForeignKey(d => d.StatusPovrataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PovratRefund_Status");
        });

        modelBuilder.Entity<PreporukaLog>(entity =>
        {
            entity.HasKey(e => e.PreporukaLogId).HasName("PK__Preporuk__758718CFEDD1B038");

            entity.ToTable("PreporukaLog");

            entity.Property(e => e.PreporukaLogId).HasColumnName("PreporukaLogID");
            entity.Property(e => e.DatumPreporuke).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.OpremaId).HasColumnName("OpremaID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");
            entity.Property(e => e.Razlog).HasMaxLength(500);
            entity.Property(e => e.Score).HasColumnType("decimal(10, 4)");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.PreporukaLogs)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PreporukaLog_Korisnik");

            entity.HasOne(d => d.Oprema).WithMany(p => p.PreporukaLogs)
                .HasForeignKey(d => d.OpremaId)
                .HasConstraintName("FK_PreporukaLog_Oprema");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.PreporukaLogs)
                .HasForeignKey(d => d.ProizvodId)
                .HasConstraintName("FK_PreporukaLog_Proizvod");
        });

        modelBuilder.Entity<Proizvod>(entity =>
        {
            entity.HasKey(e => e.ProizvodId).HasName("PK__Proizvod__21A8BE18E867214B");

            entity.ToTable("Proizvod");

            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");
            entity.Property(e => e.Aktivan).HasDefaultValue(true);
            entity.Property(e => e.Cijena).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.KategorijaProizvodaId).HasColumnName("KategorijaProizvodaID");
            entity.Property(e => e.Naziv).HasMaxLength(150);
            entity.Property(e => e.ProizvodjacId).HasColumnName("ProizvodjacID");

            entity.HasOne(d => d.KategorijaProizvoda).WithMany(p => p.Proizvods)
                .HasForeignKey(d => d.KategorijaProizvodaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Kategorija");

            entity.HasOne(d => d.Proizvodjac).WithMany(p => p.Proizvods)
                .HasForeignKey(d => d.ProizvodjacId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Proizvodjac");
        });

        modelBuilder.Entity<Proizvodjac>(entity =>
        {
            entity.HasKey(e => e.ProizvodjacId).HasName("PK__Proizvod__3722E5F5086817C5");

            entity.ToTable("Proizvodjac");

            entity.Property(e => e.ProizvodjacId).HasColumnName("ProizvodjacID");
            entity.Property(e => e.Adresa).HasMaxLength(255);
            entity.Property(e => e.Aktivan).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.GradId).HasColumnName("GradID");
            entity.Property(e => e.Naziv).HasMaxLength(150);
            entity.Property(e => e.Telefon).HasMaxLength(30);

            entity.HasOne(d => d.Grad).WithMany(p => p.Proizvodjacs)
                .HasForeignKey(d => d.GradId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvodjac_Grad");
        });

        modelBuilder.Entity<Promocija>(entity =>
        {
            entity.HasKey(e => e.PromocijaId).HasName("PK__Promocij__2C5ACD71E73B6111");

            entity.ToTable("Promocija");

            entity.Property(e => e.PromocijaId).HasColumnName("PromocijaID");
            entity.Property(e => e.Aktivna).HasDefaultValue(true);
            entity.Property(e => e.Naziv).HasMaxLength(150);
            entity.Property(e => e.PopustProcenat).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<PromocijaOprema>(entity =>
        {
            entity.HasKey(e => e.PromocijaOpremaId).HasName("PK__Promocij__5D97C9352FE1A814");

            entity.ToTable("PromocijaOprema");

            entity.HasIndex(e => new { e.PromocijaId, e.OpremaId }, "UQ_PromocijaOprema").IsUnique();

            entity.Property(e => e.PromocijaOpremaId).HasColumnName("PromocijaOpremaID");
            entity.Property(e => e.OpremaId).HasColumnName("OpremaID");
            entity.Property(e => e.PromocijaId).HasColumnName("PromocijaID");

            entity.HasOne(d => d.Oprema).WithMany(p => p.PromocijaOpremas)
                .HasForeignKey(d => d.OpremaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PromocijaOprema_Oprema");

            entity.HasOne(d => d.Promocija).WithMany(p => p.PromocijaOpremas)
                .HasForeignKey(d => d.PromocijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PromocijaOprema_Promocija");
        });

        modelBuilder.Entity<PromocijaProizvod>(entity =>
        {
            entity.HasKey(e => e.PromocijaProizvodId).HasName("PK__Promocij__A6E94E6535B35F34");

            entity.ToTable("PromocijaProizvod");

            entity.HasIndex(e => new { e.PromocijaId, e.ProizvodId }, "UQ_PromocijaProizvod").IsUnique();

            entity.Property(e => e.PromocijaProizvodId).HasColumnName("PromocijaProizvodID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");
            entity.Property(e => e.PromocijaId).HasColumnName("PromocijaID");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.PromocijaProizvods)
                .HasForeignKey(d => d.ProizvodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PromocijaProizvod_Proizvod");

            entity.HasOne(d => d.Promocija).WithMany(p => p.PromocijaProizvods)
                .HasForeignKey(d => d.PromocijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PromocijaProizvod_Promocija");
        });

        modelBuilder.Entity<Recenzija>(entity =>
        {
            entity.HasKey(e => e.RecenzijaId).HasName("PK__Recenzij__D36C6090EDB4D941");

            entity.ToTable("Recenzija");

            entity.Property(e => e.RecenzijaId).HasColumnName("RecenzijaID");
            entity.Property(e => e.Datum).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.Odobrena).HasDefaultValue(true);
            entity.Property(e => e.OpremaId).HasColumnName("OpremaID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Recenzijas)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recenzija_Korisnik");

            entity.HasOne(d => d.Oprema).WithMany(p => p.Recenzijas)
                .HasForeignKey(d => d.OpremaId)
                .HasConstraintName("FK_Recenzija_Oprema");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.Recenzijas)
                .HasForeignKey(d => d.ProizvodId)
                .HasConstraintName("FK_Recenzija_Proizvod");
        });

        modelBuilder.Entity<Rezervacija>(entity =>
        {
            entity.HasKey(e => e.RezervacijaId).HasName("PK__Rezervac__CABA44FD83453D8C");

            entity.ToTable("Rezervacija");

            entity.Property(e => e.RezervacijaId).HasColumnName("RezervacijaID");
            entity.Property(e => e.DatumKreiranja).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.OdobrioKorisnikId).HasColumnName("OdobrioKorisnikID");
            entity.Property(e => e.RazlogOtkazivanja).HasMaxLength(500);
            entity.Property(e => e.StatusRezervacijeId).HasColumnName("StatusRezervacijeID");
            entity.Property(e => e.UkupnaCijena).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.RezervacijaKorisniks)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rezervacija_Korisnik");

            entity.HasOne(d => d.OdobrioKorisnik).WithMany(p => p.RezervacijaOdobrioKorisniks)
                .HasForeignKey(d => d.OdobrioKorisnikId)
                .HasConstraintName("FK_Rezervacija_Odobrio");

            entity.HasOne(d => d.StatusRezervacije).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.StatusRezervacijeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rezervacija_Status");
        });

        modelBuilder.Entity<RezervacijaStavka>(entity =>
        {
            entity.HasKey(e => e.RezervacijaStavkaId).HasName("PK__Rezervac__D11E533FDD4B2813");

            entity.ToTable("RezervacijaStavka");

            entity.Property(e => e.RezervacijaStavkaId).HasColumnName("RezervacijaStavkaID");
            entity.Property(e => e.CijenaPoDanu).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Kolicina).HasDefaultValue(1);
            entity.Property(e => e.OpremaLokacijaId).HasColumnName("OpremaLokacijaID");
            entity.Property(e => e.RezervacijaId).HasColumnName("RezervacijaID");

            entity.HasOne(d => d.OpremaLokacija).WithMany(p => p.RezervacijaStavkas)
                .HasForeignKey(d => d.OpremaLokacijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RezervacijaStavka_OpremaLokacija");

            entity.HasOne(d => d.Rezervacija).WithMany(p => p.RezervacijaStavkas)
                .HasForeignKey(d => d.RezervacijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RezervacijaStavka_Rezervacija");
        });

        modelBuilder.Entity<StatusNarudzbe>(entity =>
        {
            entity.HasKey(e => e.StatusNarudzbeId).HasName("PK__StatusNa__A8EF200F4EC0FD89");

            entity.ToTable("StatusNarudzbe");

            entity.HasIndex(e => e.Naziv, "UQ__StatusNa__603E8146AED649FE").IsUnique();

            entity.Property(e => e.StatusNarudzbeId).HasColumnName("StatusNarudzbeID");
            entity.Property(e => e.Naziv).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusPlacanja>(entity =>
        {
            entity.HasKey(e => e.StatusPlacanjaId).HasName("PK__StatusPl__2CD7D040C9F1DE59");

            entity.ToTable("StatusPlacanja");

            entity.HasIndex(e => e.Naziv, "UQ__StatusPl__603E8146C27D599B").IsUnique();

            entity.Property(e => e.StatusPlacanjaId).HasColumnName("StatusPlacanjaID");
            entity.Property(e => e.Naziv).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusPovratum>(entity =>
        {
            entity.HasKey(e => e.StatusPovrataId).HasName("PK__StatusPo__F0BE72661320F934");

            entity.HasIndex(e => e.Naziv, "UQ__StatusPo__603E8146990DFBEC").IsUnique();

            entity.Property(e => e.StatusPovrataId).HasColumnName("StatusPovrataID");
            entity.Property(e => e.Naziv).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusRezervacije>(entity =>
        {
            entity.HasKey(e => e.StatusRezervacijeId).HasName("PK__StatusRe__821BA8164E00424F");

            entity.ToTable("StatusRezervacije");

            entity.HasIndex(e => e.Naziv, "UQ__StatusRe__603E81464E3407D5").IsUnique();

            entity.Property(e => e.StatusRezervacijeId).HasColumnName("StatusRezervacijeID");
            entity.Property(e => e.Naziv).HasMaxLength(50);
        });

        modelBuilder.Entity<Uloga>(entity =>
        {
            entity.HasKey(e => e.UlogaId).HasName("PK__Uloga__DCAB23EBBB3BE013");

            entity.ToTable("Uloga");

            entity.HasIndex(e => e.Naziv, "UQ__Uloga__603E81467E3F14C1").IsUnique();

            entity.Property(e => e.UlogaId).HasColumnName("UlogaID");
            entity.Property(e => e.Naziv).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
