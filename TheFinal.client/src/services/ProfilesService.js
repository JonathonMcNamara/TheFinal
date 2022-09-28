import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfilesService{

async getProfileById(id){
    logger.log("id", id)
    const res = await api.get(`api/profiles/${id}`)
    AppState.activeProfile = res.data
    logger.log("Profile", AppState.activeProfile)
}

}


export const profilesService = new ProfilesService()