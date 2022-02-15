import SearchObject from "@/core/SearchObject";

const const_api_location : string = "record-drawings";

class RecordDrawingsFile extends SearchObject {
    api_location = const_api_location;

    constructor(
        api_url: string,
        include_api_prefix: boolean = false,
        file_name: string
        ) { 
            super({
                file_name
            },
            api_url,
            const_api_location,
            include_api_prefix,
            null)
        }
}

export default RecordDrawingsFile;