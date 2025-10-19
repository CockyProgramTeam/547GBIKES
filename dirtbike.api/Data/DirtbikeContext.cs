using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using dirtbike.api.Models;

namespace dirtbike.api.Data;

public partial class DirtbikeContext : DbContext
{
    public DirtbikeContext()
    {
    }

    public DirtbikeContext(DbContextOptions<DirtbikeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apilog> Apilogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Park> Parks { get; set; }

    public virtual DbSet<ParkReview> ParkReviews { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<SalesCatalogue> SalesCatalogues { get; set; }

    public virtual DbSet<SalesSession> SalesSessions { get; set; }

    public virtual DbSet<Sessionlog> Sessionlogs { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userlog> Userlogs { get; set; }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }

    public virtual DbSet<Usersession> Usersessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=./SQLDATA/dirtbike.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apilog>(entity =>
        {
            entity.ToTable("apilog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apiname).HasColumnName("apiname");
            entity.Property(e => e.Apinumber).HasColumnName("apinumber");
            entity.Property(e => e.Apiresult).HasColumnName("apiresult");
            entity.Property(e => e.Eptype).HasColumnName("eptype");
            entity.Property(e => e.Hashid).HasColumnName("hashid");
            entity.Property(e => e.Parameterlist).HasColumnName("parameterlist");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.ParkId).HasColumnName("ParkID");
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.ToTable("Card");

            entity.HasIndex(e => new { e.CardLast4, e.CardExpDate }, "IX_Card_CardLast4_CardExpDate").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("CardID");
            entity.Property(e => e.IsActive).HasDefaultValue(1);
            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.DateAdded).HasDefaultValueSql("datetime('now')");
            entity.Property(e => e.IsCheckedOut).HasDefaultValue(0);
            entity.Property(e => e.ParkId).HasColumnName("ParkID");
            entity.Property(e => e.TotalPrice).HasComputedColumnSql();
            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.UserStatus).HasDefaultValue("active");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Park>(entity =>
        {
            entity.Property(e => e.ParkId).HasColumnName("ParkID");
            entity.Property(e => e.Columns)
                .HasColumnType("currentcampsites int")
                .HasColumnName("columns");
            entity.Property(e => e.Currentvisitors)
                .HasColumnType("INT")
                .HasColumnName("currentvisitors");
            entity.Property(e => e.Currentvisitorsadults)
                .HasColumnType("INT")
                .HasColumnName("currentvisitorsadults");
            entity.Property(e => e.Currentvisitorschildren)
                .HasColumnType("INT")
                .HasColumnName("currentvisitorschildren");
            entity.Property(e => e.DayPassPriceUsd).HasColumnName("DayPassPriceUSD");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Maxcampsites)
                .HasColumnType("INT")
                .HasColumnName("maxcampsites");
            entity.Property(e => e.Maxvisitors)
                .HasColumnType("INT")
                .HasColumnName("maxvisitors");
            entity.Property(e => e.Parklogourl).HasColumnType("string");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Trailmapurl)
                .HasColumnType("string")
                .HasColumnName("trailmapurl");
        });

        modelBuilder.Entity<ParkReview>(entity =>
        {
            entity.Property(e => e.DateApproved).HasColumnName("dateApproved");
            entity.Property(e => e.DateDenied).HasColumnName("dateDenied");
            entity.Property(e => e.DatePosted).HasColumnName("datePosted");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ParkId).HasColumnName("parkId");
            entity.Property(e => e.ReasonDescription).HasColumnName("reasonDescription");
            entity.Property(e => e.ReviewManagerId).HasColumnName("reviewManagerId");
            entity.Property(e => e.Stars).HasColumnName("stars");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
        });

        modelBuilder.Entity<SalesCatalogue>(entity =>
        {
            entity.ToTable("SalesCatalogue");

            entity.Property(e => e.SalesCatalogueId).HasColumnName("SalesCatalogueID");
            entity.Property(e => e.IsActive).HasDefaultValue(1);
            entity.Property(e => e.ParkId).HasColumnName("ParkID");
        });

        modelBuilder.Entity<SalesSession>(entity =>
        {
            entity.ToTable("SalesSession");

            entity.Property(e => e.SalesSessionId).HasColumnName("SalesSessionID");
            entity.Property(e => e.CartId1).HasColumnName("CartID1");
            entity.Property(e => e.CartId2).HasColumnName("CartID2");
            entity.Property(e => e.CartId3).HasColumnName("CartID3");
            entity.Property(e => e.CartId4).HasColumnName("CartID4");
            entity.Property(e => e.CartId5).HasColumnName("CartID5");
            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<Sessionlog>(entity =>
        {
            entity.ToTable("sessionlogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Hashid).HasColumnName("hashid");
            entity.Property(e => e.Moduleid).HasColumnName("moduleid");
            entity.Property(e => e.Sessionend)
                .HasColumnType("DATETIME")
                .HasColumnName("sessionend");
            entity.Property(e => e.Sessionstart)
                .HasColumnType("DATETIME")
                .HasColumnName("sessionstart");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.ToTable("Template");

            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Accountactiondate).HasColumnName("accountactiondate");
            entity.Property(e => e.Accountactiondescription).HasColumnName("accountactiondescription");
            entity.Property(e => e.Accountstatus).HasColumnName("accountstatus");
            entity.Property(e => e.Azureid).HasColumnName("azureid");
            entity.Property(e => e.Btn).HasColumnName("BTN");
            entity.Property(e => e.Companyid).HasColumnName("companyid");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Employee).HasColumnName("employee");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Fullname).HasColumnName("fullname");
            entity.Property(e => e.Groupid1).HasColumnName("groupid1");
            entity.Property(e => e.Groupid2).HasColumnName("groupid2");
            entity.Property(e => e.Groupid3).HasColumnName("groupid3");
            entity.Property(e => e.Groupid4).HasColumnName("groupid4");
            entity.Property(e => e.Groupid5).HasColumnName("groupid5");
            entity.Property(e => e.Hashedpassword).HasColumnName("hashedpassword");
            entity.Property(e => e.Iscertified).HasColumnName("iscertified");
            entity.Property(e => e.Jid).HasColumnName("jid");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Microsoftid).HasColumnName("microsoftid");
            entity.Property(e => e.Ncrid).HasColumnName("ncrid");
            entity.Property(e => e.Oracleid).HasColumnName("oracleid");
            entity.Property(e => e.Passwordtype).HasColumnName("passwordtype");
            entity.Property(e => e.Plainpassword).HasColumnName("plainpassword");
            entity.Property(e => e.Profileurl).HasColumnName("profileurl");
            entity.Property(e => e.Resettoken).HasColumnName("resettoken");
            entity.Property(e => e.Resettokenexpiration).HasColumnName("resettokenexpiration");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Twofactorkeyemaildestination).HasColumnName("twofactorkeyemaildestination");
            entity.Property(e => e.Twofactorprovider).HasColumnName("twofactorprovider");
            entity.Property(e => e.Twofactorproviderauthstring).HasColumnName("twofactorproviderauthstring");
            entity.Property(e => e.Twofactorprovidertoken).HasColumnName("twofactorprovidertoken");
            entity.Property(e => e.Uidstring).HasColumnName("uidstring");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Usertwofactorenabled).HasColumnName("usertwofactorenabled");
            entity.Property(e => e.Usertwofactorkeysmsdestination).HasColumnName("usertwofactorkeysmsdestination");
            entity.Property(e => e.Usertwofactortype).HasColumnName("usertwofactortype");
        });

        modelBuilder.Entity<Userlog>(entity =>
        {
            entity.ToTable("userlogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Hashedpassword).HasColumnName("hashedpassword");
            entity.Property(e => e.Hashid).HasColumnName("hashid");
            entity.Property(e => e.Loginstatus).HasColumnName("loginstatus");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Userprofile>(entity =>
        {
            entity.ToTable("userprofiles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Activepictureurl).HasColumnName("activepictureurl");
            entity.Property(e => e.Address1).HasColumnName("address1");
            entity.Property(e => e.Address2).HasColumnName("address2");
            entity.Property(e => e.Branchid).HasColumnName("branchid");
            entity.Property(e => e.Buid).HasColumnName("buid");
            entity.Property(e => e.Cellphone).HasColumnName("cellphone");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Companyid).HasColumnName("companyid");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.Defaultinstanceid).HasColumnName("defaultinstanceid");
            entity.Property(e => e.Defaultshardid).HasColumnName("defaultshardid");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Facebookurl).HasColumnName("facebookurl");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Fullname).HasColumnName("fullname");
            entity.Property(e => e.Googleurl).HasColumnName("googleurl");
            entity.Property(e => e.Instagramurl).HasColumnName("instagramurl");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Linkedinurl).HasColumnName("linkedinurl");
            entity.Property(e => e.Managerid).HasColumnName("managerid");
            entity.Property(e => e.Maritalstatus).HasColumnName("maritalstatus");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Postalzip).HasColumnName("postalzip");
            entity.Property(e => e.Pronoun).HasColumnName("pronoun");
            entity.Property(e => e.Regionid).HasColumnName("regionid");
            entity.Property(e => e.Sms).HasColumnName("sms");
            entity.Property(e => e.Stateregion).HasColumnName("stateregion");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Title2).HasColumnName("title2");
            entity.Property(e => e.University).HasColumnName("university");
            entity.Property(e => e.University1).HasColumnName("university1");
            entity.Property(e => e.University2).HasColumnName("university2");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Vimeourl).HasColumnName("vimeourl");
        });

        modelBuilder.Entity<Usersession>(entity =>
        {
            entity.ToTable("usersessions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Acknowledged).HasColumnName("acknowledged");
            entity.Property(e => e.Actionpriority).HasColumnName("actionpriority");
            entity.Property(e => e.Sessioncomplete).HasColumnName("sessioncomplete");
            entity.Property(e => e.Sessiondescription).HasColumnName("sessiondescription");
            entity.Property(e => e.Sessionemail).HasColumnName("sessionemail");
            entity.Property(e => e.Sessionend).HasColumnName("sessionend");
            entity.Property(e => e.Sessionfirstname).HasColumnName("sessionfirstname");
            entity.Property(e => e.Sessionfullname).HasColumnName("sessionfullname");
            entity.Property(e => e.Sessionlastname).HasColumnName("sessionlastname");
            entity.Property(e => e.Sessionrecorded).HasColumnName("sessionrecorded");
            entity.Property(e => e.Sessionrecordurl).HasColumnName("sessionrecordurl");
            entity.Property(e => e.Sessionstart).HasColumnName("sessionstart");
            entity.Property(e => e.Sessionusername).HasColumnName("sessionusername");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Twofactorkey).HasColumnName("twofactorkey");
            entity.Property(e => e.Twofactorkeyemaildestination).HasColumnName("twofactorkeyemaildestination");
            entity.Property(e => e.Twofactorkeysmsdestination).HasColumnName("twofactorkeysmsdestination");
            entity.Property(e => e.Twofactorprovider).HasColumnName("twofactorprovider");
            entity.Property(e => e.Twofactorproviderauthstring).HasColumnName("twofactorproviderauthstring");
            entity.Property(e => e.Twofactorprovidertoken).HasColumnName("twofactorprovidertoken");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
