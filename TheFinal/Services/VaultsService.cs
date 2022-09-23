using System;
using TheFinal.Models;
using TheFinal.Repositories;

namespace TheFinal.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultsRepo;

        public VaultsService(VaultsRepository vaultsRepo)
        {
            _vaultsRepo = vaultsRepo;
        }

        internal Vault GetVaultById(int id)
        {
            Vault vault = _vaultsRepo.GetVaultById(id);
            if(vault == null){
                throw new Exception("No vault by that id");
            }
            return vault;
        }
        internal Vault CreateVault(Vault newVault)
        {
            Vault vault = _vaultsRepo.CreateVault(newVault);
            return vault;
        }

        internal Vault EditVault(Vault updateVault, string userId)
        {
            Vault original = GetVaultById(updateVault.Id);
            if(original.CreatorId != userId){
                throw new Exception("You are not authorized to edit this vault");
            }
            original.Name = updateVault.Name ?? original.Name;
            original.Description = updateVault.Description ?? original.Description;
            original.IsPrivate = updateVault.IsPrivate || original.IsPrivate;
            return _vaultsRepo.EditVault(original);
        }
    }
}
