<template>

<div class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-body p-2">
        <div class="row">
          <div class="col-6">
              <img class="img-fluid" :src="activeKeep?.img" alt="">
          </div>
          <div class="col-6">
            <div class="row">
              <div class="col-3 offset-1">
                <span class="mdi mdi-eye">{{activeKeep?.views}}</span>
              </div>
              <div class="col-3">
                <span class="mdi mdi-kickstarter">{{activeKeep?.kept}}</span>
              </div>
              <div class="col-3">
                <span class="mdi mdi-share-variant">0</span>
              </div>
              <div class="col-12">
                <h1>{{activeKeep?.name}}</h1>
                <h4>{{activeKeep?.description}}</h4>
              </div>
            </div>
            <div class="row">
              <div class="col-10">
                {{activeKeep?.creator.name}}
              </div>
              <div class="col-1">
              <router-link v-if="activeKeep" :to="{name: 'Profile' , params: {profileId: activeKeep?.creatorId}}">
                <img class="img" data-bs-dismiss="modal" :src="activeKeep?.creator.picture" alt="">
              </router-link>
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
    activeKeep: computed(()=> AppState.activeKeep),
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

.bottom{
  margin-top: 0;
}

</style>