import {AxiosResponse} from "axios";

type FetchFunction = () => Promise<AxiosResponse>;
type CallbackFunction = () => void;

export const createFetch = () => {
    // Create a cache of fetches by URL
    const fetchMap: { [key: string]: Promise<any> } = {}; // Change Function to Promise<any>

    return (promise: FetchFunction, callback?: CallbackFunction) => {
        let k = promise.name;

        // Check to see if it's not in the cache otherwise fetch it
        if (!(k in fetchMap)) {
            fetchMap[k] = promise().then((res: AxiosResponse) => {
                if (res) {
                    return res.data?.data || {};
                }
                return {};
            });
        }

        callback && callback.call({});

        // Return the cached promise
        return fetchMap[k];
    };
};