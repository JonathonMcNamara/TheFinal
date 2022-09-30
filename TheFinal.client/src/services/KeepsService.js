import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService{

async createKeep(keepData){
    const res = await api.post("api/keeps", keepData)
    logger.log("Creating Keep",res.data)
    AppState.keeps.unshift(res.data)
}


async GetAllKeeps(){
    const res = await api.get("api/keeps")
    console.log("Getting keeps", res.data)
    AppState.keeps = res.data
    logger.log("AppState", AppState.keeps)
}

async getKeepById(id){
    const res = await api.get("api/keeps/"+ id)
    console.log("Getting Single Keep", res.data)
    AppState.activeKeep = res.data
    logger.log(AppState.activeKeep)
}

async getProfileKeeps(id){
    const res = await api.get(`api/profiles/${id}/keeps`)
    logger.log("Profile Keeps", res.data)
    AppState.profileKeeps = res.data
    logger.log("Profile Keeps" ,AppState.profileKeeps)
}

async deleteKeep(id){
    const res = await api.delete("api/keeps/"+id)
    logger.log("Deleting vault", res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id != id)
}

async createVaultKeep(id, keep){
    const params = {
        vaultId: id,
        keepId: keep.id
    }
    const res = await api.post(`api/vaultkeeps`, params)
    logger.log("Creating VaultKeep", res.data)
    AppState.vaultkeeps = res.data
}

}



export const keepsService = new KeepsService()