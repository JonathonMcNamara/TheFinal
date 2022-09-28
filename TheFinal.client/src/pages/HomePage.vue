<template>

<main class="container">
        <figure class="masonry" v-for="k in keeps" :key="k.id">
            <Keep :keep="k"/>
        </figure>
</main>




</template>

<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import Keep from '../components/Keep.vue';

export default {
    setup() {
        async function getAllKeeps() {
            try {
                await keepsService.GetAllKeeps();
            }
            catch (error) {
                Pop.error(error);
            }
        }
        onMounted(() => {
            getAllKeeps();
        });
        return {
            keeps: computed(() => AppState.keeps),
        };
    },
    components: { Keep }
}
</script>

<style scoped lang="scss">

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
