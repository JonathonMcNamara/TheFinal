<template>

<div class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-body">
        <div class="row">
          <div class="col-6">
              <img class="img-fluid" :src="activeKeep?.img" alt="">
          </div>
          <div class="col-6">
            <h1>{{activeKeep?.name}}</h1>
            <p>{{activeKeep?.description}}</p>
            <div class="row">
              <div class="col-10">
                {{activeKeep?.creator.name}}
              </div>
              <div class="col-1">
                <img class="img" :src="activeKeep?.creator.picture" alt="">
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

</template>
<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {
setup() {
  return {
    activeKeep: computed(()=> AppState.activeKeep)
  }
}
}
</script>
<style>
.img{
  border-radius: 50%;
  height: 20px;
  width: 20px;
}
</style>