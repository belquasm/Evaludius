﻿// ======================================
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
    public class Player
    {      

        public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int YearOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public virtual ICollection<TeamPlayer> Teams { get; set; }
        public  Position  Position { get; set; }
        public bool IsActive { get;  set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public ICollection<Assessment> Assessments { get; set; }
    }
}
