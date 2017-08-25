// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Models;
using FluentValidation;
using Evaludius.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Evaludius.ViewModels
{
    public class PlayerViewModel
    {
        public string Id { get; set; }

      //  [Required(ErrorMessage = "Playername is required"), StringLength(200, MinimumLength = 2, ErrorMessage = "Playername must be between 2 and 200 characters")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

       // [Required(ErrorMessage = "Email is required"), StringLength(200, ErrorMessage = "Email must be at most 200 characters"), EmailAddress(ErrorMessage = "Invalid email address")]
        public int YearOfBirth { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string   Position { get; set; }

        public string[] Teams { get; set; }

        public bool IsActive { get; set; }

    }




   
}
