<template>

    <h1>{{activeVault?.name}} <button @click="deleteVault()" v-if="activeVault?.creatorId == profile.id" class="btn">Delete Vault</button> </h1>
    <h1>Vault Keeps: {{vaultKeeps.length}}</h1>
    
    <div class="row">
      <div class="col-2" v-for="v in vaultKeeps" :key="v.id">
        <VaultKeep :vaultKeep="v"/>
      </div>
    </div>






</template>
<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';
import VaultKeep from '../components/VaultKeep.vue';

export default {
    setup() {
        const route = useRoute();
        async function getVaultById() {
            try {
                if (!AppState.activeVault) {
                    await vaultsService.getVaultById(route.params.vaultId);
                }
            }
            catch (error) {
                router.push({ name: "Home" });
                Pop.error("Unable to view page", error);
            }
        }
        async function getVaultKeeps() {
            try {
                await vaultsService.getVaultKeeps(route.params.vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        onMounted(async() => {
            getVaultById();
            getVaultKeeps();
        });
        return {
            profile: computed(() => AppState.account),
            activeVault: computed(() => AppState.activeVault),
            vaultKeeps: computed(() => AppState.vaultKeeps),
            async deleteVault() {
                try {
                    const yes = await Pop.confirm("Delete vault?");
                    if (!yes) {
                        return;
                    }
                    await vaultsService.deleteVault(route.params.vaultId);
                    router.push({ name: "Profile", params: { profileId: AppState.account.id } });
                    Pop.toast("Vault deleted");
                }
                catch (error) {
                    Pop.error(error);
                }
            },
        };
    },
    components: { VaultKeep }
}
</script>
<style>
</style>