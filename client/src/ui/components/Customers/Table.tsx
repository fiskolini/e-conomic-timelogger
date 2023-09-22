import Spinner from "@/ui/icons/spinner";
import Actions from "@/ui/components/Customers/Actions";
import {ApiResponse} from "@/app/types/api/ApiResponse";
import {Customer} from "@/app/types/entities/Customer";
import Pagination from "@/ui/components/Pagination";


export default function Table({data, loading, loadData, currentPage}: {
    data: ApiResponse<Customer> | undefined,
    loading: boolean,
    loadData: Function,
    currentPage: number
}) {
    const tableHeader = [
        '#', 'Customer Name', 'N. Projects', 'Total Time Allocated', 'Actions'
    ];

    return (
        <>
            <table className={`table-fixed w-full ${loading ? 'opacity-50' : ''}`}>
                <thead className="bg-gray-200">
                <tr>
                    {
                        tableHeader?.map(function (item, i) {
                            return (
                                <th key={i} className={`border px-4 py-2 ${i === 0 ? 'w-12' : ''}`}>
                                    {item}
                                </th>
                            )
                        })
                    }
                </tr>
                </thead>
                <tbody className='text-gray-500'>

                {typeof data === "undefined" &&
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
                            <tr key={i} className='text-center border'>
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
                                    <Actions handleRefresh={loadData} customer={item}/>
                                </th>
                            </tr>
                        )
                    })
                }
                </tbody>
            </table>
            {typeof data !== "undefined" &&
                <Pagination data={data} loading={loading} loadHandler={loadData} currentPage={currentPage}/>
            }
        </>
    );
}
