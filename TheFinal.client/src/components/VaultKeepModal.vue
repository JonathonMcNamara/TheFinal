<template>

    <div class="modal fade" id="vaultKeepModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-xl" role="document">
        <div class="modal-content">
          <div class="modal-body p-2">
            <div class="row">
              <div class="col-6">
                  <img class="img-fluid" :src="activeVaultKeep?.img" alt="">
              </div>
              <div class="col-6">
                <div class="row">
                  <div class="col-3 offset-1 text-center">
                    <span class="mdi mdi-eye">{{activeVaultKeep?.views}}</span>
                  </div>
                  <div class="col-3 text-center">
                    <span class="mdi mdi-kickstarter">{{activeVaultKeep?.kept}}</span>
                  </div>
                  <div class="col-3 text-center">
                    <span class="mdi mdi-share-variant">0</span>
                  </div>
                  <div class="col-12 text-center my-5">
                    <h1>{{activeVaultKeep?.name}}</h1>
                  </div>
                  <div class="col-12 text-center">
                    <p>{{activeVaultKeep?.description}}</p>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12">
                    <div class="col-3 bottom2">
                      <button @click="deleteVaultKeep()" class="btn">Delete From Vault</button>
                    </div>
                    <div class="col-3 right px-1 text-end">
                      {{activeVaultKeep?.creator.name}}
                      <router-link v-if="activeVaultKeep" :to="{name: 'Profile' , params: {profileId: activeVaultKeep?.creatorId}}">
                      <img class="img img-fluid" data-bs-dismiss="modal" :src="activeVaultKeep?.creator.picture" alt="">
                      </router-link>
                    </div>
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
    import { computed, ref } from '@vue/reactivity';
    import { AppState } from '../AppState.js';
    import { keepsService } from '../services/KeepsService.js';
    import { vaultsService } from '../services/VaultsService.js';
    import Pop from '../utils/Pop.js';
    
    export default {
      setup() {
      const editable = ref({})
      return {
        activeKeep: computed(()=> AppState.activeKeep),
        activeVaultKeep: computed(()=> AppState.activeVaultKeep),
        activeProfile: computed(()=> AppState.account.id),
        activeVault: computed(()=> AppState.activeVault),
        vaults: computed(()=> AppState.vaults),
        async deleteKeep(){
          try {
          const yes = await Pop.confirm("Delete keep?")
          if(!yes) {return}
          await keepsService.deleteKeep(this.activeKeep.id)
          Pop.toast('Keep deleted')
          } catch (error) {
          Pop.error(error)
          }
        },
        async deleteVaultKeep(){
        try {
        const yes = await Pop.confirm("Delete Vault Keep?")
        if(!yes) {return}
        await vaultsService.deleteVaultKeep(this.activeVaultKeep.vaultKeepId)
        } catch (error) {
        Pop.error(error)
        }
        }
      }
    }
    }
    </script>
    <style>
    
    .delete{
      height: 40px;
    }
    
    .modal-body{
      position: relative;
    }
    
    .bottom2{
      position: absolute;
      bottom: 8px;
    }
    
    
    .bottom {
      position: absolute;
      bottom: 8px;
      right: -40px;
    }
    
    .right{
      position: absolute;
      bottom: 8px;
      right: 0px;
    }
    
    
    
    .img{
      border-radius: 50%;
      height: 20px;
      width: 20px;
    }
    
    
    </style>