import Spinner from "@/ui/icons/spinner";
import Actions from "@/ui/components/Customers/Actions";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {Customer} from "@/app/types/entities/Customer";
import Pagination from "@/ui/components/Pagination";
import {NumberedArgumentFunction} from "@/app/types/handers/NumberedArgumentFunction";
import TableHeader from "@/ui/components/TableHeader";
import isUndefined from "lodash/isUndefined";


export default function CustomersTable({data, loading, loadData, currentPage}: {
    data: ApiPagedResponse<Customer> | undefined,
    loading: boolean,
    loadData: NumberedArgumentFunction,
    currentPage: number
}) {
    const tableHeader = [
        '#', 'Customer Name', 'N. Projects', 'Total Time Allocated', 'Actions'
    ];

    return (
        <>
            <table className={`table-fixed w-full ${loading ? 'opacity-50 cursor-wait' : ''}`}>
                <TableHeader items={tableHeader}/>
                
                <tbody className='text-gray-500'>
                {isUndefined(data) &&
                    // Skeleton while loading in the background
                    Object.keys(Array.from(Array(3))).map(function (ri) {
                        return (
                            <tr key={ri} className='text-center border'>
                                {Object.keys(tableHeader ?? []).map(function (i) {
                                    return (
                                        <th key={i} className="border px-4 py-2 w-12">
                                            <Spinner key={i}/>
                                        </th>
                                    )
                                })}
                            </tr>
                        )
                    })
                }

                {
                    // Real rows populated from data
                    data?.data.map(function (item, i) {
                        return (
                            <tr key={i}
                                className={`text-center border ${item.dateDeleted !== null ? 'opacity-30 bg-gray-200' : ''}`}>
                                <th className='border px-4 py-2 text-gray-400 text-xs'>
                                    {item.id}
                                </th>
                                <th className="border px-4 py-2">
                                    {item.name}
                                </th>
                                <th className="border px-4 py-2">
                                    {item.numberOfProjects}
                                </th>
                                <th className={`border px-4 py-2 ${item.totalTimeAllocated > 0 ? '' : 'opacity-50'}`}>
                                    {item.totalTimeAllocated} <span className='text-xs text-gray-500'> mins.</span>
                                </th>
                                <th className="border px-4 py-2">
                                    {item.dateDeleted === null &&
                                        <Actions loading={loading} handleRefresh={loadData} customer={item}/>
                                    }
                                </th>
                            </tr>
                        )
                    })
                }
                </tbody>
            </table>
            {!isUndefined(data) && 
                <Pagination data={data} loading={loading} loadHandler={loadData} currentPage={currentPage}/>
            }
        </>
    );
}
