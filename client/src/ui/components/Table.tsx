import Spinner from "@/ui/icons/spinner";
import {getAllProjects} from "@/app/api/projects";
import {createFetch} from "@/app/api/createFetch";
import {useEffect, useState} from "react";
import {Project} from "@/app/models/Project";
import TimeAllocated from "@/ui/components/TimeAllocated";
import StatusLabel from "@/ui/components/StatusLabel";
import Deadline from "@/ui/components/Deadline";
import Actions from "@/ui/components/Actions";


export default function Table() {
    const dataFetch = createFetch();
    const [data, setData] = useState<Project[]>([]);

    useEffect(loadAllData, []);

    function loadAllData() {
        dataFetch(() => getAllProjects()).then(setData);
    }

    const tableHeader = [
        '#', 'Project Name', 'Deadline', 'Time allocated', 'Status', 'Actions'
    ];

    return (
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

            {data.length === 0 &&
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
                data.map(function (item, i) {
                    return (
                        <tr key={i} className='text-center border'>
                            <th className='border px-4 py-2 text-gray-400 text-xs'>
                                {item.id}
                            </th>
                            <th className="border px-4 py-2">
                                {item.name}
                            </th>
                            <th className="border px-4 py-2">
                                <Deadline {...item}/>
                            </th>
                            <th className="border px-4 py-2">
                                <TimeAllocated {...item.timeAllocated}/>
                            </th>
                            <th className="border px-4 py-2">
                                <StatusLabel {...item}/>
                            </th>
                            <th className="border px-4 py-2">
                                <Actions {...item}/>
                            </th>
                        </tr>
                    )
                })
            }
            </tbody>
        </table>
    );
}
