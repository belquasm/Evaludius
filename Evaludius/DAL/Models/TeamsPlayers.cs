using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class TeamPlayer
    {

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

    }
}
