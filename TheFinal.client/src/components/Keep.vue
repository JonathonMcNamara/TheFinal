<template>

<div class="my-2 row">
  <div class="col-12">
    <img @click="setActiveKeep()" class="img-fluid img1" :src="keep?.img" alt="">
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
            async setActiveKeep(keepId) {
                try {
                    Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle();
                    await keepsService.setActiveKeep(props.keep.id);
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

.img1 {
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

figure > .img1 {
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
  background-color: rgba(255,255,255,.5);
  padding: .2em .5em;
  justify-self: start;
}

.container {
  column-count: 4;
  column-gap: 10px;
}

.profImg{
  border-radius: 50%;
  height: 20px;
  width: 20px;
}





</style>