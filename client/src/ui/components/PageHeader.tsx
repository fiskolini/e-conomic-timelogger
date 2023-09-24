import {ReactNode} from "react";
import isUndefined from "lodash/isUndefined";

import Spinner from "@/ui/icons/spinner";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";

export default function PageHeader<T>({data, title, children}: {
    data: ApiPagedResponse<T> | undefined,
    title: string,
    children?: string | ReactNode
}) {
    return (
        <div className='mt-4'>
            <div className="flex items-center gap-x-3">
                <h2 className="text-lg font-medium text-gray-800 capitalize">{title}</h2>

                <span className="px-3 py-1 text-xs text-blue-600 bg-blue-100 rounded-full">
                        {!isUndefined(data) ? `${data?.totalItems} items` : <Spinner/>} 
                </span>
            </div>

            {!isUndefined(children) &&
                <p className="mt-1 text-sm text-gray-400">{children}</p>
            }
        </div>
    )
}