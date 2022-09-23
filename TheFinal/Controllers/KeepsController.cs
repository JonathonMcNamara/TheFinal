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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> UpdateKeep(int id, [FromBody] Keep updateKeep){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                updateKeep.Id = id;
                Keep keep = _keepsService.UpdateKeep(updateKeep, user.Id);
                return Ok(keep);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<Keep>> DeleteKeep(int id){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                string message = _keepsService.DeleteKeep(id, user.Id);
                return Ok(message);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}