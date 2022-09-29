<template>


<div class="modal fade" id="profKeepModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
                  <select class="form-select" aria-label="Default select example">
                    <option selected>Add To Vault</option>
                    <option v-for="v in vaults" :key="v.id">{{v?.name}}</option>
                    </select>
                </div>
                <div class="col-3 bottom">
                  <i v-if="activeKeep?.creatorId == activeProfile.id" @click="deleteKeep()" class="mdi mdi-delete-forever btn"></i>
                </div>
                <div class="col-3 right px-1 text-end">
                  {{activeKeep?.creator.name}}
                  <router-link v-if="activeKeep" :to="{name: 'Profile' , params: {profileId: activeKeep?.creatorId}}">
                  <img class="img img-fluid" data-bs-dismiss="modal" :src="activeKeep?.creator.picture" alt="">
                  </router-link>
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
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';

export default {
setup() {
  return {
    activeKeep: computed(()=> AppState.activeKeep),
    vaults: computed(()=> AppState.vaults),
    activeProfile: computed(()=> AppState.account),
    async deleteKeep(){
      try {
      const yes = await Pop.confirm("Delete keep?")
      if(!yes) {return}
      await keepsService.deleteKeep(this.activeKeep.id)
      Pop.toast('Keep deleted')
      } catch (error) {
      Pop.error(error)
      }
    }
  }
}
}
</script>
<style>
</style>