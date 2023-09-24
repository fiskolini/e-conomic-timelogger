import isUndefined from "lodash/isUndefined";

import Spinner from "@/ui/icons/spinner";
import {Project} from "@/app/types/entities/Project";
import TimeAllocated from "@/ui/components/Projects/TimeAllocated";
import StatusLabel from "@/ui/components/Projects/StatusLabel";
import Deadline from "@/ui/components/Projects/Deadline";
import Actions from "@/ui/components/Projects/Actions";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {NumberedArgumentFunction} from "@/app/types/handers/NumberedArgumentFunction";
import Pagination from "@/ui/components/Pagination";


export default function ProjectsTable({data, loading, loadData, currentPage}: {
    data: ApiPagedResponse<Project> | undefined,
    loading: boolean,
    loadData: NumberedArgumentFunction,
    currentPage: number
}) {
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
                                    {item.dateDeleted === null &&
                                        <Actions loading={loading} handleRefresh={loadData} project={item}/>
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
