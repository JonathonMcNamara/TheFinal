using System;
using System.Collections.Generic;
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
    }
}