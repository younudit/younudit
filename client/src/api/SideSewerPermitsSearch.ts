import SearchObject from "@/core/SearchObject";
import { StringOperations } from "./SearchUtilities";

const const_api_location : string = "side-sewer-permits";

export enum SideSewerPermitType {
    water = "water",
    sewer = "sewer"
}

class SideSewerPermitsSearch extends SearchObject {
    api_location = const_api_location;

    constructor(
        api_url: string,
        include_api_prefix: boolean = false,
        starting_account_number?: number,//@AcctNbr1 - int
        ending_account_number?: number,//@AcctNbr2 - int
        starting_permit_number?: number,//@PermitNbr1 - int
        ending_permit_number?: number,//@PermitNbr2 - int
        base_map?: string,//@BaseMap - varchar(5)
        site_address?: string,//@SiteAddr - varchar(255)
        site_address_op?: StringOperations,//@SiteAddrCompare - int
        parcel?: string,//@Parcel - varchar(255)
        parcel_op?: StringOperations//@ParcelCompare - int
        ) { 
            super({
                starting_account_number,
                ending_account_number,
                starting_permit_number,
                ending_permit_number,
                base_map,
                site_address,
                site_address_op,
                parcel,
                parcel_op
            },
            api_url,
            const_api_location,
            include_api_prefix,
            null)
        }

}

export default SideSewerPermitsSearch;