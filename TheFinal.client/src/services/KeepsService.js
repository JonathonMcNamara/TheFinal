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
    AppState.activeKeep = res.data
    logger.log(AppState.activeKeep)
}

}



export const keepsService = new KeepsService()