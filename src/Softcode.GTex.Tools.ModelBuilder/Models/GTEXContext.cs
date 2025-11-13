using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Softcode.GTex.Data.Models
{
    public partial class GTEXContext : DbContext
    {
        public GTEXContext()
        {
        }

        public GTEXContext(DbContextOptions<GTEXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessLog> AccessLog { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
        public virtual DbSet<ApplicationMenu> ApplicationMenu { get; set; }
        public virtual DbSet<ApplicationPage> ApplicationPage { get; set; }
        public virtual DbSet<ApplicationPageFieldDetail> ApplicationPageFieldDetail { get; set; }
        public virtual DbSet<ApplicationPageNavigation> ApplicationPageNavigation { get; set; }
        public virtual DbSet<ApplicationPageService> ApplicationPageService { get; set; }
        public virtual DbSet<ApplicationRoleRight> ApplicationRoleRight { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<BusinessCategory> BusinessCategory { get; set; }
        public virtual DbSet<BusinessCategoryMapType> BusinessCategoryMapType { get; set; }
        public virtual DbSet<BusinessCategoryMapTypeOption> BusinessCategoryMapTypeOption { get; set; }
        public virtual DbSet<BusinessCategoryType> BusinessCategoryType { get; set; }
        public virtual DbSet<BusinessProfile> BusinessProfile { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Communication> Communication { get; set; }
        public virtual DbSet<CommunicationFileStore> CommunicationFileStore { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyOperatingState> CompanyOperatingState { get; set; }
        public virtual DbSet<CompanyRelationshipType> CompanyRelationshipType { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactBusinessProfile> ContactBusinessProfile { get; set; }
        public virtual DbSet<ContactOperatingCity> ContactOperatingCity { get; set; }
        public virtual DbSet<ContactSpecialisation> ContactSpecialisation { get; set; }
        public virtual DbSet<Counter> Counter { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<CustomCategory> CustomCategory { get; set; }
        public virtual DbSet<CustomCategoryMapType> CustomCategoryMapType { get; set; }
        public virtual DbSet<CustomCategoryMapTypeOption> CustomCategoryMapTypeOption { get; set; }
        public virtual DbSet<CustomCategoryType> CustomCategoryType { get; set; }
        public virtual DbSet<DocumentFileStore> DocumentFileStore { get; set; }
        public virtual DbSet<DocumentMetadata> DocumentMetadata { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EntityContact> EntityContact { get; set; }
        public virtual DbSet<EntityLayoutTemplate> EntityLayoutTemplate { get; set; }
        public virtual DbSet<EntityType> EntityType { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<FileStoreSetting> FileStoreSetting { get; set; }
        public virtual DbSet<Hash> Hash { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobParameter> JobParameter { get; set; }
        public virtual DbSet<JobQueue> JobQueue { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<PersistedGrants> PersistedGrants { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<PostalCode> PostalCode { get; set; }
        public virtual DbSet<PublicHoliday> PublicHoliday { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<SecurityConfiguration> SecurityConfiguration { get; set; }
        public virtual DbSet<SecurityProfile> SecurityProfile { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Server1> Server1 { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<State1> State1 { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<SystemEntity> SystemEntity { get; set; }
        public virtual DbSet<SystemEntityRight> SystemEntityRight { get; set; }
        public virtual DbSet<SystemEntityRightDependency> SystemEntityRightDependency { get; set; }
        public virtual DbSet<SystemModule> SystemModule { get; set; }
        public virtual DbSet<TimeZone> TimeZone { get; set; }
        public virtual DbSet<VoucherType> VoucherType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=180.92.239.110;initial catalog=GTEX;user id=sa;password=sa@#$1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessLog>(entity =>
            {
                entity.ToTable("AccessLog", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.LogDateTime).HasColumnType("datetime");

                entity.Property(e => e.LogDescription).IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.Property(e => e.Token).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.AccessLogCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccessLog_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.AccessLogLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_AccessLog_Contact_UpdatedBy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AccessLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => new { e.Value, e.Key })
                    .HasName("UX_HangFire_CounterAggregated_Key")
                    .IsUnique();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ApplicationMenu>(entity =>
            {
                entity.ToTable("ApplicationMenu", "core");

                entity.Property(e => e.Caption)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HelpText)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ImageSource)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NavigateUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.ApplicationMenuCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationMenu_Contact_CreatedBy");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.ApplicationMenu)
                    .HasForeignKey(d => d.EntityId);

                entity.HasOne(d => d.EntityRight)
                    .WithMany(p => p.ApplicationMenu)
                    .HasForeignKey(d => d.EntityRightId)
                    .HasConstraintName("FK_ApplicationMenu_SystemEntityRight_RightId");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.ApplicationMenuLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_ApplicationMenu_Contact_UpdatedBy");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.ApplicationMenu)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("FK_ApplicationMenu_ApplicationPage");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ApplicationMenu_ApplicationMenu_Parent");
            });

            modelBuilder.Entity<ApplicationPage>(entity =>
            {
                entity.ToTable("ApplicationPage", "core");

                entity.HasIndex(e => e.Name)
                    .HasName("UK_ApplicationPage_Name")
                    .IsUnique();

                entity.HasIndex(e => e.RoutingUrl)
                    .HasName("UK_ApplicationPage_RoutingUrl")
                    .IsUnique();

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PageType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoutingUrl)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.ApplicationPageCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPage_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.ApplicationPageLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_ApplicationPage_Contact_UpdatedBy");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ApplicationPage_ApplicationPage");
            });

            modelBuilder.Entity<ApplicationPageFieldDetail>(entity =>
            {
                entity.ToTable("ApplicationPageFieldDetail", "core");

                entity.Property(e => e.Alignment)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Left')");

                entity.Property(e => e.Caption)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CellTemplate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ColumnFilterEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultSortOrder)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('undefined')");

                entity.Property(e => e.Format)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReadOnly)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RowFilterEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SortEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.ApplicationPageFieldDetailCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPageFieldDetail_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.ApplicationPageFieldDetailLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_ApplicationPageFieldDetail_Contact_UpdatedBy");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.ApplicationPageFieldDetail)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPageFieldDetail_ApplicationPage");
            });

            modelBuilder.Entity<ApplicationPageNavigation>(entity =>
            {
                entity.ToTable("ApplicationPageNavigation", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.LinkName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NavigateUrl)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.ApplicationPageNavigationCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPageNavigation_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.ApplicationPageNavigationLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_ApplicationPageNavigation_Contact_UpdatedBy");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.ApplicationPageNavigation)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPageNavigation_ApplicationPage");
            });

            modelBuilder.Entity<ApplicationPageService>(entity =>
            {
                entity.ToTable("ApplicationPageService", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceUrl)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.ApplicationPageServiceCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPageService_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.ApplicationPageServiceLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_ApplicationPageService_Contact_UpdatedBy");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.ApplicationPageService)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationPageService_ApplicationPage");
            });

            modelBuilder.Entity<ApplicationRoleRight>(entity =>
            {
                entity.ToTable("ApplicationRoleRight", "security");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.ApplicationRoleRight)
                    .HasForeignKey(d => d.RightId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ApplicationRoleRight)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationRoleRight_ApplicationRoleRight_RoleId");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.ToTable("AspNetRoleClaims", "security");

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.ToTable("AspNetRoles", "security");

                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.ParentRoleId).HasMaxLength(450);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.AspNetRoles)
                    .HasForeignKey(d => d.BusinessProfileId);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.AspNetRolesCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetRoles_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.AspNetRolesLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_AspNetRoles_Contact_UpdatedBy");

                entity.HasOne(d => d.ParentRole)
                    .WithMany(p => p.InverseParentRole)
                    .HasForeignKey(d => d.ParentRoleId)
                    .HasConstraintName("FK_AspNetRoles_AspNetRoles_ParentId");
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.ToTable("AspNetUserClaims", "security");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("AspNetUserLogins", "security");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("AspNetUserRoles", "security");

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.ToTable("AspNetUsers", "security");

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.EnableWcag).HasColumnName("EnableWCAG");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Sid)
                    .HasColumnName("SID")
                    .HasMaxLength(400);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.AspNetUsersContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.AspNetUsersCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUsers_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.AspNetUsersLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_AspNetUsers_Contact_UpdatedBy");

                entity.HasOne(d => d.SecurityProfile)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.SecurityProfileId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("AspNetUserTokens", "security");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BusinessCategory>(entity =>
            {
                entity.ToTable("BusinessCategory", "herb");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessCategoryMapTypeOption)
                    .WithMany(p => p.BusinessCategory)
                    .HasForeignKey(d => d.BusinessCategoryMapTypeOptionId);

                entity.HasOne(d => d.BusinessCategoryType)
                    .WithMany(p => p.BusinessCategory)
                    .HasForeignKey(d => d.BusinessCategoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BusinessCategoryMapType>(entity =>
            {
                entity.ToTable("BusinessCategoryMapType", "herb");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BusinessCategoryMapTypeOption>(entity =>
            {
                entity.ToTable("BusinessCategoryMapTypeOption", "herb");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessCategoryMapType)
                    .WithMany(p => p.BusinessCategoryMapTypeOption)
                    .HasForeignKey(d => d.BusinessCategoryMapTypeId);
            });

            modelBuilder.Entity<BusinessCategoryType>(entity =>
            {
                entity.ToTable("BusinessCategoryType", "herb");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoutingKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessCategoryMapType)
                    .WithMany(p => p.BusinessCategoryType)
                    .HasForeignKey(d => d.BusinessCategoryMapTypeId);
            });

            modelBuilder.Entity<BusinessProfile>(entity =>
            {
                entity.ToTable("BusinessProfile", "core");

                entity.Property(e => e.Abn)
                    .HasColumnName("ABN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Acn)
                    .HasColumnName("ACN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationUrl)
                    .HasColumnName("ApplicationURL")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.Property(e => e.TimeZoneId)
                    .HasColumnName("TimeZoneID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UseSameConfigForPayrollIntegration)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.BusinessProfileCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusinessProfile_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.BusinessProfileLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_BusinessProfile_Contact_UpdatedBy");

                entity.HasOne(d => d.Logo)
                    .WithMany(p => p.BusinessProfile)
                    .HasForeignKey(d => d.LogoId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "store");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Communication>(entity =>
            {
                entity.ToTable("Communication", "service");

                entity.Property(e => e.CommunicationDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FollowupDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.ReminderDateTime).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.Communication)
                    .HasForeignKey(d => d.BusinessProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.CommunicationCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Communication_Contact_CreatedBy");

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.Communication)
                    .HasForeignKey(d => d.EntityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.FollowupByContact)
                    .WithMany(p => p.CommunicationFollowupByContact)
                    .HasForeignKey(d => d.FollowupByContactId);

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.CommunicationLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Communication_Contact_UpdatedBy");

                entity.HasOne(d => d.MethodType)
                    .WithMany(p => p.CommunicationMethodType)
                    .HasForeignKey(d => d.MethodTypeId);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CommunicationStatus)
                    .HasForeignKey(d => d.StatusId);
            });

            modelBuilder.Entity<CommunicationFileStore>(entity =>
            {
                entity.ToTable("CommunicationFileStore", "service");

                entity.HasOne(d => d.Communication)
                    .WithMany(p => p.CommunicationFileStore)
                    .HasForeignKey(d => d.CommunicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.DocumentFileStore)
                    .WithMany(p => p.CommunicationFileStore)
                    .HasForeignKey(d => d.DocumentFileStoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company", "crm");

                entity.Property(e => e.Abn)
                    .HasColumnName("ABN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Acn)
                    .HasColumnName("ACN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AnnualTurnover).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalPartnerId)
                    .HasColumnName("ExternalPartnerID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MainPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.Property(e => e.TimeZoneId)
                    .HasColumnName("TimeZoneID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TradeAs)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.BusinessProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_BusinessProfile");

                entity.HasOne(d => d.CompanyType)
                    .WithMany(p => p.CompanyCompanyType)
                    .HasForeignKey(d => d.CompanyTypeId)
                    .HasConstraintName("FK_Company_CustomCategory_CompanyType");

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.CompanyCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Contact_CreatedBy");

                entity.HasOne(d => d.IndustryType)
                    .WithMany(p => p.CompanyIndustryType)
                    .HasForeignKey(d => d.IndustryTypeId)
                    .HasConstraintName("FK_Company_CustomCategory_IndustryType");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.CompanyLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Company_Contact_UpdatedBy");

                entity.HasOne(d => d.Logo)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.LogoId)
                    .HasConstraintName("FK_Company_Photo");

                entity.HasOne(d => d.PreferredContactMethod)
                    .WithMany(p => p.CompanyPreferredContactMethod)
                    .HasForeignKey(d => d.PreferredContactMethodId)
                    .HasConstraintName("FK_Company_CustomCategory_PreferredContactMethod");

                entity.HasOne(d => d.PrimaryContact)
                    .WithMany(p => p.CompanyPrimaryContact)
                    .HasForeignKey(d => d.PrimaryContactId);

                entity.HasOne(d => d.RatingType)
                    .WithMany(p => p.CompanyRatingType)
                    .HasForeignKey(d => d.RatingTypeId)
                    .HasConstraintName("FK_Company_CustomCategory_Rating");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Company_Region");
            });

            modelBuilder.Entity<CompanyOperatingState>(entity =>
            {
                entity.ToTable("CompanyOperatingState", "crm");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyOperatingState)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyOperatingCountry_Company");

                entity.HasOne(d => d.OperatingState)
                    .WithMany(p => p.CompanyOperatingState)
                    .HasForeignKey(d => d.OperatingStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CompanyRelationshipType>(entity =>
            {
                entity.ToTable("CompanyRelationshipType", "crm");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyRelationshipType)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyRelationshipType_Company");

                entity.HasOne(d => d.RelationshipType)
                    .WithMany(p => p.CompanyRelationshipType)
                    .HasForeignKey(d => d.RelationshipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyRelationshipType_CustomCategory");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact", "core");

                entity.Property(e => e.BusinessPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessPhoneExt)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyContactEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyContactRelation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ImLoginId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastExportedDateTime).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PreferredName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialInstruction).HasMaxLength(200);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.Property(e => e.TimeZoneId).HasMaxLength(200);

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.InverseCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_Contact_CreatedBy");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.ContactGender)
                    .HasForeignKey(d => d.GenderId);

                entity.HasOne(d => d.IdentityLicense)
                    .WithMany(p => p.ContactIdentityLicense)
                    .HasForeignKey(d => d.IdentityLicenseId);

                entity.HasOne(d => d.ImType)
                    .WithMany(p => p.ContactImType)
                    .HasForeignKey(d => d.ImTypeId);

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.InverseLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Contact_Contact_UpdatedBy");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.PhotoId);

                entity.HasOne(d => d.Postion)
                    .WithMany(p => p.ContactPostion)
                    .HasForeignKey(d => d.PostionId);

                entity.HasOne(d => d.TimeZone)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.TimeZoneId)
                    .HasConstraintName("FK_Contact_TimeZone_TimezoneId");
            });

            modelBuilder.Entity<ContactBusinessProfile>(entity =>
            {
                entity.ToTable("ContactBusinessProfile", "core");

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.ContactBusinessProfile)
                    .HasForeignKey(d => d.BusinessProfileId);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactBusinessProfile)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_ContactBusinessProfile_ContactBusinessProfile_ContactId");

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.ContactBusinessProfile)
                    .HasForeignKey(d => d.EntityTypeId);
            });

            modelBuilder.Entity<ContactOperatingCity>(entity =>
            {
                entity.ToTable("ContactOperatingCity", "core");

                entity.HasIndex(e => new { e.ContactId, e.OperatingCity })
                    .HasName("UN_ContactOperatingCity")
                    .IsUnique();

                entity.Property(e => e.OperatingCity)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactOperatingCity)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ContactSpecialisation>(entity =>
            {
                entity.ToTable("ContactSpecialisation", "core");

                entity.Property(e => e.OtherSpecialisation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactSpecialisation)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Specialisation)
                    .WithMany(p => p.ContactSpecialisation)
                    .HasForeignKey(d => d.SpecialisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => new { e.Value, e.Key })
                    .HasName("IX_HangFire_Counter_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "core");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.CountryCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Country_Contact_CreatedBy");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Country)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Country_Currency");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.CountryLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Country_Contact_UpdatedBy");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Isocode)
                    .IsRequired()
                    .HasColumnName("ISOCode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Precision).HasDefaultValueSql("((2))");

                entity.Property(e => e.Symbol).HasMaxLength(10);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.CurrencyCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Currency_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.CurrencyLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Currency_Contact_UpdatedBy");
            });

            modelBuilder.Entity<CustomCategory>(entity =>
            {
                entity.ToTable("CustomCategory", "core");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_CustomCategory");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Desciption)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.CustomCategory)
                    .HasForeignKey(d => d.BusinessProfileId)
                    .HasConstraintName("FK_CustomCategory_BusinessProfile");

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.CustomCategoryCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CustomCategoryMapTypeOption)
                    .WithMany(p => p.CustomCategory)
                    .HasForeignKey(d => d.CustomCategoryMapTypeOptionId)
                    .HasConstraintName("FK_CustomCategory_CustomCategoryMapTypeOption_MapTypeOptionId");

                entity.HasOne(d => d.CustomCategoryType)
                    .WithMany(p => p.CustomCategory)
                    .HasForeignKey(d => d.CustomCategoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.CustomCategoryLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId);
            });

            modelBuilder.Entity<CustomCategoryMapType>(entity =>
            {
                entity.ToTable("CustomCategoryMapType", "core");

                entity.HasIndex(e => e.Name)
                    .HasName("UK_CustomCategoryMapType")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomCategoryMapTypeOption>(entity =>
            {
                entity.ToTable("CustomCategoryMapTypeOption", "core");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CustomCategoryMapType)
                    .WithMany(p => p.CustomCategoryMapTypeOption)
                    .HasForeignKey(d => d.CustomCategoryMapTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomCategoryType>(entity =>
            {
                entity.ToTable("CustomCategoryType", "core");

                entity.HasIndex(e => e.RoutingKey)
                    .HasName("UK_CustomCategoryType_RoutingKey")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.HelpText).IsUnicode(false);

                entity.Property(e => e.ImageSource)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoutingKey)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CustomCategoryMapType)
                    .WithMany(p => p.CustomCategoryType)
                    .HasForeignKey(d => d.CustomCategoryMapTypeId)
                    .HasConstraintName("FK_CustomCategoryType_MapType_CustomCategoryMapTypeId");

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.CustomCategoryType)
                    .HasForeignKey(d => d.RightId);
            });

            modelBuilder.Entity<DocumentFileStore>(entity =>
            {
                entity.ToTable("DocumentFileStore", "dms");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.FileSizeInKb).HasColumnName("FileSizeInKB");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MimeType)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrginalFileName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.DocumentFileStoreCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentFileStore_Contact_CreatedBy");

                entity.HasOne(d => d.DocumentMetadata)
                    .WithMany(p => p.DocumentFileStore)
                    .HasForeignKey(d => d.DocumentMetadataId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.DocumentFileStoreLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_DocumentFileStore_Contact_UpdatedBy");
            });

            modelBuilder.Entity<DocumentMetadata>(entity =>
            {
                entity.ToTable("DocumentMetadata", "dms");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.DocumentMetadata)
                    .HasForeignKey(d => d.BusinessProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.DocumentMetadataCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentMetadata_Contact_CreatedBy");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.DocumentMetadata)
                    .HasForeignKey(d => d.DocumentTypeId);

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.DocumentMetadata)
                    .HasForeignKey(d => d.EntityTypeId);

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.DocumentMetadataLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_DocumentMetadata_Contact_UpdatedBy");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "hrm");

                entity.Property(e => e.BirthRegistrationNo).HasMaxLength(100);

                entity.Property(e => e.CardId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmationDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DrivingLicenseNo).HasMaxLength(100);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FatherNameBangla)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.JoiningDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MotherName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MotherNameBangla)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NationalIdNo).HasMaxLength(20);

                entity.Property(e => e.PassportNo).HasMaxLength(100);

                entity.Property(e => e.SpouseName).HasMaxLength(100);

                entity.Property(e => e.SpouseNameBangla).HasMaxLength(100);

                entity.Property(e => e.TaxIdentificationNo).HasMaxLength(100);

                entity.Property(e => e.TerminationDate).HasColumnType("datetime");

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.EmployeeBloodGroup)
                    .HasForeignKey(d => d.BloodGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.EmployeeContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.EmployeeCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_CustomCategory_CreatedById");

                entity.HasOne(d => d.EmpStatus)
                    .WithMany(p => p.EmployeeEmpStatus)
                    .HasForeignKey(d => d.EmpStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_CustomCategory_EmployeeStatusId");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.EmployeeLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Employee_CustomCategory_UpdatedById");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.EmployeeNationality)
                    .HasForeignKey(d => d.NationalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Religion)
                    .WithMany(p => p.EmployeeReligion)
                    .HasForeignKey(d => d.ReligionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TerminationType)
                    .WithMany(p => p.EmployeeTerminationType)
                    .HasForeignKey(d => d.TerminationTypeId)
                    .HasConstraintName("FK_Employee_CustomCategory_TeminationTypeId");
            });

            modelBuilder.Entity<EntityContact>(entity =>
            {
                entity.ToTable("EntityContact", "core");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.EntityContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.EntityContact)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntityContact_Company_CompanyId");

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.EntityContact)
                    .HasForeignKey(d => d.EntityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<EntityLayoutTemplate>(entity =>
            {
                entity.ToTable("EntityLayoutTemplate", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.EntityLayoutTemplate)
                    .HasForeignKey(d => d.BusinessProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntityLayoutTemplate_BusinessProfile");

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.EntityLayoutTemplateCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntityLayoutTemplate_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.EntityLayoutTemplateLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_EntityLayoutTemplate_Contact_UpdatedBy");
            });

            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.ToTable("EntityType", "core");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLog", "core");

                entity.Property(e => e.ApplicationVersion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DomainName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorDateTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MachineUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.Osversion)
                    .HasColumnName("OSVersion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Resolution).IsUnicode(false);

                entity.Property(e => e.Source).IsUnicode(false);

                entity.Property(e => e.StackTrace).IsUnicode(false);

                entity.Property(e => e.TargetSite).IsUnicode(false);

                entity.Property(e => e.UserIp)
                    .HasColumnName("UserIP")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserLocation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserMachineName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.ErrorLog)
                    .HasForeignKey(d => d.BusinessProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ErrorLog_BusinessProfile");
            });

            modelBuilder.Entity<FileStoreSetting>(entity =>
            {
                entity.ToTable("FileStoreSetting", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HostDomain)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.HostIp)
                    .IsRequired()
                    .HasColumnName("HostIP")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.HostName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HostPassword).IsUnicode(false);

                entity.Property(e => e.HostUsername)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ShareDirectoryPath)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ShareName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.FileStoreSetting)
                    .HasForeignKey(d => d.BusinessProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileStoreSetting_BusinessProfile");

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.FileStoreSettingCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.FileStoreSettingLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId);
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => new { e.ExpireAt, e.Key })
                    .HasName("IX_HangFire_Hash_Key");

                entity.HasIndex(e => new { e.Id, e.ExpireAt })
                    .HasName("IX_HangFire_Hash_ExpireAt");

                entity.HasIndex(e => new { e.Key, e.Field })
                    .HasName("UX_HangFire_Hash_Key_Field")
                    .IsUnique();

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.StateName)
                    .HasName("IX_HangFire_Job_StateName");

                entity.HasIndex(e => new { e.Id, e.ExpireAt })
                    .HasName("IX_HangFire_Job_ExpireAt");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.ToTable("JobParameter", "HangFire");

                entity.HasIndex(e => new { e.JobId, e.Name })
                    .HasName("IX_HangFire_JobParameter_JobIdAndName");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameter)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.ToTable("JobQueue", "HangFire");

                entity.HasIndex(e => new { e.Queue, e.FetchedAt })
                    .HasName("IX_HangFire_JobQueue_QueueAndFetchedAt");

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");

                entity.Property(e => e.Queue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => new { e.Id, e.ExpireAt })
                    .HasName("IX_HangFire_List_ExpireAt");

                entity.HasIndex(e => new { e.ExpireAt, e.Value, e.Key })
                    .HasName("IX_HangFire_List_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value).HasColumnType("nvarchar(max)");
            });

            modelBuilder.Entity<PersistedGrants>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("PersistedGrants", "security");

                entity.Property(e => e.Key)
                    .HasMaxLength(200)
                    .ValueGeneratedNever();

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.SubjectId).HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.FileSizeInKb).HasColumnName("FileSizeInKB");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.OrginalFileName).IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.PhotoCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Photo_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.PhotoLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Photo_Contact_UpdatedBy");
            });

            modelBuilder.Entity<PostalCode>(entity =>
            {
                entity.ToTable("PostalCode", "core");

                entity.Property(e => e.Bspname)
                    .HasColumnName("BSPName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bspnumber)
                    .HasColumnName("BSPNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeliveryOffice)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.PareclZone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.PostalCodeCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostalCode_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.PostalCodeLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_PostalCode_Contact_UpdatedBy");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.PostalCode)
                    .HasForeignKey(d => d.StateId);
            });

            modelBuilder.Entity<PublicHoliday>(entity =>
            {
                entity.ToTable("PublicHoliday", "core");

                entity.HasIndex(e => new { e.EventDate, e.CountryId, e.StateId })
                    .HasName("UN_PublicHoliday")
                    .IsUnique();

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EventDate).HasColumnType("date");

                entity.Property(e => e.EventName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Timestamp).IsRowVersion();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PublicHoliday)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicHoliday_Country");

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.PublicHolidayCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublicHoliday_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.PublicHolidayLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_PublicHoliday_Contact_UpdatedBy");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.PublicHoliday)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_PublicHoliday_State");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalPartnerId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.RegionCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.RegionCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Region_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.RegionLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Region_Contact_UpdatedBy");

                entity.HasOne(d => d.Logo)
                    .WithMany(p => p.Region)
                    .HasForeignKey(d => d.LogoId)
                    .HasConstraintName("FK_Region_Photo");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version);

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<SecurityConfiguration>(entity =>
            {
                entity.ToTable("SecurityConfiguration", "core");

                entity.Property(e => e.AppHelpContentUrl)
                    .HasColumnName("AppHelpContentURL")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationTitle).HasMaxLength(200);

                entity.Property(e => e.B2busernameType).HasColumnName("B2BUsernameType");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnableRetrievePassword)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EnableSessionLog)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EnableSso).HasColumnName("EnableSSO");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MaximumImageUploadSizeInKb).HasColumnName("MaximumImageUploadSizeInKB");

                entity.Property(e => e.MaximumSesssionSpaceInKb).HasColumnName("MaximumSesssionSpaceInKB");

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.SecurityConfiguration)
                    .HasForeignKey(d => d.BusinessProfileId);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.SecurityConfigurationCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SecurityConfiguration_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.SecurityConfigurationLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_SecurityConfiguration_Contact_UpdatedBy");
            });

            modelBuilder.Entity<SecurityProfile>(entity =>
            {
                entity.ToTable("SecurityProfile", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descriptions).IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ProfileName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ResetUrlexpiryInHours).HasColumnName("ResetURLExpiryInHours");

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.BusinessProfile)
                    .WithMany(p => p.SecurityProfile)
                    .HasForeignKey(d => d.BusinessProfileId);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.SecurityProfileCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SecurityProfile_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.SecurityProfileLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_SecurityProfile_Contact_UpdatedBy");
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "core");

                entity.Property(e => e.CopyToEmailAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IncomingServer)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.OutgoingServer)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.ReplyToEmailAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SenderId)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SenderName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.Property(e => e.UseSslforIncoming).HasColumnName("UseSSLForIncoming");

                entity.Property(e => e.UseSslforOutgoing).HasColumnName("UseSSLForOutgoing");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.ServerCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Server_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.ServerLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_Server_Contact_UpdatedBy");
            });

            modelBuilder.Entity<Server1>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => new { e.Id, e.ExpireAt })
                    .HasName("IX_HangFire_Set_ExpireAt");

                entity.HasIndex(e => new { e.Key, e.Value })
                    .HasName("UX_HangFire_Set_KeyAndValue")
                    .IsUnique();

                entity.HasIndex(e => new { e.ExpireAt, e.Value, e.Key })
                    .HasName("IX_HangFire_Set_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State", "core");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.StateCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_State_Contact_CreatedBy");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.StateLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_State_Contact_UpdatedBy");
            });

            modelBuilder.Entity<State1>(entity =>
            {
                entity.ToTable("State", "HangFire");

                entity.HasIndex(e => e.JobId)
                    .HasName("IX_HangFire_State_JobId");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State1)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory", "store");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_Category");
            });

            modelBuilder.Entity<SystemEntity>(entity =>
            {
                entity.ToTable("SystemEntity", "security");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.SystemEntity)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SystemEntityRight>(entity =>
            {
                entity.ToTable("SystemEntityRight", "security");

                entity.HasIndex(e => new { e.RightKey, e.EntityId })
                    .HasName("UK_SystemEntityRight")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RightKey)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.SystemEntityRight)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SystemEntityRightDependency>(entity =>
            {
                entity.ToTable("SystemEntityRightDependency", "security");

                entity.HasOne(d => d.DependentRight)
                    .WithMany(p => p.SystemEntityRightDependencyDependentRight)
                    .HasForeignKey(d => d.DependentRightId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.SystemEntityRightDependencyRight)
                    .HasForeignKey(d => d.RightId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SystemModule>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.ToTable("SystemModule", "security");

                entity.Property(e => e.ModuleId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).IsRowVersion();
            });

            modelBuilder.Entity<TimeZone>(entity =>
            {
                entity.ToTable("TimeZone", "core");

                entity.Property(e => e.Id)
                    .HasMaxLength(200)
                    .ValueGeneratedNever();

                entity.Property(e => e.DaylightDisplayName).HasMaxLength(200);

                entity.Property(e => e.DaylightSavingEndDateTime).HasColumnType("datetime");

                entity.Property(e => e.DaylightSavingStartDateTime).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(200);
            });

            modelBuilder.Entity<VoucherType>(entity =>
            {
                entity.ToTable("VoucherType", "acc");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Remarks).HasMaxLength(250);

                entity.Property(e => e.ShortName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByContact)
                    .WithMany(p => p.VoucherTypeCreatedByContact)
                    .HasForeignKey(d => d.CreatedByContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherType_Contact");

                entity.HasOne(d => d.LastUpdatedByContact)
                    .WithMany(p => p.VoucherTypeLastUpdatedByContact)
                    .HasForeignKey(d => d.LastUpdatedByContactId)
                    .HasConstraintName("FK_VoucherType_Contact1");
            });
        }
    }
}
