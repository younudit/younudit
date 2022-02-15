/**
 * We'll load the axios HTTP library which allows us to easily issue requests
 * to our Laravel back-end. This library automatically handles sending the
 * CSRF token as a header based on the value of the "XSRF" token cookie.
 */
 import axios from "axios";

 axios.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest'; 
 axios.defaults.headers.common['Content-Type'] = 'application/json';
 axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
