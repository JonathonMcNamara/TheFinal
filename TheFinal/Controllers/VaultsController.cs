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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService)
        {
            _vaultsService = vaultsService;
            _vaultKeepsService = vaultKeepsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                newVault.CreatorId = user.Id;
                Vault vault = _vaultsService.CreateVault(newVault);
                vault.Creator = user;
                return Ok(vault);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetVaultById(int id){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vaultsService.GetVaultById(id);
                if(vault.IsPrivate && vault.CreatorId != user.Id){
                    throw new Exception("Unable to view this vault");
                }
                return Ok(vault);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditVault(int id, [FromBody] Vault updateVault){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                updateVault.Id = id;
                Vault vault = _vaultsService.EditVault(updateVault, user.Id);
                return Ok(vault);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteVault(int id){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vaultsService.GetVaultById(id);
                if(vault.CreatorId != user.Id){
                    throw new Exception("Unable to delete this");
                }
                string message = _vaultsService.DeleteVault(id, user.Id);
                return Ok(message);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async  Task<ActionResult<List<VaultedKeep>>> GetVaultKeeps(int id){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<VaultedKeep> vaultKeeps = _vaultKeepsService.GetKeepsByVault(user.Id, id);
                return Ok(vaultKeeps);
            }
                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}