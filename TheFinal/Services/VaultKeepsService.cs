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
        private readonly KeepsRepository _keepsRepo;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultsService, KeepsService keepsService, KeepsRepository keepsRepo)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
            _keepsRepo = keepsRepo;
        }

        internal List<VaultedKeep> GetKeepsByVault(string userId, int id)
        {
            Vault vault = _vaultsService.GetVaultById(id);
            if(vault.IsPrivate && vault.CreatorId != userId){
                throw new Exception("Unable to access these vault keeps");
            }
            List<VaultedKeep> vaultkeeps = _vaultKeepsRepo.GetVaultKeepsByVaultId(vault.Id);
            return vaultkeeps;
        }

        internal VaultKeep Create(VaultKeep vaultKeep, Account user)
        {
            Vault vault = _vaultsService.GetVaultById(vaultKeep.VaultId);
            VaultedKeep keep = _keepsService.GetVaultedKeepById(vaultKeep.KeepId);
            if(vault.CreatorId != user.Id){
                throw new Exception("Unable to add vaultkeeps to this vault");
            }
            keep.Kept++;
            _keepsRepo.UpdateKeep(keep);
            return _vaultKeepsRepo.Create(vaultKeep);
        }

        internal string DeleteVaultKeep(int id, string userId)
        {
            VaultedKeep vaultedKeep = GetVaultKeepByVaultKeepId(id);
            if(vaultedKeep.CreatorId != userId){
                throw new Exception("You are unable to delete this");
            }
            vaultedKeep.Kept--;
            _keepsRepo.UpdateKeep(vaultedKeep);
            _vaultKeepsRepo.DeleteVaultKeep(id);
            return "VaultKeep removed";
        }

        internal VaultedKeep GetVaultKeepByVaultKeepId(int id)
        {
            VaultedKeep vaultedKeep = _vaultKeepsRepo.GetVaultKeepsByVaultKeepId(id);
            if(vaultedKeep == null){
                throw new Exception("Unable to delete this");
            }
            return vaultedKeep;
        }
    }
}