// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OpenIddict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SkillSet> SkillSets { get; set; }

		public DbSet<SkillSetItem> SkillSetItems { get; set; }

        public DbSet<AssessmentResult> AssessmentResults { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get;  set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<TeamPlayer>()
         .HasKey(x => new { x.TeamId, x.PlayerId });

            modelBuilder.Entity<TeamPlayer>()
                .HasOne(x => x.Team)
                .WithMany(y => y.Players)
                .HasForeignKey(y => y.TeamId);

            modelBuilder.Entity<TeamPlayer>()
                .HasOne(x => x.Player)
                .WithMany(y => y.Teams)
                .HasForeignKey(y => y.PlayerId);


        }
    }
}
