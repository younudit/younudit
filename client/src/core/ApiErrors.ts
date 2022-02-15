/**
 * 
 */
class ApiErrors {
    errors: { [key: string]: any }

    constructor() {
        this.errors = {};
    }


    has(field: string) {
        return this.errors.hasOwnProperty(field);
    }


    any() {
        return Object.keys(this.errors).length > 0;
    }


    get(field: string) {
        if (this.errors[field]) {
            return this.errors[field][0];
        }
    }


    record(errors: { [key:string] : any }) {
        this.errors = errors.errors;
    }


    clear(field: string) {
        if (field) {
            delete this.errors[field];

            return;
        }

        this.errors = {};
    }
};

export default ApiErrors;

