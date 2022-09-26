using System.Data;
using System.Linq;
using Dapper;
using TheFinal.Models;

namespace TheFinal.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Profile GetUserProfile(int id)
        {
            string sql = @"
            SELECT * from accounts
            WHERE id = @id;
            ";
            Profile profile = _db.Query(sql, new{id}).FirstOrDefault();
            return profile;
        }
    }
}