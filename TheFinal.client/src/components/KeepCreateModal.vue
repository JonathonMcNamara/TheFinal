<template>

<div class="modal fade" id="keepCreateModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-body p-2">
        
<form @submit.prevent="submitKeep()" class="post-form bg-light rounded">
<h3>Create Keep</h3>
<div class="row">

    <div class="col-12">
        <label class="form-label" for="title">Name</label>
        <input class="form-control" type="text" minlength="5" id="title" name="name" v-model="editable.name">
    </div>

    <div class="col-12">
        <label class="form-label" for="body">Description</label>
        <input class="form-control" type="text" minlength="5" id="description" name="description" v-model="editable.description">
    </div>

    <div class="col-12">
        <label class="form-label" for="img">Img</label>
        <input class="form-control" type="text" minlength="5" id="img" name="img" v-model="editable.img">
    </div>

    <div>
        <button class="btn" type="submit">Create Keep</button>
    </div>
</div>

</form>

      </div>
    </div>
  </div>
</div>


</template>
<script>
import { ref, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';

export default {
setup() {
  const editable = ref({})

  watchEffect(()=> {
    editable.value = {...AppState.keeps}
  })

  return {
    editable,
    async submitKeep(){
    try {
    keepsService.createKeep(editable.value)
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