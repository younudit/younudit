import SearchObject from "@/core/SearchObject";

const const_api_location : string = "engineering-counter-logs";

export enum LogStatus {
    Active = "Active",
    Closed = "Closed"
}

class EngineeringCounterLogsSearch extends SearchObject {
    api_location = const_api_location;

    constructor(
        api_url: string,
        include_api_prefix: boolean = false,
        starting_log_number?: string,//@LogNbr1 - decimal(18, 2)
        ending_log_number?: string,//@LogNbr2 - decimal(18, 2)
        base_map?: string,//@BaseMap - varchar(4)
        start_date?: Date,//@FirstDate1 - datetime
        end_date?: Date,//@FirstDate2 - datetime
        status?: LogStatus//@Status - varchar(20)
        ) { 
            super({
                starting_log_number,
                ending_log_number,
                base_map,
                start_date,
                end_date,
                status,
            },
            api_url,
            const_api_location,
            include_api_prefix,
            null)
        }

}

export default EngineeringCounterLogsSearch;