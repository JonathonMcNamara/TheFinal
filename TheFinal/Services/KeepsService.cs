using System;
using System.Collections.Generic;
using TheFinal.Models;
using TheFinal.Repositories;

namespace TheFinal.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsRepo;

        public KeepsService(KeepsRepository keepsRepo)
        {
            _keepsRepo = keepsRepo;
        }

        internal List<Keep> GetAllKeeps()
        {
            return _keepsRepo.GetAll();
        }

        internal Keep GetKeepById(int id)
        {
            Keep keep = _keepsRepo.GetKeepById(id);
            if(keep == null){
                throw new Exception("No keep by that id");
            }
            return keep;
        }
        
        internal Keep CreateKeep(Keep newKeep)
        {
        Keep keep =  _keepsRepo.Create(newKeep);
        return keep;
        }

        internal Keep UpdateKeep(Keep updateKeep, string userId)
        {
            Keep original = GetKeepById(updateKeep.Id);
            if (original.CreatorId != userId){
                throw new Exception("You are forbidden from updating this keep");
            }
            original.Name = updateKeep.Name ?? original.Name;
            original.Description = updateKeep.Description ?? original.Description;
            original.Img = updateKeep.Img ?? original.Img;
            return _keepsRepo.UpdateKeep(original);
        }

        internal List<Keep> GetKeepsByCreatorId(string id)
        {
            return _keepsRepo.GetKeepsByCreatorId(id);
        }

        internal string DeleteKeep(int id, string userId)
        {
            Keep keep = GetKeepById(id);
            if(keep.CreatorId != userId){
                throw new Exception("You are not allowed to delete this keep");
            }
            _keepsRepo.DeleteKeep(id);
            return $"The Keep has been deleted";
        }
        internal VaultedKeep GetVaultedKeepById(int keepId)
        {
            VaultedKeep keep = _keepsRepo.GetVaultKeep(keepId);
            if(keep == null){
                throw new Exception("No Vault Keep By This Id");
            }
            return keep;
        }
    }
}