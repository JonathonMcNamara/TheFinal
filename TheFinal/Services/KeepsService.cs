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

    }
}