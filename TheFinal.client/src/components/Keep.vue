<template>

<div class="my-2 row">
  <div class="col-12">
    <img @click="setActiveKeep()" class="img-fluid" :src="keep?.img" alt="">
    <p>
      {{keep.name}}
      <router-link :to="{name: 'Profile' , params: {profileId: keep?.creatorId}}">
      <img class="profImg" :src="keep.creator?.picture" alt="">
      </router-link>
    </p>
  </div>
</div>
<KeepModal/>





</template>
<script>
import { computed } from '@vue/reactivity';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import KeepModal from './KeepModal.vue';



export default {
    props: { keep: { type: Object, required: true } },
    setup(props) {
        return {
            activeKeep: computed(() => AppState.activeKeep),
            async setActiveKeep() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle();
                    await keepsService.getKeepById(props.keep.id);
                }
                catch (error) {
                    Pop.error(error);
                }
            }
        };
    },
    components: { KeepModal }
}
</script>
<style>


.profImg{
  border-radius: 50%;
  height: 20px;
  width: 20px;
}





</style>