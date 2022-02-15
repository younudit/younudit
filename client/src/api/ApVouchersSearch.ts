import SearchObject from "@/core/SearchObject";
import { StringOperations } from "./SearchUtilities";

const const_api_location : string = "ap-vouchers";

class ApVouchersSearch extends SearchObject {
    api_location = const_api_location;

    constructor(
        api_url: string,
        include_api_prefix: boolean = false,
        invoice_date_start?: Date, // @InvoiceDate1
        invoice_date_end?: Date, // @InvoiceDate2
        payment_date_start?: Date, // @PaymentDate1
        payment_date_end?: Date, // @PaymentDate2
        payment_amount_start?: number, // @paymentAmount1
        payment_amount_end?: number, // @paymentAmount2
        check_number_start?: string, //@CheckNum1
        check_number_end?: string, //@CheckNum2
        vendor?: string, // @Vendor
        vendor_op?: StringOperations, // @VendorCompare
        ) { 
            super({
                api_url,
                include_api_prefix,
                check_number_start, //@CheckNum1
                check_number_end,
                vendor,
                vendor_op,
                invoice_date_start,
                invoice_date_end,
                payment_date_start,
                payment_date_end,
                payment_amount_start,
                payment_amount_end,
            },
            api_url,
            const_api_location,
            include_api_prefix,
            null)
        }

}

export default ApVouchersSearch;