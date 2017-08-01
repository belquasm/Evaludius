using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Evaludius.Policies;
using Evaludius.ViewModels;
using DAL;
using Microsoft.Extensions.Logging;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Evaludius.ControllersUser
{
    [Route("api/[controller]")]
    public class EvaludiusController : Controller
    {

		private IUnitOfWork _unitOfWork;
		readonly ILogger _logger;


		public EvaludiusController(IUnitOfWork unitOfWork, ILogger<EvaludiusController> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		[HttpGet("players")]
		[Produces(typeof(List<PlayerViewModel>))]
		[Authorize(AuthPolicies.ViewPlayersPolicy)]
		public  IActionResult GetPlayers()
		{
			return  GetPlayers(-1, -1);
		}


		[HttpGet("players/{page:int}/{pageSize:int}")]
		[Produces(typeof(List<PlayerViewModel>))]
		[Authorize(AuthPolicies.ViewPlayersPolicy)]
		public  IActionResult GetPlayers(int page, int pageSize)
		{
			var players =  _unitOfWork.Players.GetAll();

			List<PlayerViewModel> playersVM = new List<PlayerViewModel>();

			foreach (var player in players)
			{
				var playerVM = Mapper.Map<PlayerViewModel>(player);


				playersVM.Add(playerVM);
			}

			return Ok(playersVM);
		}

	}
}
