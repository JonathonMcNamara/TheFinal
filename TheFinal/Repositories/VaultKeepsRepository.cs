using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TheFinal.Models;

namespace TheFinal.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (vaultId, keepId, creatorId )
            VALUES
            (@vaultId, @keepId, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;
        }

        internal List<VaultedKeep> GetVaultKeepsByVaultId(int vaultId)
        {
            string sql = @"
            SELECT 
            vk.*,
            k.*,
            a.*
            FROM vaultkeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts a ON a.id = k.creatorId
            WHERE vk.vaultId = @vaultId;
            ";
            List<VaultedKeep> vaultKeeps = _db.Query<VaultKeep, VaultedKeep, Account, VaultedKeep>(sql, (vaultKeep, vaultedKeep, acc)=>{
                vaultedKeep.Creator = acc;
                vaultedKeep.VaultKeepId = vaultKeep.Id;
                return vaultedKeep;
            }, new {vaultId}).ToList();
            return vaultKeeps;
        }

        internal VaultedKeep GetVaultKeepsByVaultKeepId(int id)
        {
            string sql = @"
            SELECT * FROM vaultkeeps vk
            JOIN accounts a ON a.id = vk.creatorId
            WHERE vk.id = @id;
            ";
            VaultedKeep vaultedKeep = _db.Query<VaultedKeep, Profile, VaultedKeep>(sql, (vaultedKeep, prof)=>{
                vaultedKeep.Creator = prof;
                return vaultedKeep;
            }, new{id}).FirstOrDefault();
            return vaultedKeep;
        }

        internal void DeleteVaultKeep(int id){
            string sql = @"
            DELETE FROM vaultkeeps WHERE id = @id;
            ";
            _db.Execute(sql, new{id});
        }
    }
}