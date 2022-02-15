<template>
    <div>
        <v-card-subtitle class="text-uppercase">AP Vouchers</v-card-subtitle>
        <v-card-text>
            <v-row>
                <v-col dense>
                    <v-row dense>
                        <v-col>
                            <v-text-field 
                                placeholder="Starting Voucher Number" 
                                label="Starting Voucher Number"
                                hide-details 
                                v-model="searchObject.check_number_start"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Ending Voucher Number"
                                placeholder="Voucher Number" 
                                hide-details
                                v-model="searchObject.check_number_end"
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
                                v-model="searchObject.vendor_op"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Vendor"
                                placeholder="Vendor" 
                                v-model="searchObject.vendor"
                                clearable
                                hide-details 
                            />
                        </v-col>
                    </v-row>
                    <v-row dense>
                        <v-col>
                            <v-text-field 
                                label="Starting Payment Amount"
                                placeholder="Payment Amount"
                                hide-details
                                v-model="searchObject.payment_amount_start"
                                clearable
                            />
                        </v-col>
                        <v-col>
                            <v-text-field 
                                label="Ending Payment Amount"
                                placeholder="Payment Amount"
                                hide-details
                                v-model="searchObject.payment_amount_end"
                                clearable
                            />
                        </v-col>
                    </v-row>
                </v-col>
                <v-divider vertical />
                <v-col dense>
                    <v-row dense>
                        <v-col>
                            <v-menu ref="invoice_date_start_menu"
                                    v-model="invoice_date_start_menu"
                                    :close-on-content-click="false"
                                    :return-value.sync="searchObject.invoice_date_start"
                                    transition="scale-transition"
                                    offset-y
                                    min-width="auto">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field v-model="searchObject.invoice_date_start"
                                                  label="Invoice Starting Date"
                                                  prepend-icon="mdi-calendar"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"
                                                  hide-details></v-text-field>
                                </template>
                                <v-date-picker v-model="searchObject.invoice_date_start"
                                               no-title
                                               scrollable>
                                    <v-spacer></v-spacer>
                                    <v-btn text
                                           color="primary"
                                           @click="invoice_date_start_menu = false">
                                        Cancel
                                    </v-btn>
                                    <v-btn text
                                           color="primary"
                                           @click="$refs.invoice_date_start_menu.save(searchObject.invoice_date_start)">
                                        OK
                                    </v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                        <v-col>
                            <v-menu ref="invoice_date_end_menu"
                                    v-model="invoice_date_end_menu"
                                    :close-on-content-click="false"
                                    :return-value.sync="searchObject.invoice_date_end"
                                    transition="scale-transition"
                                    offset-y
                                    min-width="auto">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field v-model="searchObject.invoice_date_end"
                                                  label="Invoice Ending Date"
                                                  prepend-icon="mdi-calendar"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"
                                                  hide-details></v-text-field>
                                </template>
                                <v-date-picker v-model="searchObject.invoice_date_end"
                                               no-title
                                               scrollable>
                                    <v-spacer></v-spacer>
                                    <v-btn text
                                           color="primary"
                                           @click="invoice_date_end_menu = false">
                                        Cancel
                                    </v-btn>
                                    <v-btn text
                                           color="primary"
                                           @click="$refs.invoice_date_end_menu.save(searchObject.invoice_date_end)">
                                        OK
                                    </v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                    <v-row dense>
                        <v-col>
                            <v-menu ref="voucher_date_start_menu"
                                    v-model="voucher_date_start_menu"
                                    :close-on-content-click="false"
                                    :return-value.sync="searchObject.voucher_date_start"
                                    transition="scale-transition"
                                    offset-y
                                    min-width="auto">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field v-model="searchObject.voucher_date_start"
                                                  label="Voucher Starting Date"
                                                  prepend-icon="mdi-calendar"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"
                                                  hide-details></v-text-field>
                                </template>
                                <v-date-picker v-model="searchObject.voucher_date_start"
                                               no-title
                                               scrollable>
                                    <v-spacer></v-spacer>
                                    <v-btn text
                                           color="primary"
                                           @click="voucher_date_start_menu = false">
                                        Cancel
                                    </v-btn>
                                    <v-btn text
                                           color="primary"
                                           @click="$refs.voucher_date_start_menu.save(searchObject.voucher_date_start)">
                                        OK
                                    </v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                        <v-col>
                            <v-menu ref="voucher_date_end_menu"
                                    v-model="voucher_date_end_menu"
                                    :close-on-content-click="false"
                                    :return-value.sync="searchObject.voucher_date_end"
                                    transition="scale-transition"
                                    offset-y
                                    min-width="auto">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field v-model="searchObject.voucher_date_end"
                                                  label="Voucher Ending Date"
                                                  prepend-icon="mdi-calendar"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"
                                                  hide-details></v-text-field>
                                </template>
                                <v-date-picker v-model="searchObject.voucher_date_end"
                                               no-title
                                               scrollable>
                                    <v-spacer></v-spacer>
                                    <v-btn text
                                           color="primary"
                                           @click="voucher_date_end_menu = false">
                                        Cancel
                                    </v-btn>
                                    <v-btn text
                                           color="primary"
                                           @click="$refs.voucher_date_end_menu.save(searchObject.voucher_date_end)">
                                        OK
                                    </v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                    <v-row dense>
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
                        v-slot:[`item.InvoiceDate`]="{ item }"
                    >
                        {{ getUsDate(item.InvoiceDate) }}
                    </template>
                    <template
                        v-slot:[`item.PaymentDate`]="{ item }"
                    >
                        {{ getUsDate(item.PaymentDate) }}
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
    import ApVouchersSearch from '@/api/ApVouchersSearch';
    import { StringOperationValues } from '@/api/SearchUtilities';
    import ApVouchersFile from '@/api/ApVouchersFile';

    const ApVouchers = Vue.extend({
        name: 'ApVouchers',
        mounted(){
            // @ts-expect-error: App Config is a little weird for vue and ts
            this.searchObject = new ApVouchersSearch(this?.appConfig?.$api_url);
        },
        data: () => ({
            loading: false,
            filter: "",
            results: [] as any[],
            searchObject: {} as ApVouchersSearch,
            stringOps: StringOperationValues,
            headers: [
                { text: 'Check Number', align: 'start', value: 'Check#', sortable: true },
                { text: 'Vendor Name', align: 'start', value: 'VendorName', sortable: true },
                { text: 'Invoice Date', align: 'start', value: 'InvoiceDate', sortable: true },
                { text: 'Voucher Date', align: 'start', value: 'PaymentDate', sortable: true },
                { text: 'Payment Amount', align: 'start', value: 'PaymentAmount', sortable: true },
                { text: '', align: 'start', value: 'FileName', sortable: false, filterable: false },
            ],
            invoice_date_start_menu: false,
            invoice_date_end_menu: false,
            payment_date_start_menu: false,
            payment_date_end_menu: false,
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
                const fileMaker = new ApVouchersFile(self?.appConfig?.$api_url, false, path, name);
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

    export default ApVouchers;
</script>