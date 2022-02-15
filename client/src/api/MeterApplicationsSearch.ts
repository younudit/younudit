import SearchObject from "@/core/SearchObject";
import { StringOperations } from "./SearchUtilities";

const const_api_location : string = "meter-applications";

class MeterApplicationsSearch extends SearchObject {
    api_location = const_api_location;

    constructor(
        api_url: string,
        include_api_prefix: boolean = false,
        account_number?: number,//@ARUsrAcctNbr - int
        meter_number?: string,//@MtrNbr - varchar(50)
        site_address?: string,//@SiteAddr - varchar(50)
        site_address_op?: StringOperations,//@SiteAddrCompare - int
        owner?: string,//@OwnerName - varchar(50)
        owner_op?: StringOperations//@OwnerNameCompare - int
        ) { 
            super({
                account_number,
                meter_number,
                site_address,
                site_address_op,
                owner,
                owner_op,
            },
            api_url,
            const_api_location,
            include_api_prefix,
            null)
        }

}

export default MeterApplicationsSearch;

