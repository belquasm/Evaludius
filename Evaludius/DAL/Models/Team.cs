// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System.Collections.Generic;

namespace DAL.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public int BirthYear { get; set; }

        public ApplicationUser Manager { get; set; }

        
        public virtual ICollection<TeamPlayer> Players { get; set; }
    }
}
