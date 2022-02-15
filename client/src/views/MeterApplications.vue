<template>
    <div>
        <v-card-subtitle class="text-uppercase">Meeter Applications</v-card-subtitle>
        <v-card-text>
            <v-row>
                <v-col dense>
                    <v-row dense>
                        <v-col>
                            <v-text-field 
                                label="Account Number"
                                placeholder="Account Number" 
                                v-model="searchObject.account_number"
                                clearable
                                hide-details 
                            />
                        </v-col>
                    </v-row>
                    <v-row dense>
                        <v-col class="col-4">
                            <v-select 
                                :items="stringOps"
                                item-text="name"
                                item-value="value"
                                label="Operator"  
                                hide-details 
                                v-model="searchObject.site_address_op"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Site Address"
                                placeholder="Address" 
                                hide-details 
                                v-model="searchObject.site_address"
                                clearable
                            />
                        </v-col>
                    </v-row>
                </v-col>
                <v-divider vertical />
                <v-col dense>
                    <v-row dense>
                        <v-col>
                            <v-text-field 
                                label="Meter Number"
                                placeholder="Meter Number" 
                                v-model="searchObject.meter_number"
                                clearable
                                hide-details 
                            />
                        </v-col>
                    </v-row>
                    <v-row dense>
                        <v-col class="col-4">
                            <v-select 
                                :items="stringOps"
                                item-text="name"
                                item-value="value"
                                label="Operator"  
                                hide-details 
                                v-model="searchObject.owner_op"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Owner"
                                placeholder="Owner" 
                                hide-details 
                                v-model="searchObject.owner"
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
                        <template
                            v-slot:[`item.FileName`]="{ item }"
                        >
                            <v-btn color="primary" outlined small
                                @click="getFile(item.Path, item.FileName)"
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
    import MeterApplicationsSearch from '@/api/MeterApplicationsSearch';
    import { StringOperationValues } from '@/api/SearchUtilities';
    import MeterApplicationsFile from '@/api/MeterApplicationsFile';

    const MeterApplications = Vue.extend({
        name: 'Meeter Applications',
        mounted(){
            // @ts-expect-error: App Config is a little weird for vue and ts
            this.searchObject = new MeterApplicationsSearch(this?.appConfig?.$api_url);
        },
        data: () => ({
            loading: false,
            filter: "",
            results: [] as any[],
            searchObject: {} as MeterApplicationsSearch,
            stringOps: StringOperationValues,
            headers: [
                { text: 'Account Number', align: 'start', value: 'AccountNbr', sortable: true },
                { text: 'Site Address', align: 'start', value: 'SiteAddress', sortable: true },
                { text: 'Meter Number', align: 'start', value: 'MeterNbr', sortable: true },
                { text: 'Meter Location', align: 'start', value: 'MtrLocation', sortable: true },
                { text: 'Owner Name', align: 'start', value: 'OwnerName', sortable: true },
                { text: 'File', align: 'start', value: 'FileName', sortable: true }
            ],
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
                const fileMaker = new MeterApplicationsFile(self?.appConfig?.$api_url, false, path, name);
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
    });
    
    export default MeterApplications;
</script>