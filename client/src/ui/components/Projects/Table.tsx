import Spinner from "@/ui/icons/spinner";
import {getProjectsByCustomerId} from "@/app/api/projects";
import {useEffect, useState} from "react";
import {Project} from "@/app/models/Project";
import TimeAllocated from "@/ui/components/TimeAllocated";
import StatusLabel from "@/ui/components/StatusLabel";
import Deadline from "@/ui/components/Deadline";
import Actions from "@/ui/components/Actions";
import {ApiResponse} from "@/app/models/ApiResponse";
import {AxiosResponse} from "axios";


export default function Table() {
    const [data, setData] = useState<ApiResponse<Project>>();
    const [loading, setLoading] = useState<boolean>(false);
    const [currentPage, setCurrentPage] = useState<number>(1);


    useEffect(loadData, []);

    function loadData(page: number = currentPage) {
        setLoading(true);
        setTimeout(() => {
            getProjectsByCustomerId(page).then((res: AxiosResponse<ApiResponse<Project>>) => {
                setData(res.data);
                setCurrentPage(res.data.pageNumber);
                setLoading(false);
            });
        }, 500);
    }

    const tableHeader = [
        '#', 'Project Name', 'Deadline', 'Time allocated', 'Status', 'Actions'
    ];

    return (
        <>
            <table className="table-fixed w-full">
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
                                    <Deadline>
                                        {item.deadline}
                                    </Deadline>
                                </th>
                                <th className="border px-4 py-2">
                                    <TimeAllocated time={item.timeAllocated}/>
                                </th>
                                <th className="border px-4 py-2">
                                    <StatusLabel project={item}/>
                                </th>
                                <th className="border px-4 py-2">
                                    <Actions handleRefresh={loadData} project={item}/>
                                </th>
                            </tr>
                        )
                    })
                }
                </tbody>
            </table>
            {typeof data !== "undefined" &&

                <div
                    className="px-5 py-5 bg-white border-t flex flex-col xs:flex-row items-center xs:justify-between">
                        <span className="text-xs xs:text-sm text-gray-900">
                            Showing 1 to {data?.data.length} of {data?.totalItems} Entries
                        </span>
                    <div className={`inline-flex mt-2 xs:mt-0 ${loading ? 'cursor-wait' : 'cursor-pointer'}`}>
                        <button
                            onClick={() => loadData(currentPage - 1)}
                            className={`text-sm bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold py-2 px-4 rounded-l ${currentPage - 1 === 0 ? 'cursor-not-allowed opacity-50' : ''}`}
                            disabled={loading || currentPage - 1 === 0}
                        >
                            &lt; Prev
                        </button>
                        <button
                            onClick={() => loadData(currentPage + 1)}
                            className={`text-sm bg-gray-300 hover:bg-gray-400 text-gray-800 font-semibold py-2 px-4 rounded-r  ${currentPage === data.totalPages ? 'cursor-not-allowed opacity-50' : ''}`}
                            disabled={loading || currentPage + 1 > data.totalPages}
                        >
                            Next &gt;
                        </button>
                    </div>
                </div>
            }
        </>
    );
}
