import isUndefined from "lodash/isUndefined";

import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {NumberedArgumentFunction} from "@/app/types/handers/NumberedArgumentFunction";

export default function Pagination<T>({data, loading, loadHandler, currentPage}: {
    data: ApiPagedResponse<T>,
    loading: boolean,
    loadHandler: NumberedArgumentFunction,
    currentPage: number
}) {
    const prevPage = currentPage - 1;
    const nextPage = currentPage + 1;

    if (data?.data.length === 0) {
        return;
    }

    return (
        <div className="px-5 py-5 bg-white border-t flex flex-col xs:flex-row items-center xs:justify-between">
            <span className="text-xs xs:text-sm text-gray-900">
                Showing 1 to {data?.data.length} of {data?.totalItems} Entries
            </span>
            <div className={`inline-flex mt-2 xs:mt-0 ${loading ? 'cursor-wait' : 'cursor-pointer'}`}>
                <button
                    onClick={() => loadHandler(prevPage)}
                    className={`text-sm bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold py-2 px-4 rounded-l ${isUndefined(currentPage) || prevPage === 0 ? 'cursor-not-allowed opacity-50' : loading ? 'cursor-wait' : 'cursor-pointer'}`}
                    disabled={loading || isUndefined(currentPage) || prevPage === 0}
                >
                    &lt; Prev
                </button>
                <button
                    onClick={() => loadHandler(nextPage)}
                    className={`text-sm bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold py-2 px-4 rounded-r  ${isUndefined(currentPage) || currentPage === data.totalPages ? 'cursor-not-allowed opacity-50' : loading ? 'cursor-wait' : 'cursor-pointer'}`}
                    disabled={loading || isUndefined(currentPage) || nextPage > data.totalPages}
                >
                    Next &gt;
                </button>
            </div>
        </div>
    )
}