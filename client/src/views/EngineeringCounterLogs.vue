<template>
  <div>
    <v-card-subtitle class="text-uppercase">Engineering Counter Logs</v-card-subtitle>
    <v-card-text>
      <v-row>
        <v-col dense>
          <v-row dense>
            <v-col>
                <v-text-field 
                    label="Starting Log Number"
                    placeholder="Log Number"
                    hide-details
                    v-model="searchObject.starting_log_number"
                    clearable
                />
            </v-col>
            <v-col>
                <v-text-field 
                    label="Ending Log Number"
                    placeholder="Log Number"
                    hide-details
                    v-model="searchObject.ending_log_number"
                    clearable
                />
            </v-col>
          </v-row>
            <v-row dense>
                <v-col>
                    <v-text-field 
                        label="Base Map"
                        placeholder="Base Map" 
                        v-model="searchObject.base_map"
                        clearable
                        hide-details 
                    />
                </v-col>
            </v-row>
        </v-col>
        <v-divider vertical />
        <v-col dense>
          <v-row dense>
            <v-col>
              <v-menu ref="start_date_menu"
                      v-model="start_date_menu"
                      :close-on-content-click="false"
                      :return-value.sync="searchObject.start_date"
                      transition="scale-transition"
                      offset-y
                      min-width="auto">
                  <template v-slot:activator="{ on, attrs }">
                      <v-text-field v-model="searchObject.start_date"
                                    label="Starting Date"
                                    prepend-icon="mdi-calendar"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                    hide-details></v-text-field>
                  </template>
                  <v-date-picker v-model="searchObject.start_date"
                                  no-title
                                  scrollable>
                      <v-spacer></v-spacer>
                      <v-btn text
                              color="primary"
                              @click="start_date_menu = false">
                          Cancel
                      </v-btn>
                      <v-btn text
                              color="primary"
                              @click="$refs.start_date_menu.save(searchObject.start_date)">
                          OK
                      </v-btn>
                  </v-date-picker>
              </v-menu>
            </v-col>
            <v-col>
              <v-menu ref="end_date_menu"
                      v-model="end_date_menu"
                      :close-on-content-click="false"
                      :return-value.sync="searchObject.end_date"
                      transition="scale-transition"
                      offset-y
                      min-width="auto">
                  <template v-slot:activator="{ on, attrs }">
                      <v-text-field v-model="searchObject.end_date"
                                    label="Ending Date"
                                    prepend-icon="mdi-calendar"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                    hide-details></v-text-field>
                  </template>
                  <v-date-picker v-model="searchObject.end_date"
                                  no-title
                                  scrollable>
                      <v-spacer></v-spacer>
                      <v-btn text
                              color="primary"
                              @click="end_date_menu = false">
                          Cancel
                      </v-btn>
                      <v-btn text
                              color="primary"
                              @click="$refs.end_date_menu.save(searchObject.end_date)">
                          OK
                      </v-btn>
                  </v-date-picker>
              </v-menu>
            </v-col>
          </v-row>
            <v-row dense>
              <v-col>
                <v-select 
                    :items="Object.values(logStatuses)"
                    label="Status" 
                    hide-details
                    v-model="searchObject.status"
                    clearable
                />
              </v-col>
            </v-row>
        </v-col>
      </v-row>
        <v-row dense>
          <v-spacer></v-spacer>
          <v-btn color="primary"
              @click="runSearch()"
              :disabled="loading"
              class="mt-5">
                <v-icon
                    v-if="!loading"
                    left 
                    dark
                >
                    {{ 'mdi-magnify' }}
                </v-icon>
                <v-progress-circular
                    v-else
                    :width="2"
                    indeterminate
                    color="white"
                ></v-progress-circular>
              Search
          </v-btn>
        </v-row>
        <br />
        <v-divider></v-divider>
        <v-row>
            <v-col>
                <v-data-table :headers="headers" :items="results" dense :search="filter" :loading="loading">
                  <template v-slot:top>
                      <v-text-field
                          v-model="filter"
                          label="Filter Results"
                          class="mt-5"
                      ></v-text-field>
                  </template>
                  <template v-slot:[`item.FirstDate`]="{ item }">
                        {{ getUsDate(item.FirstDate) }}
                  </template>
                  <template v-slot:[`item.Filename`]="{ item }">
                    <v-btn color="primary" outlined small
                        @click="getFile(item.Path, item.Filename)"
                    >
                      <v-icon
                          v-if="!loading"
                          dark
                      >{{ 'mdi-file' }}                           
                      </v-icon>
                    </v-btn>
                  </template>
                </v-data-table>
            </v-col>
        </v-row>
    </v-card-text>
  </div>
</template>

<script lang="ts">
    import Vue from 'vue'
    import EngineeringCounterLogsSearch, { LogStatus } from '@/api/EngineeringCounterLogsSearch';
    import EngineeringCounterLogsFile from '@/api/EngineeringCounterLogsFile';

    const EngineeringCounterLogs = Vue.extend({
        name: 'EngineeringCounterLogs',
        mounted(){
            // @ts-expect-error: App Config is a little weird for vue and ts
            this.searchObject = new EngineeringCounterLogsSearch(this?.appConfig?.$api_url);
        },
        data: () => ({
            loading: false,
            filter: "",
            results: [] as any[],
            searchObject: {} as EngineeringCounterLogsSearch,
            logStatuses: LogStatus,
            headers: [
                { text: 'Log Number', align: 'start', value: 'LogNo', sortable: true },
                { text: 'Base Map', align: 'start', value: 'BaseMap', sortable: true },
                { text: 'First Date', align: 'start', value: 'FirstDate', sortable: true },
                { text: 'Status', align: 'start', value: 'Status', sortable: true },
                { text: 'File', align: 'start', value: 'Filename', sortable: true }
            ],
            start_date_menu: false,
            end_date_menu: false,
        }),
                methods: {
            runSearch() : void {
                let self = this;
                self.loading = true;
                self.searchObject.search().then((r) => { 
                    if(self) self.results = r as any[];
                    self.loading = false;
                })
                .catch(error => console.log(error));
            },
            getFile(path: string, name: string) : void {
                let self = this;
                // @ts-expect-error: App Config is a little weird for vue and ts
                const fileMaker = new EngineeringCounterLogsFile(self?.appConfig?.$api_url, false, path, name);
                fileMaker.file().then((response: any) => {
                    const pdfBlob = new Blob([response.data], { type: "application/pdf" })

                    const blobUrl = window.URL.createObjectURL(pdfBlob)
                    const link = document.createElement('a')
                        link.href = blobUrl
                        link.setAttribute('download', name)
                        link.click();
                        link.remove();
                    URL.revokeObjectURL(blobUrl);
                })
                .catch((error: any) => console.log(error));
            },
            getUsDate(date: string) {
                const dateParts = date.split("-");
                return new Date(parseInt(dateParts[0]), parseInt(dateParts[1]) - 1, parseInt(dateParts[2].substr(0,2))).toJSON().slice(0,10);
            }
        }
    })

    export default EngineeringCounterLogs;
</script>