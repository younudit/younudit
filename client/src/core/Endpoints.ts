import Vue from "vue";

export type apiEndpointBuilder = (api_location?: string, id?: number | null) => string;

/**
 * 
 */
const SEARCH_ENDPOINT_NAME  = 'search';
const FILE_ENDPOINT_NAME    = 'file';

/**
 * 
 */
class Endpoints {
    search: string;
    file: string;
    [key: string]:  any;

    constructor(api_url: string, api_location: string, include_api_prefix: boolean, extra: { [key: string] : apiEndpointBuilder } | null = null) {
        this.api_location = api_location;


        this.search = `${api_url}${include_api_prefix ? 
            '/api/' : '/'}${api_location}/${SEARCH_ENDPOINT_NAME}`;


        this.file = `${api_url}${include_api_prefix ? 
            '/api/' : '/'}${api_location}/${FILE_ENDPOINT_NAME}`;

        if(extra) {
            Object.keys(extra).forEach(key => {
                this[key] = extra[key](this.api_location, this.id);
            });
        }
    }
};

export default Endpoints;