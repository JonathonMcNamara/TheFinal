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
      <h1>Vaults <i @click="createVault()" class="mdi mdi-plus selectable"></i> </h1>
      <div class="row">
        <div class="col-2" v-for="v in profileVaults" :key="v.id">
          <ProfileVaultCard :vault="v"/>
        </div>
      </div>
    </div>
  </div>
  <div class="row my-5 py-5">
    <div class="col-12">
    </div>
    <div class="col-12">
      <h1>Keeps <i @click="createKeep()" class="mdi mdi-plus selectable"></i> </h1>
    </div>
      <main class="container">
      <figure class="masonry" v-for="k in profileKeeps" :key="k.id">
        <ProfileKeepCard :keep="k"/>
      </figure>
    </main>

  </div>
    </div>
<VaultCreateModal/>
<KeepCreateModal/>








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
import { Modal } from 'bootstrap';
import { logger } from '../utils/Logger.js';
import VaultCreateModal from '../components/VaultCreateModal.vue';
import KeepCreateModal from '../components/KeepCreateModal.vue';
import ProfileKeepCard from '../components/ProfileKeepCard.vue';

export default {
    setup() {
        const route = useRoute();
        async function getProfileById() {
            try {
                await profilesService.getProfileById(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getProfileVaults() {
            try {
                await vaultsService.getProfileVaults(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getProfileKeeps() {
            try {
                await keepsService.getProfileKeeps(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        onMounted(async() => {
            getProfileById();
            getProfileVaults();
            getProfileKeeps();
        });
        return {
            profile: computed(() => AppState.activeProfile),
            profileVaults: computed(() => AppState.vaults),
            profileKeeps: computed(() => AppState.profileKeeps),
            async createVault() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("vaultCreateModal")).toggle();
                }
                catch (error) {
                    Pop.error(error);
                    logger.log("Getting modal", error);
                }
            },
            async createKeep() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("keepCreateModal")).toggle();
                }
                catch (error) {
                    Pop.error(error);
                    logger.log("Getting modal", error);
                }
            }
        };
    },
    components: { VaultCreateModal, KeepCreateModal, ProfileKeepCard }
}
</script>
<style scoped>

body {
  background-color: #000;
  font: 1.1em Arial, Helvetica, sans-serif;
}

img {
  max-width: 100%;
  display: block;
}

figure {
  margin: 0;
  display: grid;
  grid-template-rows: 1fr auto;
  margin-bottom: 10px;
  break-inside: avoid;
}

figure>img {
  grid-row: 1 / -1;
  grid-column: 1;
}

figure a {
  color: black;
  text-decoration: none;
}

figcaption {
  grid-row: 2;
  grid-column: 1;
  background-color: rgba(255, 255, 255, .5);
  padding: .2em .5em;
  justify-self: start;
}

.container {
  column-count: 4;
  column-gap: 10px;
}

</style>