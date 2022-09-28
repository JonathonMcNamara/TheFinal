<template>

<div class="profile container-fluid" v-if="profile">
  <div class="row my-5">
    <div class="col-1">
      <img class="img-fluid" :src="profile.picture" alt="">
   </div>
    <div class="col-2">
      <div class="row">
        <div class="col-12">
          <h3>{{profile.name}}</h3>
        </div>
        <div class="col-12">
          <h4>Vaults: {{profileVaults.length}}</h4>
        </div>
        <div class="col-12">
          <h4>Keeps: {{profileKeeps.length}}</h4>
        </div>
      </div>
    </div>
  </div>
  <div class="row my-5 py-5">
    <div class="col-12">
      <h1>Vaults <i class="mdi mdi-plus"></i> </h1>
      <div class="row">
        <div class="col-2">
          VAULT CARD FOR ALL THE VAULTS IN HERE
        </div>
      </div>
    </div>
  </div>
  <div class="row my-5 py-5">
    <div class="col-12">
      <h1>Keeps <i class="mdi mdi-plus"></i> </h1>
      <div class="row">
        <div class="col-2">
          KEEP CARD FOR ALL THE KEEPS IN HERE BY PROFILE
        </div>
      </div>
    </div>
  </div>



</div>








</template>
<script>
import { computed } from '@vue/reactivity';
import { profilesService} from '../services/ProfilesService.js'
import { vaultsService} from '../services/VaultsService.js'
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';

export default {
setup() {
  const route = useRoute()

  async function getProfileById(){
    try {
    await profilesService.getProfileById(route.params.profileId)
    } catch (error) {
    Pop.error(error)
    }
  }
  async function getProfileVaults(){
    try {
    await vaultsService.getProfileVaults(route.params.profileId)
    } catch (error) {
    Pop.error(error)
    }
  }
  async function getProfileKeeps(){
    try {
    await keepsService.getProfileKeeps(route.params.profileId)
    } catch (error) {
    Pop.error(error)
    }
  }

  onMounted(()=>{
    getProfileById()
    getProfileVaults()
    getProfileKeeps()
  })

  return {
    profile: computed(()=> AppState.activeProfile),
    profileVaults: computed(()=> AppState.vaults),
    profileKeeps: computed(()=> AppState.profileKeeps)
  }
}
}
</script>
<style>

i{
  color: rgb(6, 248, 119);
}

</style>