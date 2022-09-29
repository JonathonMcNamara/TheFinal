using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
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
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

        public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
        {
            _profilesService = profilesService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
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

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetUserKeeps(string id){
            try
            {
                List<Keep> keeps = _keepsService.GetKeepsByCreatorId(id);
                return Ok(keeps);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetUserVaults(string id){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> vaults = _vaultsService.GetVaultsByCreatorId(id, user?.Id);
                return Ok(vaults);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}