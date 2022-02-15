<template>
    <div>
        <v-card-subtitle class="text-uppercase">Customer Documents</v-card-subtitle>
        <v-card-text>
            <v-row>
                <v-col dense>
                    <v-row dense>
                        <v-col>
                            <v-text-field label="Account"
                                          placeholder="Account" hide-details />
                        </v-col>
                    </v-row>
                    <v-row dense>
                        <v-col>
                            <v-select :items="['active', 'closed']"
                                      label="Doc Type" hide-details />
                        </v-col>
                    </v-row>
                </v-col>
                <v-divider vertical />
                <v-col dense>
                    <v-row dense>
                        <v-col>
                            <v-menu ref="menu"
                                    v-model="menu"
                                    :close-on-content-click="false"
                                    :return-value.sync="date"
                                    transition="scale-transition"
                                    offset-y
                                    min-width="auto">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field v-model="date"
                                                  label="Starting Date"
                                                  prepend-icon="mdi-calendar"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"
                                                  hide-details></v-text-field>
                                </template>
                                <v-date-picker v-model="date"
                                               no-title
                                               scrollable>
                                    <v-spacer></v-spacer>
                                    <v-btn text
                                           color="primary"
                                           @click="menu = false">
                                        Cancel
                                    </v-btn>
                                    <v-btn text
                                           color="primary"
                                           @click="$refs.menu.save(date)">
                                        OK
                                    </v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                        <v-col>
                            <v-menu ref="ending_menu"
                                    v-model="ending_menu"
                                    :close-on-content-click="false"
                                    :return-value.sync="ending_date"
                                    transition="scale-transition"
                                    offset-y
                                    min-width="auto">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field v-model="ending_date"
                                                  label="Ending Date"
                                                  prepend-icon="mdi-calendar"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"
                                                  hide-details></v-text-field>
                                </template>
                                <v-date-picker v-model="ending_date"
                                               no-title
                                               scrollable>
                                    <v-spacer></v-spacer>
                                    <v-btn text
                                           color="primary"
                                           @click="ending_menu = false">
                                        Cancel
                                    </v-btn>
                                    <v-btn text
                                           color="primary"
                                           @click="$refs.ending_menu.save(date)">
                                        OK
                                    </v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                    <v-row dense>
                        <v-col>
                            <v-select :items="['active', 'closed']"
                                      label="User" hide-details />
                        </v-col>
                    </v-row>

                </v-col>
            </v-row>
            <v-row dense>
                <v-spacer></v-spacer>
                <v-btn color="primary"
                       class="mt-5">Search</v-btn>
            </v-row>
            <br />
            <v-divider></v-divider>
            <v-row>
                <v-col>
                    <v-data-table :headers="headers" dense></v-data-table>
                </v-col>
            </v-row>
        </v-card-text>
    </div>
</template>

<script lang="ts">
    export default {
        name: 'CustomerDocuments',
        data: () => ({
            headers: [
                { text: 'Log Number', align: 'start', value: 'log_number', sortable: true },
                { text: 'Base Map', align: 'start', value: 'base_map', sortable: true },
                { text: 'First Date', align: 'start', value: 'first_date', sortable: true },
                { text: 'Status', align: 'start', value: 'status', sortable: true },
                { text: 'File', align: 'start', value: 'file_name', sortable: true }
            ],
            date: null,// (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
            ending_date: null,// (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
            menu: false,
            ending_menu: false,
        }),
    }
</script>