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

namespace DAL.Models
{
    public class SkillSetItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxPoints { get; set; }
        public string Tag { get; set; }      

        public SkillSet SkillSet { get; set; }
    }
}
