import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService{

async GetAllKeeps(){
    const res = await api.get("api/keeps")
    console.log("Getting keeps", res.data)
    AppState.keeps = res.data
    logger.log("AppState", AppState.keeps)
}

async getKeepById(id){
    const res = await api.get("api/keeps/"+ id)
    console.log("Getting Single Keep", res.data)
    return res.data
}

async setActiveKeep(id){
    const res = await this.getKeepById(id)
    console.log("Setting active keep", res)
    AppState.activeKeep = res
    logger.log("Active keep in App State", AppState.activeKeep)
}

}



export const keepsService = new KeepsService()