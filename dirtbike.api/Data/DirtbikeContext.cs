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

    public virtual DbSet<Adminlog> Adminlogs { get; set; }

    public virtual DbSet<Apilog> Apilogs { get; set; }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<Batchtype> Batchtypes { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartMaster> CartMasters { get; set; }

    public virtual DbSet<Cartitem> Cartitems { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Learnlog> Learnlogs { get; set; }

    public virtual DbSet<Noctech> Noctechs { get; set; }

    public virtual DbSet<Park> Parks { get; set; }

    public virtual DbSet<ParkCalendar> ParkCalendars { get; set; }

    public virtual DbSet<ParkReview> ParkReviews { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<SalesCatalogue> SalesCatalogues { get; set; }

    public virtual DbSet<SalesSession> SalesSessions { get; set; }

    public virtual DbSet<Sessionlog> Sessionlogs { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<Superuserlog> Superuserlogs { get; set; }

    public virtual DbSet<TaxtableState> TaxtableStates { get; set; }

    public virtual DbSet<TaxtableU> TaxtableUs { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Useraction> Useractions { get; set; }

    public virtual DbSet<Usergroup> Usergroups { get; set; }

    public virtual DbSet<Userhelp> Userhelps { get; set; }

    public virtual DbSet<Userlog> Userlogs { get; set; }

    public virtual DbSet<Usernotice> Usernotices { get; set; }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }

    public virtual DbSet<Usersession> Usersessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=./SQLDATA/dirtbike.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adminlog>(entity =>
        {
            entity.ToTable("adminlogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acknowledged).HasColumnName("acknowledged");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME")
                .HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Managerescid).HasColumnName("managerescid");
            entity.Property(e => e.Techid).HasColumnName("techid");
            entity.Property(e => e.Threatlevel).HasColumnName("threatlevel");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

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

        modelBuilder.Entity<Batch>(entity =>
        {
            entity.ToTable("batches");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batchend)
                .HasColumnType("DATE")
                .HasColumnName("batchend");
            entity.Property(e => e.Batchname).HasColumnName("batchname");
            entity.Property(e => e.Batchstart)
                .HasColumnType("DATE")
                .HasColumnName("batchstart");
            entity.Property(e => e.Batchstatus).HasColumnName("batchstatus");
            entity.Property(e => e.Batchtype).HasColumnName("batchtype");
            entity.Property(e => e.Filelocationpath).HasColumnName("filelocationpath");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Qtyactual).HasColumnName("qtyactual");
            entity.Property(e => e.Qtyend).HasColumnName("qtyend");
            entity.Property(e => e.Qtyerror).HasColumnName("qtyerror");
            entity.Property(e => e.Qtyexpected).HasColumnName("qtyexpected");
            entity.Property(e => e.Qtystart).HasColumnName("qtystart");
        });

        modelBuilder.Entity<Batchtype>(entity =>
        {
            entity.ToTable("batchtype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batchtypename).HasColumnName("batchtypename");
            entity.Property(e => e.Instance).HasColumnName("instance");
            entity.Property(e => e.Region).HasColumnName("region");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Adults)
                .HasColumnType("INT")
                .HasColumnName("adults");
            entity.Property(e => e.Cancellationrefund)
                .HasColumnType("float")
                .HasColumnName("cancellationrefund");
            entity.Property(e => e.CartDetailsJson).HasColumnName("cartDetailsJson");
            entity.Property(e => e.Children)
                .HasColumnType("INT")
                .HasColumnName("children");
            entity.Property(e => e.ParkId).HasColumnName("ParkID");
            entity.Property(e => e.ResEnd)
                .HasColumnType("date")
                .HasColumnName("resEnd");
            entity.Property(e => e.ResStart)
                .HasColumnType("date")
                .HasColumnName("resStart");
            entity.Property(e => e.Reservationstatus).HasColumnName("reservationstatus");
            entity.Property(e => e.Reservationtype).HasColumnName("reservationtype");
            entity.Property(e => e.Reversetransactionid).HasColumnName("reversetransactionid");
            entity.Property(e => e.Tentsites)
                .HasColumnType("INT")
                .HasColumnName("tentsites");
            entity.Property(e => e.Totalcartitems)
                .HasColumnType("INT")
                .HasColumnName("totalcartitems");
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.ToTable("Card");

            entity.HasIndex(e => new { e.CardLast4, e.CardExpDate }, "IX_Card_CardLast4_CardExpDate").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("CardID");
            entity.Property(e => e.Cardbtn).HasColumnName("cardbtn");
            entity.Property(e => e.Fullname).HasColumnName("fullname");
            entity.Property(e => e.IsActive).HasDefaultValue(1);
            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.Adults)
                .HasColumnType("INT")
                .HasColumnName("adults");
            entity.Property(e => e.Bookinginfo).HasColumnName("bookinginfo");
            entity.Property(e => e.CartId).HasColumnName("cartId");
            entity.Property(e => e.Children)
                .HasColumnType("INT")
                .HasColumnName("children");
            entity.Property(e => e.DateAdded).HasColumnName("dateAdded");
            entity.Property(e => e.IsCheckedOut).HasColumnName("isCheckedOut");
            entity.Property(e => e.ItemDescription).HasColumnName("itemDescription");
            entity.Property(e => e.ItemType).HasColumnName("itemType");
            entity.Property(e => e.Johnstotals).HasColumnName("johnstotals");
            entity.Property(e => e.Multipleitems).HasColumnName("multipleitems");
            entity.Property(e => e.ParkId).HasColumnName("parkId");
            entity.Property(e => e.Parkname).HasColumnName("parkname");
            entity.Property(e => e.Paymentid).HasColumnName("paymentid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ResEnd)
                .HasColumnType("date")
                .HasColumnName("resEnd");
            entity.Property(e => e.ResStart)
                .HasColumnType("date")
                .HasColumnName("resStart");
            entity.Property(e => e.Tentsites)
                .HasColumnType("INT")
                .HasColumnName("tentsites");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");
            entity.Property(e => e.Totalcartitems).HasColumnName("totalcartitems");
            entity.Property(e => e.Transactiontotal).HasColumnName("transactiontotal");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.UnitPrice).HasColumnName("unitPrice");
        });

        modelBuilder.Entity<CartMaster>(entity =>
        {
            entity.ToTable("CartMaster");

            entity.Property(e => e.CartsActive).HasDefaultValue(0);
            entity.Property(e => e.CartsActiveList).HasDefaultValue("{}");
            entity.Property(e => e.CartsCancelled).HasDefaultValue(0);
            entity.Property(e => e.CartsCount).HasDefaultValue(0);
            entity.Property(e => e.Loyaltyid).HasColumnName("loyaltyid");
            entity.Property(e => e.Loyaltyvendor).HasColumnName("loyaltyvendor");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Cartitem>(entity =>
        {
            entity.ToTable("CARTITEM");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adults)
                .HasColumnType("INT")
                .HasColumnName("adults");
            entity.Property(e => e.Cartid).HasColumnName("cartid");
            entity.Property(e => e.Cartitemdate)
                .HasColumnType("DATETIME")
                .HasColumnName("cartitemdate");
            entity.Property(e => e.Children)
                .HasColumnType("INT")
                .HasColumnName("children");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("DATE")
                .HasColumnName("created_date");
            entity.Property(e => e.Itemdescription).HasColumnName("itemdescription");
            entity.Property(e => e.Itemextendedprice).HasColumnName("itemextendedprice");
            entity.Property(e => e.Itemqty).HasColumnName("itemqty");
            entity.Property(e => e.Itemsubtotal)
                .HasColumnType("float")
                .HasColumnName("itemsubtotal");
            entity.Property(e => e.Itemtotals).HasColumnName("itemtotals");
            entity.Property(e => e.Itemvendor).HasColumnName("itemvendor");
            entity.Property(e => e.Memberid).HasColumnName("memberid");
            entity.Property(e => e.Parkid).HasColumnName("parkid");
            entity.Property(e => e.Parkname).HasColumnName("parkname");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Qrcodeurl).HasColumnName("qrcodeurl");
            entity.Property(e => e.ResEnd)
                .HasColumnType("DATE")
                .HasColumnName("res_end");
            entity.Property(e => e.ResStart)
                .HasColumnType("DATE")
                .HasColumnName("res_start");
            entity.Property(e => e.Reservationcode).HasColumnName("reservationcode");
            entity.Property(e => e.Rewardsprovider).HasColumnName("rewardsprovider");
            entity.Property(e => e.Salescatid).HasColumnName("salescatid");
            entity.Property(e => e.Shopid).HasColumnName("shopid");
            entity.Property(e => e.Statetaxauth).HasColumnName("statetaxauth");
            entity.Property(e => e.Statetaxpercent)
                .HasColumnType("float")
                .HasColumnName("statetaxpercent");
            entity.Property(e => e.Statetaxtotal)
                .HasColumnType("float")
                .HasColumnName("statetaxtotal");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");
            entity.Property(e => e.Userid)
                .HasColumnType("INT")
                .HasColumnName("userid");
            entity.Property(e => e.Ustaxpercent)
                .HasColumnType("float")
                .HasColumnName("ustaxpercent");
            entity.Property(e => e.Ustaxtotal)
                .HasColumnType("float")
                .HasColumnName("ustaxtotal");
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

        modelBuilder.Entity<Learnlog>(entity =>
        {
            entity.ToTable("learnlog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cataloguesku).HasColumnName("cataloguesku");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Certauthority).HasColumnName("certauthority");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Emplid).HasColumnName("emplid");
            entity.Property(e => e.Employee).HasColumnName("employee");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Employeeidasint).HasColumnName("employeeidasint");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Fullname).HasColumnName("fullname");
            entity.Property(e => e.Learningmodulesid).HasColumnName("learningmodulesid");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Noctech>(entity =>
        {
            entity.ToTable("noctechs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Sms)
                .HasDefaultValue(1)
                .HasColumnName("sms");
            entity.Property(e => e.Techaddress1).HasColumnName("techaddress1");
            entity.Property(e => e.Techaddress2).HasColumnName("techaddress2");
            entity.Property(e => e.Techcity).HasColumnName("techcity");
            entity.Property(e => e.Techstate).HasColumnName("techstate");
            entity.Property(e => e.Techzip).HasColumnName("techzip");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Park>(entity =>
        {
            entity.Property(e => e.ParkId).HasColumnName("ParkID");
            entity.Property(e => e.Cabins)
                .HasColumnType("INT")
                .HasColumnName("cabins");
            entity.Property(e => e.Camping)
                .HasColumnType("INT")
                .HasColumnName("camping");
            entity.Property(e => e.Canoeing)
                .HasColumnType("INT")
                .HasColumnName("canoeing");
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
            entity.Property(e => e.Frisbee)
                .HasColumnType("INT")
                .HasColumnName("frisbee");
            entity.Property(e => e.Hqbranchid).HasColumnName("hqbranchid");
            entity.Property(e => e.Iscanadian)
                .HasColumnType("INT")
                .HasColumnName("iscanadian");
            entity.Property(e => e.Ismexican)
                .HasColumnType("INT")
                .HasColumnName("ismexican");
            entity.Property(e => e.Isnationalpark).HasColumnName("isnationalpark");
            entity.Property(e => e.Isstatepark).HasColumnName("isstatepark");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Maxcampsites)
                .HasColumnType("INT")
                .HasColumnName("maxcampsites");
            entity.Property(e => e.Maxvisitors)
                .HasColumnType("INT")
                .HasColumnName("maxvisitors");
            entity.Property(e => e.Motocross)
                .HasColumnType("INT")
                .HasColumnName("motocross");
            entity.Property(e => e.Mountainbikes)
                .HasColumnType("INT")
                .HasColumnName("mountainbikes");
            entity.Property(e => e.Parklogourl).HasColumnType("string");
            entity.Property(e => e.Pic1url).HasColumnName("pic1url");
            entity.Property(e => e.Pic2url).HasColumnName("pic2url");
            entity.Property(e => e.Pic3url).HasColumnName("pic3url");
            entity.Property(e => e.Pic4url).HasColumnName("pic4url");
            entity.Property(e => e.Pic5url).HasColumnName("pic5url");
            entity.Property(e => e.Pic6url).HasColumnName("pic6url");
            entity.Property(e => e.Pic7url).HasColumnName("pic7url");
            entity.Property(e => e.Pic8url).HasColumnName("pic8url");
            entity.Property(e => e.Pic9url).HasColumnName("pic9url");
            entity.Property(e => e.Rafting)
                .HasColumnType("INT")
                .HasColumnName("rafting");
            entity.Property(e => e.Skiing)
                .HasColumnType("INT")
                .HasColumnName("skiing");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Tents)
                .HasColumnType("INT")
                .HasColumnName("tents");
            entity.Property(e => e.Trailmapurl)
                .HasColumnType("string")
                .HasColumnName("trailmapurl");
        });

        modelBuilder.Entity<ParkCalendar>(entity =>
        {
            entity.ToTable("ParkCalendar");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.EndDate).HasColumnType("DATE");
            entity.Property(e => e.QtyAdults).HasDefaultValue(0);
            entity.Property(e => e.QtyChildren).HasDefaultValue(0);
            entity.Property(e => e.StartDate).HasColumnType("DATE");
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
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
            entity.Property(e => e.Transtype).HasColumnName("transtype");
        });

        modelBuilder.Entity<SalesCatalogue>(entity =>
        {
            entity.ToTable("SalesCatalogue");

            entity.Property(e => e.SalesCatalogueId).HasColumnName("SalesCatalogueID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Global).HasColumnType("INT");
            entity.Property(e => e.IsActive).HasDefaultValue(1);
            entity.Property(e => e.National).HasColumnType("INT");
            entity.Property(e => e.ParkId).HasColumnName("ParkID");
            entity.Property(e => e.Productclass).HasColumnName("productclass");
            entity.Property(e => e.Qtyadults)
                .HasColumnType("INT")
                .HasColumnName("qtyadults");
            entity.Property(e => e.Qtychildren)
                .HasColumnType("INT")
                .HasColumnName("qtychildren");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.State).HasColumnType("INT");
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

        modelBuilder.Entity<Site>(entity =>
        {
            entity.ToTable("site");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1).HasColumnName("address1");
            entity.Property(e => e.Address2).HasColumnName("address2");
            entity.Property(e => e.Branchid).HasColumnName("branchid");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Fax).HasColumnName("fax");
            entity.Property(e => e.Instanceid).HasColumnName("instanceid");
            entity.Property(e => e.Parkid).HasColumnName("parkid");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Postalzip).HasColumnName("postalzip");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<Superuserlog>(entity =>
        {
            entity.ToTable("superuserlogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acknowledged).HasColumnName("acknowledged");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME")
                .HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Managerescid).HasColumnName("managerescid");
            entity.Property(e => e.Techid).HasColumnName("techid");
            entity.Property(e => e.Threatlevel).HasColumnName("threatlevel");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<TaxtableState>(entity =>
        {
            entity.ToTable("TaxtableState");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CorporateTaxRate).HasColumnName("corporate_tax_rate");
            entity.Property(e => e.IndividualTaxRate).HasColumnName("individual_tax_rate");
            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<TaxtableU>(entity =>
        {
            entity.ToTable("TaxtableUS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uscorporateratelow).HasColumnName("uscorporateratelow");
            entity.Property(e => e.Uscorporateratemiddle).HasColumnName("uscorporateratemiddle");
            entity.Property(e => e.Uscorporateratetop).HasColumnName("uscorporateratetop");
            entity.Property(e => e.Uspersonalhigh).HasColumnName("uspersonalhigh");
            entity.Property(e => e.Uspersonallow).HasColumnName("uspersonallow");
            entity.Property(e => e.Uspersonalmiddle).HasColumnName("uspersonalmiddle");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.ToTable("Template");

            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accountactiondate).HasColumnName("accountactiondate");
            entity.Property(e => e.Accountactiondescription).HasColumnName("accountactiondescription");
            entity.Property(e => e.Accountstatus).HasColumnName("accountstatus");
            entity.Property(e => e.Activepictureurl).HasColumnName("activepictureurl");
            entity.Property(e => e.Activeprofileurl).HasColumnName("activeprofileurl");
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
            entity.Property(e => e.Resettokenexpiration)
                .HasColumnType("datetime")
                .HasColumnName("resettokenexpiration");
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

        modelBuilder.Entity<Useraction>(entity =>
        {
            entity.ToTable("useraction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acknowledged).HasColumnName("acknowledged");
            entity.Property(e => e.Actiondate).HasColumnName("actiondate");
            entity.Property(e => e.Actionpriority).HasColumnName("actionpriority");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Usergroup>(entity =>
        {
            entity.ToTable("usergroup");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Groupcompanyid).HasColumnName("groupcompanyid");
            entity.Property(e => e.Groupdescription).HasColumnName("groupdescription");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Groupownerid).HasColumnName("groupownerid");
        });

        modelBuilder.Entity<Userhelp>(entity =>
        {
            entity.ToTable("userhelp");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bestcontactnumber).HasColumnName("bestcontactnumber");
            entity.Property(e => e.Descr).HasColumnName("descr");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Emplid).HasColumnName("emplid");
            entity.Property(e => e.Fullname).HasColumnName("fullname");
            entity.Property(e => e.Replied).HasColumnName("replied");
            entity.Property(e => e.Repliedmanageremail).HasColumnName("repliedmanageremail");
            entity.Property(e => e.Repliedmanagerid).HasColumnName("repliedmanagerid");
            entity.Property(e => e.Repliedmanagerphone).HasColumnName("repliedmanagerphone");
            entity.Property(e => e.Responsedate)
                .HasColumnType("DATE")
                .HasColumnName("responsedate");
            entity.Property(e => e.Severity).HasColumnName("severity");
            entity.Property(e => e.Ticketdate)
                .HasColumnType("DATE")
                .HasColumnName("ticketdate");
            entity.Property(e => e.Ticketid).HasColumnName("ticketid");
            entity.Property(e => e.Userid).HasColumnName("userid");
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

        modelBuilder.Entity<Usernotice>(entity =>
        {
            entity.ToTable("usernotices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.NoticeDatetime)
                .HasDefaultValueSql("DATETIME('now')")
                .HasColumnType("DATETIME")
                .HasColumnName("notice_datetime");
            entity.Property(e => e.Userid)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("userid");
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
