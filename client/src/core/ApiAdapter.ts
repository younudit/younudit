import ApiErrors from '../core/ApiErrors';
import axios, { AxiosError, AxiosResponse, Method } from 'axios';

export enum AxiosVerbs {
    get     = 'get',
    post    = 'post',
    all     = 'all',
    delete  = 'delete',
    put     = 'put',
    patch   = 'patch'
}

/**
 * ApiAdapter
 * 
 * A class used to connect the client to the API backend. It should send 
 * and receive data for any object definition that extends or includes
 * it. All traffic should be routed through here, as it includes auth handling
 * and app wide success and failure logging.
 * 
 */
class ApiAdapter {

    originalData: { [key: string] : any };
    [key: string] : any; 

    constructor(data: any) {
        this.originalData = data;

        for (let field in data) {
            this[field] = data[field];
        }

        this.errors = new ApiErrors();
    }

    data() {
        let data : { [key: string] : any } = {};

        for (let property in this.originalData) {
            if(this[property] !== "") {
                data[property] = this[property];
            }
        }

        return data;
    }
 

    reset() {
        for (let field in this.originalData) {
            this[field] = '';
        }

        this.errors.clear();
    }


    post(url: string) {
        return this.submit(AxiosVerbs.post, url);
    }

    postBlob(url: string) {
        return this.submitReturnBlob(AxiosVerbs.post, url);
    }

    put(url: string) {
        return this.submit(AxiosVerbs.put, url);
    }


    patch(url: string) {
        return this.submit(AxiosVerbs.patch, url);
    }


    delete(url: string) {
        return this.submit(AxiosVerbs.delete, url);
    }

    upload(url: string, formData : any){
        return new Promise((resolve, reject) => {
            axios.post(url, formData, {
                headers: {
                    'content-type': 'multipart/form-data'
                    }
                })
            .then(response => {
                resolve(response.data);
            }).catch(error => {
                this.onFail(error.response?.data || 'Null Response');

                reject(error.response.data);
            });
        });
    }

    get(url: string) {
        return new Promise((resolve, reject) => {
            axios.get(url)
                .then(response => {
                    resolve(response.data);
                })
                .catch(error => {
                    this.onFail(error.response?.data || 'Null Response');

                    reject(error.response.data);
                });
        });
    }

    async submit(requestType: Method, url: string) {
        const self = this;
        return new Promise((resolve, reject) => {
            axios({
                method: requestType,
                url: url,
                data: self.data(),
            })
            .then((response: AxiosResponse) => {
                self.onSuccess(response.data);

                resolve(response.data);
            })
            .catch((error: AxiosError) => {
                self.onFail(error.response?.data || 'Null Response');

                reject(error.response?.data || 'Null Response');
            });
        });
    }

    async submitReturnBlob(requestType: Method, url: string) {
        const self = this;
        return new Promise((resolve, reject) => {
            axios({
                method: requestType,
                url: url,
                data: self.data(),
                responseType: 'blob'
            })
            .then((response: AxiosResponse) => {
                self.onSuccess(response);

                resolve(response);
            })
            .catch((error: AxiosError) => {
                self.onFail(error.response?.data || 'Null Response');

                reject(error.response?.data || 'Null Response');
            });
        });
    }


    onSuccess(data: any) {
        //alert(data.message); // temporary

        //this.reset();
    }


    onFail(errors: any) {
        this.errors.record(errors);
        console.log(errors);
    }
};

export default ApiAdapter;