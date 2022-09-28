<template>

<div class="profile" v-if="profile">
  <div class="row my-3">
    <div class="col-1">
      <img class="img-fluid" :src="profile.picture" alt="">
   </div>
    <div class="col-2">
      <div class="row">
        <div class="col-12">
          <h3>{{profile.name}}</h3>
        </div>
        <div class="col-12">
          <h4>Vaults: num</h4>
        </div>
        <div class="col-12">
          <h4>Keeps: num</h4>
        </div>
      </div>
    </div>
</div>



</div>








</template>
<script>
import { computed } from '@vue/reactivity';
import { profilesService } from '../services/ProfilesService.js'
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';

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

  onMounted(()=>{
    getProfileById()
  })

  return {
    profile: computed(()=> AppState.activeProfile)
  }
}
}
</script>
<style>
</style>