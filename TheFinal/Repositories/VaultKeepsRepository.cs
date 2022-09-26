using System;
using System.Collections.Generic;
using System.Data;
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

        internal int Create(VaultKeep newVaultKeep)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (vaultId, keepId, creatorId)
            VALUES
            (@vaultId, @keepId, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            return id;
        }

        internal List<VaultedKeep> GetKeepsByVaultId(int vaultId)
        {
            throw new NotImplementedException();
        }
    }
}