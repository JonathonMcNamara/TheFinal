using System;
using Microsoft.AspNetCore.Mvc;
using TheFinal.Models;
using TheFinal.Services;

namespace TheFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;

        public ProfilesController(ProfilesService profilesService)
        {
            _profilesService = profilesService;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetUserProfile(string id){
            try
            {
                Profile profile = _profilesService.GetUserProfile(id);
                return Ok(profile);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}