using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TheFinal.Models;

namespace TheFinal.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetAll()
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId;
            ";
            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=>
            {
                keep.Creator = profile;
                return keep;
            }).ToList();
            return keeps;
        }

        internal Keep GetKeepById(int id)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.id = @id;
            ";
            Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=>
            {
                keep.Creator = profile;
                return keep;
            }, new {id}).FirstOrDefault();
            return keep;
        }

        internal Keep UpdateKeep(Keep updateData)
        {
            string sql = @"
            UPDATE keeps SET
            name = @name,
            description = @description,
            img = @img
            WHERE id = @id;
            ";
            _db.Execute(sql, updateData);
            return updateData;
        }

        internal Keep Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, creatorId)
            VALUES
            (@name, @description, @img, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newKeep);
            newKeep.Id = id;
            return newKeep;
        }

        internal void DeleteKeep(int id)
        {
            string sql = @"
            DELETE FROM keeps WHERE id = @id;
            ";
            _db.Execute(sql, new {id});
        }
    }
}