// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Interfaces;

namespace DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }




    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Users.AnyAsync())
            {
                const string adminRoleName = "administrator";
                const string userRoleName = "user";

                await ensureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
                await ensureRoleAsync(userRoleName, "Default user", new string[] { });

                await createUserAsync("admin", "tempP@ss123", "Inbuilt Administrator", "admin@evaludius.com", "+1 (123) 000-0000", new string[] { adminRoleName });
                await createUserAsync("user",  "tempP@ss123", "Inbuilt Standard User", "user@evaludius.com", "+1 (123) 000-0001", new string[] { userRoleName });
            }



            if (!await _context.Categories.AnyAsync() && !await _context.Categories.AnyAsync())
            {
                Category cat = new Category
                {
                    Name = "Technical",
                   Description = "Technical aspect",
				   Tag = "Tech"
                };

				_context.Categories.Add(cat);
				// Technical SkillSets
				SkillSet skillSet = new SkillSet
				{
					Category = cat,
					Description = "Vision and Awareness",
					Name = "Vision and Awareness",
					Tag = "VA"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Receiving and Turning Efficiency",
					Name = "Receiving and Turning Efficiency",
					Tag = "RTE"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Proactive Passing",
					Name = "Proactive Passing",
					Tag = "PP"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Running & Dribbling",
					Name = "Running & Dribbling",
					Tag = "RD"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Finishing",
					Name = "Finishing",
					Tag = "FIN"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Proactive Defending",
					Name = "Proactive Defending",
					Tag = "PD"
				};

				_context.SkillSets.Add(skillSet);

				cat = new Category
				{
					Name = "Tactical",
					Description = "Tactical aspect",
					Tag = "Tact"
				};

				_context.Categories.Add(cat);

				// Tactical SkillSets

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Attacking",
					Name = "Attacking",
					Tag = "ATT"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Transition",
					Name = "Transition",
					Tag = "TRN"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Defending",
					Name = "Defending",
					Tag = "DEF"
				};

				_context.SkillSets.Add(skillSet);


				cat = new Category
				{
					Name = "SE",
					Description = "Socio-emotional aspect",
					Tag = "SE"
				};
				_context.Categories.Add(cat);

				// Socio-Emotional SkillSets
				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Cohesion",
					Name = "Cohesion",
					Tag = "COH"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Emotional Balance",
					Name = "Emotional Balance",
					Tag = "EB"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Communication",
					Name = "Communication",
					Tag = "COM"
				};

				_context.SkillSets.Add(skillSet);

				cat = new Category
				{
					Name = "Psycholigical",
					Description = "Psychological aspect",
					Tag = "Psy"
				};
				_context.Categories.Add(cat);

				// Psychological SkillSets
				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Commitment",
					Name = "Commitment",
					Tag = "COMM"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Concentration",
					Name = "Concentration",
					Tag = "CONC"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Confidence",
					Name = "Confidence",
					Tag = "CONF"
				};

				_context.SkillSets.Add(skillSet);



				cat = new Category
				{
					Name = "Physical",
					Description = "Physical acpect",
					Tag = "Phy"
				};

				_context.Categories.Add(cat);
				// Psychological SkillSets
				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Physical Literacy",
					Name = "Physical Literacy",
					Tag = "PHL"
				};

				_context.SkillSets.Add(skillSet);

				skillSet = new SkillSet
				{
					Category = cat,
					Description = "Match Fitness",
					Name = "Match Fitness",
					Tag = "MAFI"
				};

				_context.SkillSets.Add(skillSet);
				
				await _context.SaveChangesAsync();
            }
			if (!await _context.Positions.AnyAsync() && !await _context.Positions.AnyAsync())
			{
                _context.Positions.Add(new Position
                {
                    IsFieldPosition = false,
                    Name = "Goalie"
                });

                _context.Positions.Add(new Position
                {
                    Name = "Defender",
                    IsFieldPosition = true
                });

                _context.Positions.Add(new Position
                {
                    Name = "Midfield",
                    IsFieldPosition = true
                });

                _context.Positions.Add(new Position
                {
                    Name = "Forward",
                    IsFieldPosition = true
                });

                await _context.SaveChangesAsync();
            }
        }



        private async Task ensureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole(roleName, description);

                var result = await this._accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Item1)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
            }
        }

        private async Task<ApplicationUser> createUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Item1)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");


            return applicationUser;
        }
    }
}
