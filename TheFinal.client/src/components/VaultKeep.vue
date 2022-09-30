<template>

<div class="my-2 row">
  <div class="col-12">
    <img @click="setActiveVaultKeep()" class="img-fluid" :src="vaultKeep?.img" alt="">
    <p>
      {{vaultKeep.name}}
      <router-link :to="{name: 'Profile' , params: {profileId: vaultKeep?.creatorId}}">
      <img class="profImg" :src="vaultKeep.creator?.picture" alt="">
      </router-link>
    </p>
  </div>
</div>
<VaultKeepModal/>


</template>
<script>
import { computed } from '@vue/reactivity';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';
import VaultKeepModal from './VaultKeepModal.vue';
export default {
    props: { vaultKeep: { type: Object, required: true } },
    setup(props) {
        return {
            activeVaultKeep: computed(() => AppState.activeVaultKeep),
            activeKeep: computed(()=> AppState.activeKeep),
            async setActiveVaultKeep() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("vaultKeepModal")).toggle();
                    await vaultsService.getVaultKeepById(props.vaultKeep?.id)
                }
                catch (error) {
                    Pop.error(error);
                }
            },
        };
    },
    components: { VaultKeepModal }
}
</script>
<style>
</style>