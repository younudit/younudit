<template>
    <div id="app">
        <v-app>
            <v-system-bar color="deep-purple darken-3"></v-system-bar>
            <v-app-bar app
                       color="cyan accent-4"
                       dark>
                <div class="d-flex align-center">
                    <v-icon class="ml-2 mr-3">mdi-image</v-icon>
                    Northshore Utility Scanned Images

                </div>

                <v-spacer></v-spacer>

                <v-btn href="#"
                       target="_blank"
                       text>
                    <span class="mr-2">Vhsw Web</span>
                    <v-icon>mdi-open-in-new</v-icon>
                </v-btn>
            </v-app-bar>


            <v-main class="pt-4">
                <v-card class="pt-12 pl-2 pr-2"
                        style="background-color: #0093E9; background-image: linear-gradient(160deg, #0093E9 0%, #80D0C7 100%); "
                        flat
                        height="100%" >
                    <v-row >
                        <v-col dense class="col-2">
                            <v-card elevation="12" dense>
                                <v-navigation-drawer floating
                                                     permanent dense>
                                    <v-list dense
                                            shaped>
                                        <v-subheader class="text-uppercase pl-5">Scanned Images Menu</v-subheader>
                                        <v-list-item-group color="blue">
                                            <v-list-item v-for="item in items"
                                                         :key="item.title"
                                                         :to="item.to"
                                                         exact
                                                         :disabled="item.disabled"
                                                         link
                                                         v-show="!item.disabled"
                                                         >
                                                <v-list-item-icon class="ml-0 mr-2">
                                                    <v-icon>{{ item.icon }}</v-icon>
                                                </v-list-item-icon>
                                                <v-list-item-content>
                                                    <v-list-item-title class="text-left">{{ item.title }}</v-list-item-title>
                                                </v-list-item-content>
                                            </v-list-item>
                                        </v-list-item-group>
                                    </v-list>
                                </v-navigation-drawer>
                            </v-card>
                        </v-col>
                        <v-col class="col-10" dense>
                            <v-card elevation="12">
                                <router-view />
                            </v-card>
                        </v-col>
                    </v-row>
                </v-card>
            </v-main>
        </v-app>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'App',

  data: () => ({
    //
      drawer: true,
      items: [
          { title: 'AP Vouchers', icon: 'mdi-folder', to: 'ap-vouchers', disabled: false },
          { title: 'Customer Documents', icon: 'mdi-content-copy', to: 'customer-documents', disabled: true },
          { title: 'Engineering Counter Logs', icon: 'mdi-view-list', to: 'engineering-counter-logs', disabled: false },
          { title: 'Side Sewer Permits', icon: 'mdi-circle', to: 'side-sewer-permits', disabled: false },
          { title: 'Meter Applications', icon: 'mdi-network', to: 'meter-applications', disabled: false },
          { title: 'Record Drawings', icon: 'mdi-folder', to: 'record-drawings', disabled: false },
          { title: 'Easements', icon: 'mdi-folder', to: 'easements', disabled: false },
          { title: 'Plat Maps', icon: 'mdi-map', to: 'plat-maps', disabled: true },
          { title: 'ULID T-Sheets', icon: 'mdi-content-paste', to: 'ulid-t-sheets', disabled: true },
      ],
  }),
});
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
