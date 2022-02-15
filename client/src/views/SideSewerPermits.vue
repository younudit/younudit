<template>
    <div>
        <v-card-subtitle class="text-uppercase">Side Sewer Permits</v-card-subtitle>
        <v-card-text>
            <v-row>
                <v-col dense>
                    <v-row dense>
                        <v-col>
                            <v-text-field 
                                placeholder="Starting Permit Number" 
                                label="Starting Permit Number"
                                hide-details 
                                v-model="searchObject.starting_permit_number"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Ending Permit Number"
                                placeholder="Permit Number" 
                                hide-details
                                v-model="searchObject.ending_permit_number"
                                clearable
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
                            <v-text-field 
                                placeholder="Starting Account Number" 
                                label="Starting Account Number"
                                hide-details 
                                v-model="searchObject.starting_account_number"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Ending Account Number"
                                placeholder="Account Number" 
                                hide-details
                                v-model="searchObject.ending_account_number"
                                clearable
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
                                v-model="searchObject.parcel_op" 
                                hide-details 
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Parcel"
                                placeholder="Parcel" hide-details 
                                v-model="searchObject.parcel"
                                clearable
                            />
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
                </v-col>
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
    import SideSewerPermitsSearch, { SideSewerPermitType } from '@/api/SideSewerPermitsSearch';
    import { StringOperationValues } from '@/api/SearchUtilities';
    import SideSewerPermitsFile from '@/api/SideSewerPermitsFile';

    const SideSewerPermits = Vue.extend({
        name: 'SideSewerPermits',
        mounted(){
            // @ts-expect-error: App Config is a little weird for vue and ts
            this.searchObject = new SideSewerPermitsSearch(this?.appConfig?.$api_url);
        },
        data: () => ({
            loading: false,
            filter: "",
            results: [] as any[],
            searchObject: {} as SideSewerPermitsSearch,
            stringOps: StringOperationValues,
            permitType: SideSewerPermitType,
            headers: [
                { text: 'Permit Number', align: 'start', value: 'PermitNum', sortable: true },
                { text: 'Account Number', align: 'start', value: 'AcctNum', sortable: true },
                { text: 'Service Address', align: 'start', value: 'SiteAddress', sortable: true },
                { text: 'Base Map', align: 'start', value: 'BaseMap', sortable: true },
                { text: 'Tax Number', align: 'start', value: 'ParcelTaxNbr', sortable: true },
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
                const fileMaker = new SideSewerPermitsFile(self?.appConfig?.$api_url, false, path, name);
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

    export default SideSewerPermits;
</script>