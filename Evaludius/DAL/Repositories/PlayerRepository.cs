// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.Repositories.Interfaces;
using AutoMapper;
namespace DAL.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext context) : base(context)
        { }


        public IEnumerable<Player> GetTopActivePlayers(int count)
        {
            return GetAllPlayersData().Where(x => x.IsActive).Take(count).ToList();
        }


        public IEnumerable<Player> GetAllPlayersData()
        {
            return AppContext.Players.Include(x=>x.Position).Include(y=>y.Teams).ThenInclude(z=>z.Team)
                
                .ToList();
        }

        public Tuple<bool, string[]> UpdatePlayerAsync(Player player)
        {

           var playerEdit = AppContext.Players.FirstOrDefault(x => x.Id == player.Id);

            if (playerEdit == null)
            {
                string[] error = { $"{player.FullName} not found." };

                return Tuple.Create<bool, string[]>(false, error );
            }

            Mapper.Map(player, playerEdit);

            AppContext.SaveChanges();

            return Tuple.Create(true, new string[] { });
        }

        private ApplicationDbContext AppContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
