using System.Data;
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

        internal Profile GetUserProfile(string id)
        {
            string sql = @"
            SELECT * from accounts WHERE id = @id;
            ";
            return  _db.QueryFirstOrDefault<Profile>(sql, new {id});
        }
    }
}