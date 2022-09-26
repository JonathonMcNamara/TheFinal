using System;
using TheFinal.Models;
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

        internal Profile GetUserProfile(string profileId)
        {
            Profile profile = _profilesRepo.GetUserProfile(profileId);
            if(profile == null){
                throw new Exception("No profile by that Id");
            }
            return profile;
        }
    }
}