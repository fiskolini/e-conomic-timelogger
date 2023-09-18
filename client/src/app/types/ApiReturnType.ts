import {SWRResponse} from "swr";
import {AxiosError, AxiosResponse} from "axios";

export interface Return<Data, Error>
    extends Pick<
        SWRResponse<AxiosResponse<Data>, AxiosError<Error>>,
        'isValidating' | 'error' | 'mutate' | 'isLoading'
    > {
    data: Data | undefined
    response: AxiosResponse<Data> | undefined
}