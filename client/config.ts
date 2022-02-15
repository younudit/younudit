
export type AppConfig = {
    /**
     * The url of the application api, ie https://localhost:8001, 
     * https://api.example.com, https://example.com/api/
     */
    $api_url: string,
    
    /**
     * How long to wait before time out on any request
     */
    timeoutDuration: number,
    
    /**
     * The url of the application, ie https://localhost
     */
    app_url: string,

    /**
     * Should each api request add api to the beginning of each call
     */
    api_prefix: boolean
}

let config : AppConfig;

if (process.env.NODE_ENV === "production") {
  config = {
    $api_url: "./api",
    timeoutDuration: 30000,
    app_url: '',
    api_prefix: false
  };
} else {
  config = {
    $api_url: "https://localhost:44334",
    timeoutDuration: 1000,
    app_url: '',
    api_prefix: false
  };
}

export { config }