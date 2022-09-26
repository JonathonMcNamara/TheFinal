using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheFinal.Models;
using TheFinal.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly VaultsService _vaultsService;

        public AccountController(AccountService accountService, VaultsService vaultsService)
        {
            _accountService = accountService;
            _vaultsService = vaultsService;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("vaults")]
        [Authorize]
        public async Task<ActionResult<List<Vault>>> GetAccountVaults(){
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> vaults = _vaultsService.GetVaultByAccount(user.Id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}