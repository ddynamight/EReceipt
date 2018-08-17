namespace EReceipt.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EReceiptModel : DbContext
    {
        // Your context has been configured to use a 'EReceiptModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EReceipt.Model.EReceiptModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'EReceiptModel' 
        // connection string in the application configuration file.
        public EReceiptModel()
            : base("name=EReceiptModel")
        {
            base.Configuration.LazyLoadingEnabled = false;
            base.Configuration.ProxyCreationEnabled = false;
            //base.Configuration.AutoDetectChangesEnabled = true;
        }


        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Single Property Configuration and Keys Configuration

            modelBuilder.Entity<aspnet_Users>().HasKey(u => u.UserId);
            modelBuilder.Entity<aspnet_Membership>().HasKey(m => m.UserId);
            modelBuilder.Entity<aspnet_WebEvent_Events>().HasKey(w => w.EventId);
            modelBuilder.Entity<Company>().HasKey(c => c.Id);
            modelBuilder.Entity<Invoice>().HasKey(c => c.Id);
            modelBuilder.Entity<Profile>().HasKey(p => p.UserId);
            modelBuilder.Entity<Profile>().Property(p => p.Image).IsOptional();


            // One to Many Relationships Configurations

            modelBuilder.Entity<aspnet_Users>()
                .HasMany<Company>(u => u.Companies)
                .WithRequired(c => c.aspnet_Users)
                .HasForeignKey(c => c.aspnet_UsersUserId);

            modelBuilder.Entity<Company>()
                .HasMany<Invoice>(c => c.Invoices)
                .WithRequired(i => i.Company)
                .HasForeignKey(i => i.CompanyId);


            // One to Zero or One Relationships Configurations

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(u => u.Profile)
                .WithRequired(p => p.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(u => u.aspnet_Membership)
                .WithRequired(m => m.aspnet_Users);
        }
    }
}