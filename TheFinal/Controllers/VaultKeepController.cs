using System;
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
    public class VaultKeepController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultKeepController(VaultKeepsService vaultKeepsService)
        {
            _vaultKeepsService = vaultKeepsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultedKeep>> Create([FromBody] VaultKeep newVaultKeep){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                VaultedKeep keep = _vaultKeepsService.Create(newVaultKeep, user.Id);
                return Ok(keep);
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
    }
}