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
            var players = _unitOfWork.Players.GetAllPlayersData();

            List<PlayerViewModel> playersVM = new List<PlayerViewModel>();
           
			foreach (var player in players)
			{
				var playerVM = Mapper.Map<PlayerViewModel>(player);

                playerVM.Position = player.Position.Name;
                playerVM.Teams = player.Teams.Select(x=>x.Team.Name).ToArray();

                playersVM.Add(playerVM);
			}

			return Ok(playersVM);
		}

        [HttpPut("player/{id}")]
        [Authorize(AuthPolicies.ManagePlayersPolicy)]
        public IActionResult UpdatePlayer(int id, [FromBody] Player player)
        {
            if (ModelState.IsValid)
            {
                if (player == null)
                    return BadRequest($"{nameof(player)} cannot be null");

                if ( id != player.Id)
                    return BadRequest("Conflicting player id in parameter and model data");
                             

                var result = _unitOfWork.Players.UpdatePlayerAsync(player);
                if (result.Item1)
                    return NoContent();

                AddErrors(result.Item2);

            }

            return BadRequest(ModelState);
        }

        [HttpDelete("player/{id}")]
        [Authorize(AuthPolicies.ManagePlayersPolicy)]
        public IActionResult DeletePlayer(int id)
        {
            if (ModelState.IsValid)
            {
                var player = _unitOfWork.Players.GetSingleOrDefault(x => x.Id == id);
                if (player == null)
                    return BadRequest($"{nameof(player)} with Id {id} not found");

                try
                {
                    _unitOfWork.Players.Remove(player);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    ModelState.AddModelError(null,$"En Error occurred while dleting Player with {player.FullName}");
                }
                
            }

            return BadRequest(ModelState);
        }


        [HttpPost("players")]
        [Authorize(AuthPolicies.ManagePlayersPolicy)]
        public IActionResult NewPlayer([FromBody] Player player)
        {
            if (ModelState.IsValid)
            {
                if (player == null)
                    return BadRequest($"{nameof(player)} cannot be null");



                _unitOfWork.Players.Add(player);
                _unitOfWork.SaveChanges();

            }

            return BadRequest(ModelState);
        }

        private void AddErrors(string[] errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}
