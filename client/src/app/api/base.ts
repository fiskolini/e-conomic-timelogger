import Axios from "axios";

export const apiClient = Axios.create({
    baseURL: process.env.NEXT_PUBLIC_BACKEND_URL
});

export const fetcher = (url: string) => apiClient.get(url).then((res) => res.data);