import {getAllProjects} from "@/app/api/projects";
import Spinner from "@/ui/icons/spinner";

export default function Table() {
    const {data, isLoading} = getAllProjects();
    const tableHeader = [
        '#', 'Project Name', 'abc', 'xyz'
    ];


    return (
        <table className="table-fixed w-full">
            <thead className="bg-gray-200">
            <tr>
                {
                    tableHeader?.map(function (item, i) {
                        return (
                            <th className={`border px-4 py-2 ${i === 0 ? 'w-12' : ''}`}>
                                {item}
                            </th>
                        )
                    })
                }
            </tr>
            </thead>
            <tbody className='text-gray-500'>

            {isLoading &&
                // Skeleton while loading in the background
                Array.from(Array(3)).map(function (ri) {
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
                data?.map(function (item, i) {
                    return (
                        <tr key={i} className='text-center border'>
                            <th className="border px-4 py-2 w-12">
                                {item.id}
                            </th>
                            <th className="border px-4 py-2 w-12">
                                {item.name}
                            </th>
                        </tr>
                    )
                })
            }
            </tbody>
        </table>
    );
}
