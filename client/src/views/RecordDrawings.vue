<template>
    <div>
        <v-card-subtitle class="text-uppercase">Record Drawings</v-card-subtitle>
        <v-card-text>
            <v-row>
                <v-col dense>
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
                            <v-select 
                                :items="Object.values(recordDrawingTypes)"
                                label="Type" 
                                hide-details
                                v-model="searchObject.type"
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
            <br />
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
                        <template v-slot:[`item.FileName`]="{ item }">
                            <v-btn color="primary" outlined small
                                @click="getFile(item.FileName)"
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
    import RecordDrawingsSearch, { RecordDrawingType } from '@/api/RecordDrawingsSearch';
    import { StringOperationValues } from '@/api/SearchUtilities';
    import RecordDrawingsFile from '@/api/RecordDrawingsFile';

    const RecordDrawings = Vue.extend({
        name: 'Record Drawings',
        mounted(){
            // @ts-expect-error: App Config is a little weird for vue and ts
            this.searchObject = new RecordDrawingsSearch(this?.appConfig?.$api_url);
        },
        data: () => ({
            loading: false,
            filter: "",
            results: [] as any[],
            searchObject: {} as RecordDrawingsSearch,
            stringOps: StringOperationValues,
            recordDrawingTypes: RecordDrawingType,
            headers: [
                { text: 'Project Name', align: 'start', value: 'ProjectName', sortable: true },
                { text: 'Base Mape', align: 'start', value: 'BaseMap', sortable: true },
                { text: 'Type', align: 'start', value: 'Type', sortable: true },
                { text: 'File Name', align: 'start', value: 'FileName', sortable: true }
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
            getFile(name: string) : void {
                let self = this;
                // @ts-expect-error: App Config is a little weird for vue and ts
                const fileMaker = new RecordDrawingsFile(self?.appConfig?.$api_url, false, name);
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

    export default RecordDrawings;
</script>