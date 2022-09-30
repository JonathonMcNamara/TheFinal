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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly KeepsService _keepsService;

        public VaultKeepsController(VaultKeepsService vaultKeepsService, KeepsService keepsService)
        {
            _vaultKeepsService = vaultKeepsService;
            _keepsService = keepsService;
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<VaultedKeep>> Create([FromBody] VaultKeep vaultKeep){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                vaultKeep.CreatorId = user.Id; // what if the user is null here???
                VaultKeep newVaultKeep = _vaultKeepsService.Create(vaultKeep, user);
                VaultedKeep keep = _keepsService.GetVaultedKeepById(newVaultKeep.KeepId);
                newVaultKeep.VaultId = vaultKeep.VaultId;
                newVaultKeep.Id = vaultKeep.Id;
                return Ok(vaultKeep);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VaultedKeep>> GetVaultKeepByVaultKeepId(int id){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                VaultedKeep vaultedKeep = _vaultKeepsService.GetVaultKeepByVaultKeepId(id);
                return Ok(vaultedKeep);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }







        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteVaultKeep(int id){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                VaultedKeep vaultedKeep = _vaultKeepsService.GetVaultKeepByVaultKeepId(id);
                if(vaultedKeep.CreatorId != user.Id){
                    throw new Exception("Unable to delete this");
                }
                string message = _vaultKeepsService.DeleteVaultKeep(id, user.Id);
                return Ok(message);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
    
}