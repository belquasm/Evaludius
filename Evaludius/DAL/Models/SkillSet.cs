// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SkillSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public string Tag { get; set; }
		public Category Category { get; set; }
		public ICollection<SkillSetItem> SkillSetItems { get; set; }
	}
}
