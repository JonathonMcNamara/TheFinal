using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TheFinal.Models;

namespace TheFinal.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault CreateVault(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, isPrivate, creatorId)
            VALUES
            (@name, @description, @isPrivate, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVault);
            newVault.Id = id;
            return newVault;
        }

        internal Vault GetVaultById(int id)
        {
            string sql = @"
            SELECT
            v.*,
            a.* 
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.id = @id;             
            ";
            Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile)=>{
                vault.Creator = profile;
                return vault;
            }, new {id}).FirstOrDefault();
            return vault;
        }

        internal Vault EditVault(Vault updateData)
        {
            string sql = @"
            UPDATE vaults SET
            name = @name,
            description = @description,
            isPrivate = @isPrivate
            WHERE id = @id;
            ";
            _db.Execute(sql, updateData);
            return updateData;
        }

        internal List<Vault> GetVaultByAccountId(string userId)
        {
            string sql = @"
            SELECT 
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE a.id = @userId;
            ";
            List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account)=>{
                vault.Creator = account;
                return vault;
            }, new{userId}).ToList();
            return vaults;
        }

        internal void DeleteVault(int id)
        {
            string sql = @"
            DELETE FROM vaults WHERE id = @id
            ";
            _db.Execute(sql, new{id});
        }

        internal List<Vault> GetVaultsByCreatorId(string id, string userId)
        {
            string sql = @"
            SELECT 
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @id
            ORDER BY v.isPrivate DESC;
            ";
            List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, prof)=>{
                vault.Creator = prof;
                return vault;
            },new {id}).ToList();
            return vaults;
        }

    }
}