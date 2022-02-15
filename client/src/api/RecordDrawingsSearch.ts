import SearchObject from "@/core/SearchObject";
import { StringOperations } from "./SearchUtilities";

const const_api_location : string = "record-drawings";

export enum RecordDrawingType {
    water = "water",
    sewer = "sewer"
}

class RecordDrawingsSearch extends SearchObject {
    api_location = const_api_location;

    constructor(
        api_url: string,
        include_api_prefix: boolean = false,
        type?: RecordDrawingType,//@Type - varchar(1)
        base_map?: any,//@MapNumber - varchar(5)
        project?: string,//@ProjectName - varchar(255)
        project_op?: StringOperations//@ProjectNameCompare - int
        ) { 
            super({
                type,
                base_map,
                project,
                project_op,
            },
            api_url,
            const_api_location,
            include_api_prefix,
            null)
        }

}

export default RecordDrawingsSearch;