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

namespace DAL.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string LastName { get; set; }
       
        public int BirthYear { get; set; }


        public ICollection<Player> Players { get; set; }
    }
}
