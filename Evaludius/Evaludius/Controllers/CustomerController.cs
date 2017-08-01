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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Evaludius.ViewModels;
using AutoMapper;
using DAL.Models;
using Microsoft.Extensions.Logging;
using Evaludius.Helpers;

namespace Evaludius.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;


        public CategoryController(IUnitOfWork unitOfWork, ILogger<CategoryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }



        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var allCategorys = _unitOfWork.Categories.GetAllCategorysData();
            return Ok(Mapper.Map<IEnumerable<CategoryViewModel>>(allCategorys));
        }



        //[HttpGet("throw")]
        //public IEnumerable<CategoryViewModel> Throw()
        //{
        //    throw new InvalidOperationException("This is a test exception: " + DateTime.Now);
        //}



        [HttpGet("email")]
        public async Task<string> Email()
        {
            string recepientName = "QickApp Tester"; //         <===== Put the recepient's name here
            string recepientEmail = "test@ebenmonney.com"; //   <===== Put the recepient's email here

            string message = EmailTemplates.GetTestEmail(recepientName, DateTime.UtcNow);

            (bool success, string errorMsg) response = await EmailSender.SendEmailAsync(recepientName, recepientEmail, "Test Email from Evaludius", message);

            if (response.success)
                return "Success";

            return "Error: " + response.errorMsg;
        }



        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value: " + id;
        }



        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }



        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }



        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
