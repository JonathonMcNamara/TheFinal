<template>


<div class="modal fade" id="vaultCreateModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-body p-2">

<form @submit.prevent="createVault()" class="post-form bg-light rounded">
<h3>Create Vault</h3>
<div class="row">

    <div class="col-12 my-2">
        <label class="form-label" for="title">Name</label>
        <input class="form-control" type="text" minlength="5" maxlength="30" id="title" name="name" v-model="editable.name">
    </div>

    <div class="col-12 my-2">
        <label class="form-label" for="body">Description</label>
        <input class="form-control" type="text" minlength="5" id="description" name="description" v-model="editable.description">
    </div>

    <div class="col-12 my-2">
        <div class="form-check">
        <input class="form-check-input" type="checkbox" v-model="editable.isPrivate" id="flexCheckDefault">
        <label class="form-check-label" for="flexCheckDefault">
            Private vault?
        </label>
</div>
    </div>

    <div>
        <button class="btn" type="submit">Create Vault</button>
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
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
setup() {
const editable = ref({})

watchEffect(()=>{
    editable.value = {...AppState.vaults}
})

  return {
    editable,
    async createVault(){
    try {
    await vaultsService.createVault(editable.value)
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