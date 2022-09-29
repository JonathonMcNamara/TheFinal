<template>

<div class="row">
  <div class="col-12">
    <img @click="setActiveKeep()" class="img-fluid" :src="keep.img" alt="">
  </div>
</div>
<ProfileKeepModal/>



</template>
<script>
import { computed } from '@vue/reactivity';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import ProfileKeepModal from './ProfileKeepModal.vue';

export default {
    props: { keep: { type: Object, required: true } },
    setup(props) {
        return {
            keeps: computed(() => AppState.profileKeeps),
            async setActiveKeep() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("profKeepModal")).toggle();
                    await keepsService.getKeepById(props.keep.id);
                }
                catch (error) {
                    Pop.error(error);
                }
            }
        };
    },
    components: { ProfileKeepModal }
}
</script>
<style>
</style>
