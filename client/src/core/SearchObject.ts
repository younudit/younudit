import ApiAdapter   from './ApiAdapter';
import Endpoints, { apiEndpointBuilder }    from './Endpoints';

/**
 * 
 */
abstract class SearchObject extends ApiAdapter {
    abstract api_location: string;
    endpoints: Endpoints;

    constructor(data: { [key: string] : any }, api_url: string, api_location: string, include_api_prefix: boolean, extra: { [key: string] : apiEndpointBuilder } | null = null) {
        super(data);

        this.endpoints = new Endpoints(api_url, api_location, include_api_prefix, extra);
    }

    /**
     * 
     * @returns 
     */
    file(){
        return this.postBlob(this.endpoints.file);
    }

    /**
     * 
     * @returns 
     */
    search(){
        return this.post(this.endpoints.search);
    }

}

export default SearchObject;