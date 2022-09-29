<template>

<div class="row">
  <div class="col-12 text-center">
    {{vault.name}}
  </div>
  <div class="col-12" v-if="vault.isPrivate == false">
    <img @click="setActiveVault()" class="img-fluid" src="https://grid-paint.com/images/png/5692020383285248.png" alt="">
  </div>
  <div class="col-12" v-else="vault?.isPrivate == true">
    <img @click="setActiveVault()" class="img-fluid" src="https://lh3.googleusercontent.com/NGnsuT-nlywlSEk3S3e1c4C8-0owqKFTBbcBjovOAFp3tSUkYaIYONRoVFQvQHBTYueBDe6HdU2WwGy8sXu0ZOVDeid7dOG2L6M=s400" alt="">
  </div>
</div>


</template>
<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
props: {vault: {type: Object, required: true}},
setup(props) {
  return {
    activeVault: computed(()=> AppState.activeVault),
    async setActiveVault(){
      try {
      await vaultsService.getVaultById(props.vault.id)
      router.push({name: 'Vault', params: {vaultId: props.vault.id}})
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