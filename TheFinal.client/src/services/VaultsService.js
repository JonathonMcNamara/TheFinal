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

async createVault(vaultData){
    const res = await api.post("api/vaults", vaultData)
    logger.log(res.data)
    AppState.vaults.unshift(res.data)
}

async getVaultById(id){
    const res = await api.get("api/vaults/"+id)
    logger.log(res.data)
    AppState.activeVault = res.data
    logger.log(AppState.activeVault)
}

async deleteVault(id){
    const res = await api.delete("api/vaults/"+id)
    logger.log(res.data)
    AppState.vaults = AppState.vaults.filter(v => v.id != id)
}







}



export const vaultsService = new VaultsService()