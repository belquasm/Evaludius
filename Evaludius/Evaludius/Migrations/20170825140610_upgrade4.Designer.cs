using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DAL;

namespace Evaludius.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170825140610_upgrade4")]
    partial class upgrade4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("DAL.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Configuration");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("JobTitle");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DAL.Models.Assessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("PlayerId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Assessment");
                });

            modelBuilder.Entity("DAL.Models.AssessmentResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AssessmentId");

                    b.Property<int?>("PlayerId");

                    b.Property<int?>("SkillSetItemId");

                    b.Property<int>("TotalPoints");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SkillSetItemId");

                    b.ToTable("AssessmentResults");
                });

            modelBuilder.Entity("DAL.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DAL.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<int?>("PositionId");

                    b.Property<int>("YearOfBirth");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DAL.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsFieldPosition");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("DAL.Models.SkillSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SkillSets");
                });

            modelBuilder.Entity("DAL.Models.SkillSetItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MaxPoints");

                    b.Property<string>("Name");

                    b.Property<int?>("SkillSetId");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.HasIndex("SkillSetId");

                    b.ToTable("SkillSetItems");
                });

            modelBuilder.Entity("DAL.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BirthYear");

                    b.Property<string>("Description");

                    b.Property<string>("ManagerId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DAL.Models.TeamPlayer", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("PlayerId");

                    b.HasKey("TeamId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("TeamPlayer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<string>("ClientSecret");

                    b.Property<string>("DisplayName");

                    b.Property<string>("LogoutRedirectUri");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("Scope");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AuthorizationId");

                    b.Property<string>("Subject");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("DAL.Models.Assessment", b =>
                {
                    b.HasOne("DAL.Models.Player")
                        .WithMany("Assessments")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("DAL.Models.AssessmentResult", b =>
                {
                    b.HasOne("DAL.Models.Assessment", "Assessment")
                        .WithMany()
                        .HasForeignKey("AssessmentId");

                    b.HasOne("DAL.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("DAL.Models.SkillSetItem", "SkillSetItem")
                        .WithMany()
                        .HasForeignKey("SkillSetItemId");
                });

            modelBuilder.Entity("DAL.Models.Player", b =>
                {
                    b.HasOne("DAL.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("DAL.Models.SkillSet", b =>
                {
                    b.HasOne("DAL.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("DAL.Models.SkillSetItem", b =>
                {
                    b.HasOne("DAL.Models.SkillSet", "SkillSet")
                        .WithMany("SkillSetItems")
                        .HasForeignKey("SkillSetId");
                });

            modelBuilder.Entity("DAL.Models.Team", b =>
                {
                    b.HasOne("DAL.Models.ApplicationUser", "Manager")
                        .WithMany("Teams")
                        .HasForeignKey("ManagerId");
                });

            modelBuilder.Entity("DAL.Models.TeamPlayer", b =>
                {
                    b.HasOne("DAL.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.Player", "Player")
                        .WithMany("Teams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });
        }
    }
}
