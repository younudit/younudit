<template>
    <div>
        <v-card-subtitle class="text-uppercase">
            Easements
            </v-card-subtitle>
        <v-card-text>
            <v-row >
                <v-col dense>
                    <v-row dense>
                        <v-col class="col-4">
                            <v-select 
                                :items="stringOps"
                                item-text="name"
                                item-value="value"
                                label="Operator" 
                                hide-details
                                v-model="searchObject.recording_number_op"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Recording Number"
                                placeholder="Recording Number" 
                                hide-details 
                                v-model="searchObject.recording_number"
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
                                v-model="searchObject.project_op"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Project"
                                placeholder="Project" 
                                hide-details 
                                v-model="searchObject.project"
                                clearable
                            />
                        </v-col>
                    </v-row>
                </v-col>
                <v-divider vertical />
                <v-col dense>
                    <v-row dense>
                        <v-col class="col-4">
                            <v-select 
                                :items="stringOps"
                                item-text="name"
                                item-value="value"
                                label="Operator" 
                                v-model="searchObject.reference_op" 
                                hide-details 
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Reference"
                                placeholder="Reference" hide-details 
                                v-model="searchObject.reference"
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
            </v-row>
            <v-row dense justify="space-between">
                <v-coL>
                    <v-row>
                        <v-col dense>
                            <v-checkbox
                                :label="`Water`"
                                v-model="searchObject.water"
                            ></v-checkbox>
                        </v-col>
                        <v-col dense>
                            <v-checkbox
                                :label="`Sewer`"
                                v-model="searchObject.sewer"
                            ></v-checkbox>
                        </v-col>
                        <v-col dense >
                            <v-checkbox
                                :label="`Encroachment`"
                                v-model="searchObject.encroachment"
                            ></v-checkbox>
                        </v-col>
                    </v-row>
                </v-coL>
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
                        v-slot:[`item.Project`]="{ item }"
                    >
                        <v-tooltip bottom v-if="item.Project && item.Project.length > 25">
                            <template v-slot:activator="{ on, attrs }">
                                <span
                                color="primary"
                                dark
                                v-bind="attrs"
                                v-on="on"
                                >
                                 {{ item.Project.substr(0,25) }}...
                                </span>
                            </template>
                            <span>{{ item.Project }}</span>
                        </v-tooltip>
                        <span v-else>{{ item.Project }}</span>
                    </template>
                    <template
                        v-slot:[`item.Status`]="{ item }"
                    >
                        <v-tooltip bottom>
                            <template v-slot:activator="{ on, attrs }">
                                <span
                                color="primary"
                                dark
                                v-bind="attrs"
                                v-on="on"
                                >
                                 {{ item.Status.substr(0,2) }}
                                </span>
                            </template>
                            <span>{{ item.Status }}</span>
                        </v-tooltip>
                    </template>
                    <template
                        v-slot:[`item.Water`]="{ item }"
                    >
                        {{ item.Water ? '✔️' : '' }}
                    </template>
                    <template
                        v-slot:[`item.Sewer`]="{ item }"
                    >
                        {{ item.Sewer ? '✔️' : '' }}
                    </template>
                    <template
                        v-slot:[`item.EncroachMent`]="{ item }"
                    >
                        {{ item.EncroachMent ? '✔️' : '' }}
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
    import EasementsSearch from "../api/EasementsSearch";
    import EasementsFile from "../api/EasementsFile";
    import { StringOperationValues } from "../api/SearchUtilities"
    
    const Easements = Vue.extend({
        name: 'Easements',
        mounted(){
            // @ts-expect-error: App Config is a little weird for vue and ts
            this.searchObject = new EasementsSearch(this?.appConfig?.$api_url);
        },
        data: () => ({
            loading: false,
            filter: "",
            results: [] as any[],
            searchObject: {} as EasementsSearch,
            stringOps: StringOperationValues,
            headers: [
                { text: 'Recording', align: 'start', value: 'RecordingNbr', sortable: true, filterable: true },
                { text: 'Project', align: 'start', value: 'Project', sortable: true },
                { text: 'Reference', align: 'start', value: 'Reference', sortable: true },
                { text: 'Map', align: 'start', value: 'BaseMap', sortable: true },
                { text: 'Status', align: 'start', value: 'Status', sortable: true },
                { text: 'S', align: 'start', value: 'Sewer', sortable: true },
                { text: 'W', align: 'start', value: 'Water', sortable: true },
                { text: 'E', align: 'start', value: 'EncroachMent', sortable: true },
                { text: 'Release', align: 'start', value: 'ReleaseNbr', sortable: true },
                { text: '', align: 'start', value: 'FileName', sortable: false, filterable: false },
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
                const fileMaker = new EasementsFile(self?.appConfig?.$api_url, false, path, name);
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
                .catch(error => console.log(error));
            }
        }
    })

    export default Easements;
</script>