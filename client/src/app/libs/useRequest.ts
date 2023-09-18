import useSWR, {SWRConfiguration} from 'swr'
import {AxiosRequestConfig, AxiosResponse, AxiosError} from 'axios'

import {apiClient} from "@/app/api/base";
import {Return} from "@/app/types/ApiReturnType";

export type GetRequest = AxiosRequestConfig | null

export interface Config<Data = unknown, Error = unknown>
    extends Omit<
        SWRConfiguration<AxiosResponse<Data>, AxiosError<Error>>,
        'fallbackData'
    > {
    fallbackData?: Data
}

export default function useRequest<Data = unknown, Error = unknown>(
    request: GetRequest,
    {fallbackData, ...config}: Config<Data, Error> = {}
): Return<Data, Error> {
    const {
        data: response,
        error,
        isValidating,
        mutate,
        isLoading
    } = useSWR<AxiosResponse<Data>, AxiosError<Error>>(
        request,
        () => apiClient.request<Data>(request!),
        {
            ...config,
            fallbackData: fallbackData && {
                status: 200,
                statusText: 'InitialData',
                // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
                config: request!,
                headers: {},
                data: fallbackData
            } as AxiosResponse<Data>
        }
    )

    return {
        data: response && response.data,
        response,
        error,
        isValidating,
        mutate,
        isLoading
    }
}