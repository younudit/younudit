import SearchObject from "@/core/SearchObject";
import { StringOperations } from "./SearchUtilities";

const const_api_location : string = "easements";

export enum EasementType {
    allTypes,
    waterOnly,
    sewerOnly
}

class EasementsSearch extends SearchObject {
    api_location = const_api_location;

    constructor(
        api_url: string,
        include_api_prefix: boolean = false,
        recording_number?: string,//@RecordingNumber
        recording_number_op?: StringOperations,//@RecordingNumberCompare
        reference?: string, //@RecordDrawingReference
        reference_op?: StringOperations,//@RecordDrawingReferenceCompare
        project?: string,//@ProjectName
        project_op?: StringOperations,//@ProjectNameCompare
        base_map?: string,//@BaseMap
        water?: boolean,//@Water
        sewer?: boolean,//@Sewer
        encroachment?: boolean,//@EncroachMent
        ) { 
            super({
                recording_number,
                recording_number_op,
                reference,
                reference_op, 
                project,
                project_op,
                base_map,
                water,
                sewer,
                encroachment
            },
            api_url,
            const_api_location,
            include_api_prefix,
            null)
        }

}

export default EasementsSearch;