<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        Keeper
      </div>
    </router-link>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
      </ul>
      <!-- LOGIN COMPONENT HERE -->
        <div class="d-flex flex-column align-items-center mx-5">
          <button class="btn bg-dark text-white" v-if="account" @click="myProfile()">My Profile</button>
        </div>
      </div>
      <Login />
  </nav>
</template>

<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    return {
      account: computed(()=> AppState.account),
      async myProfile(){
        try {
        router.push({name: 'Profile', params: {profileId: AppState.account.id}})
        } catch (error) {
        router.push({name: 'Home'})
        Pop.error(error)
        }
      }
    };
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
