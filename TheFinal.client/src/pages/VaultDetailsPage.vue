<template>


Here is the vault details page with all the vaults



<h1>{{activeVault?.name}} <button @click="deleteVault()" v-if="activeVault?.creatorId == profile.id" class="btn">Delete Vault</button> </h1>
<h1>Vault Keeps count</h1>

<h1>Here goes the keeps in the vault</h1>



</template>
<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
setup() {
  const route = useRoute();
  async function getVaultById(){
    try {
    if(!AppState.activeVault){
      await vaultsService.getVaultById(route.params.vaultId)
    }
    } catch (error) {
    router.push({name: 'Home'})
    Pop.error("Unable to view page", error)
    }
  }

  onMounted(()=>{
    getVaultById()
  })
  return {
    profile: computed(()=> AppState.account),
    activeVault: computed(()=> AppState.activeVault),
    async deleteVault(){
      try {
      const yes = await Pop.confirm("Delete vault?")
      if(!yes){return}
      await vaultsService.deleteVault(route.params.vaultId)
      router.push({name: 'Profile', params:{profileId: AppState.account.id}})
      Pop.toast("Vault deleted")
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