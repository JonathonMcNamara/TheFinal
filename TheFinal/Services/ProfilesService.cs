using TheFinal.Repositories;

namespace TheFinal.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _profilesRepo;

        public ProfilesService(ProfilesRepository profilesRepo)
        {
            _profilesRepo = profilesRepo;
        }
    }
}