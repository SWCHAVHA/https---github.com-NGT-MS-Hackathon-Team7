using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Penalty_Calculation1.Models;

public partial class PenaltyCalculationContext : DbContext
{
    public PenaltyCalculationContext()
    {
    }

    public PenaltyCalculationContext(DbContextOptions<PenaltyCalculationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<HolidayCalender> HolidayCalenders { get; set; }

    public virtual DbSet<LoginUser> LoginUsers { get; set; }

    public virtual DbSet<Party> Parties { get; set; }

    public virtual DbSet<SecurityPenaltyRate> SecurityPenaltyRates { get; set; }

    public virtual DbSet<SecurityPrice> SecurityPrices { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Userrole> Userroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Penalty_Calculation;Username=postgres;Password=Priti@12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.CountryId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("country_id");
            entity.Property(e => e.CntlTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cntl_timestamp");
            entity.Property(e => e.CntlUserid)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("cntl_userid");
            entity.Property(e => e.CountryName)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<HolidayCalender>(entity =>
        {
            entity.HasKey(e => e.HolidayId).HasName("holiday_calender_pkey");

            entity.ToTable("holiday_calender");

            entity.Property(e => e.HolidayId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("holiday_id");
            entity.Property(e => e.CntlUserid)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("cntl_userid");
            entity.Property(e => e.CountryId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("country_id");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.HolidayDate).HasColumnName("holiday_date");
            entity.Property(e => e.LastUpdatedDate).HasColumnName("last_updated_date");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Country).WithMany(p => p.HolidayCalenders)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("holiday_calender_country_id_fkey");
        });

        modelBuilder.Entity<LoginUser>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("login_user_pkey");

            entity.ToTable("login_user");

            entity.Property(e => e.LoginId)
                .ValueGeneratedNever()
                .HasColumnName("login_id");
            entity.Property(e => e.CntlTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cntl_timestamp");
            entity.Property(e => e.CntlUserid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("cntl_userid");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("telephone_number");
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.LoginUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("login_user_role_id_fkey");
        });

        modelBuilder.Entity<Party>(entity =>
        {
            entity.HasKey(e => e.PartyId).HasName("party_pkey");

            entity.ToTable("party");

            entity.Property(e => e.PartyId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("party_id");
            entity.Property(e => e.CntlTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cntl_timestamp");
            entity.Property(e => e.CntlUserId)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("cntl_user_id");
            entity.Property(e => e.PartyName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("party_name");
        });

        modelBuilder.Entity<SecurityPenaltyRate>(entity =>
        {
            entity.HasKey(e => e.PenaltyId).HasName("security_penalty_rate_pkey");

            entity.ToTable("security_penalty_rate");

            entity.Property(e => e.PenaltyId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("penalty_id");
            entity.Property(e => e.LastUpdatedDate).HasColumnName("last_updated_date");
            entity.Property(e => e.PenaltyRate).HasColumnName("penalty_rate");
            entity.Property(e => e.ValidFromDate).HasColumnName("valid_from_date");
        });

        modelBuilder.Entity<SecurityPrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("security_price_pkey");

            entity.ToTable("security_price");

            entity.Property(e => e.PriceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("price_id");
            entity.Property(e => e.CntlTimestamp).HasColumnName("cntl_timestamp");
            entity.Property(e => e.CntlUserid)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("cntl_userid");
            entity.Property(e => e.IsinSecId)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("isin_sec_id");
            entity.Property(e => e.Poh).HasColumnName("poh");
            entity.Property(e => e.SecPrice).HasColumnName("sec_price");
            entity.Property(e => e.ValidFromDate).HasColumnName("valid_from_date");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("transaction_pkey");

            entity.ToTable("transaction");

            entity.Property(e => e.TransactionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("transaction_id");
            entity.Property(e => e.CalendarId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("calendar_id");
            entity.Property(e => e.CntlTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cntl_timestamp");
            entity.Property(e => e.CntlUserid)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("cntl_userid");
            entity.Property(e => e.CounterPartyId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("counter_party_id");
            entity.Property(e => e.CounterPartyRoleCd)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("counter_party_role_cd");
            entity.Property(e => e.FailingPartyRoleCd)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("failing_party_role_cd");
            entity.Property(e => e.InstructionTypeCode)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("instruction_type_code");
            entity.Property(e => e.Isin)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("isin");
            entity.Property(e => e.LoginId).HasColumnName("login_id");
            entity.Property(e => e.MatchingReference)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("matching_reference");
            entity.Property(e => e.PartyId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("party_id");
            entity.Property(e => e.PartyRoleCd)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("party_role_cd");
            entity.Property(e => e.PenaltyAmount)
                .HasPrecision(6, 3)
                .HasColumnName("penalty_amount");
            entity.Property(e => e.PenaltyId).HasColumnName("penalty_id");
            entity.Property(e => e.PlaceOfHoldingTechNumber).HasColumnName("place_of_holding_tech_number");
            entity.Property(e => e.PriceId).HasColumnName("price_id");
            entity.Property(e => e.SecurityQuantity).HasColumnName("security_quantity");
            entity.Property(e => e.SettlementCashAmount)
                .HasPrecision(6, 2)
                .HasColumnName("settlement_cash_amount");
            entity.Property(e => e.SettlementDate).HasColumnName("settlement_date");
            entity.Property(e => e.Sign)
                .HasColumnType("bit(1)")
                .HasColumnName("sign");
            entity.Property(e => e.TransactionTypeCode)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("transaction_type_code");

            entity.HasOne(d => d.Login).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaction_login_id_fkey");

            entity.HasOne(d => d.Party).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaction_party_id_fkey");

            entity.HasOne(d => d.Penalty).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PenaltyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaction_penalty_id_fkey");

            entity.HasOne(d => d.Price).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaction_price_id_fkey");
        });

        modelBuilder.Entity<Userrole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("userrole_pkey");

            entity.ToTable("userrole");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.CntlTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cntl_timestamp");
            entity.Property(e => e.CntlUserId)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("cntl_user_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
