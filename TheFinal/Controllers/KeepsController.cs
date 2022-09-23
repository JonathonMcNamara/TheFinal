using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFinal.Models;
using TheFinal.Services;

namespace TheFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;

        public KeepsController(KeepsService keepsService)
        {
            _keepsService = keepsService;
        }

        [HttpGet]
        public ActionResult<List<Keep>> GetAllKeeps(){
            try
            {
                List<Keep> keeps = _keepsService.GetAllKeeps();
                return Ok(keeps);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep newKeep){
            try
            {
            Account user = await HttpContext.GetUserInfoAsync<Account>();
            newKeep.CreatorId = user.Id;
            Keep keep = _keepsService.CreateKeep(newKeep);
            keep.Creator = user;
            return Ok(keep);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Keep> GetKeepById(int id){
            try
            {
                Keep keep = _keepsService.GetKeepById(id);
                return keep;
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}