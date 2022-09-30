<template>

<div class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content">
      <div class="modal-body p-2">
        <div class="row">
          <div class="col-6">
              <img class="img-fluid" :src="activeKeep?.img" alt="">
          </div>
          <div class="col-6">
            <div class="row">
              <div class="col-3 offset-1 text-center">
                <span class="mdi mdi-eye">{{activeKeep?.views}}</span>
              </div>
              <div class="col-3 text-center">
                <span class="mdi mdi-kickstarter">{{activeKeep?.kept}}</span>
              </div>
              <div class="col-3 text-center">
                <span class="mdi mdi-share-variant">0</span>
              </div>
              <div class="col-12 text-center my-5">
                <h1>{{activeKeep?.name}}</h1>
              </div>
              <div class="col-12 text-center">
                <p>{{activeKeep?.description}}</p>
              </div>
            </div>
            <div class="row">
              <div class="col-12">
                <div class="col-3 bottom2">
                  <select @change="setActiveVault($event.target.value)" v-if="activeProfile" class="form-select" aria-label="Default select example">
                    <option selected>Add To Vault</option>
                    <option v-for="v in vaults" :key="v.id" :value="v.id">{{v?.name}}</option>
                    </select>
                </div>
                <div class="col-3 bottom">
                  <i v-if="activeKeep?.creatorId == activeProfile" @click="deleteKeep()" class="btn mdi mdi-delete-forever"></i>
                </div>
                <div class="col-3 right px-1 text-end">
                  <router-link v-if="activeKeep" :to="{name: 'Profile' , params: {profileId: activeKeep?.creatorId}}">
                  <img class="img img-fluid" data-bs-dismiss="modal" :src="activeKeep?.creator.picture" alt="">
                  </router-link>
                  {{activeKeep?.creator.name}}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

</template>
<script>
import { computed, ref } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
  const editable = ref({})
  return {
    activeKeep: computed(()=> AppState.activeKeep),
    activeProfile: computed(()=> AppState.account.id),
    activeVault: computed(()=> AppState.activeVault),
    vaults: computed(()=> AppState.vaults),
    async deleteKeep(){
      try {
      const yes = await Pop.confirm("Delete keep?")
      if(!yes) {return}
      await keepsService.deleteKeep(this.activeKeep.id)
      Pop.toast('Keep deleted')
      } catch (error) {
      Pop.error(error)
      }
    },
    async setActiveVault(id){
    try {
    const yes = await Pop.confirm("Add to vault?")
    if(!yes) {return}
    await keepsService.createVaultKeep (id, AppState.activeKeep)
    Pop.toast("Added to vault!")
    } catch (error) {
    Pop.error(error)
    }
    }
  }
}
}
</script>
<style>

.delete{
  height: 40px;
}

.modal-body{
  position: relative;
}

.bottom2{
  position: absolute;
  bottom: 8px;
}


.bottom {
  position: absolute;
  bottom: 8px;
  right: -40px;
}

.right{
  position: absolute;
  bottom: 8px;
  right: 0px;
}



.img{
  border-radius: 50%;
  height: 20px;
  width: 20px;
}


</style>