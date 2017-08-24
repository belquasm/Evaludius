// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        IEnumerable<Player> GetAllPlayersData();
        IEnumerable<Player> GetTopActivePlayers(int count);

        Tuple<bool, string[]> UpdatePlayerAsync(Player player);
    }
}
