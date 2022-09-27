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

}



export const keepsService = new KeepsService()