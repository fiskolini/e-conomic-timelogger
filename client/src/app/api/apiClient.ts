import Axios, {Method} from "axios";

export const apiClient = Axios.create({
    baseURL: process.env.NEXT_PUBLIC_BACKEND_URL
});


export const Get = (url: string) => doRequest('GET', url);

export const Post = (url: string, data?: Object) => doRequest('POST', url, data);

export const Patch = (url: string, data?: Object) => doRequest('PATCH', url, data);

export const Delete = (url: string) => doRequest('DELETE', url);

export const doRequest = (method: Method, url: string, data?: object) => {
    return apiClient.request({url, method, data})
}