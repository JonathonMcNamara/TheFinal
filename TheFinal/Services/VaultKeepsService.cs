using System;
using System.Collections.Generic;
using TheFinal.Models;
using TheFinal.Repositories;

namespace TheFinal.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultsService, KeepsService keepsService)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }

        internal VaultedKeep Create(VaultKeep newVaultKeep, string userId)
        {
            Vault vault = _vaultsService.GetVaultById(newVaultKeep.VaultId);
            if(vault.CreatorId != userId){
                throw new Exception("You are not the owner of this vault");
            }
            int id = _vaultKeepsRepo.Create(newVaultKeep);
            VaultedKeep vaultKeep = _keepsService.GetVaultedKeepById(newVaultKeep.KeepId);
            vaultKeep.VaultKeepId = id;
            return vaultKeep; 
        }

        internal List<VaultedKeep> GetKeepsByVault(int vaultId)
        {
            return _vaultKeepsRepo.GetKeepsByVaultId(vaultId);
        }
    }
}