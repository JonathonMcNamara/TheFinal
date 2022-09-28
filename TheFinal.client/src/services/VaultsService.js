import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class VaultsService{

async getProfileVaults(id){
    const res= await api.get(`/api/profiles/${id}/vaults`)
    logger.log(res.data)
    AppState.vaults = res.data
    logger.log("Profile vaults", AppState.vaults)
}







}



export const vaultsService = new VaultsService()