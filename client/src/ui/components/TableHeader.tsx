export default function TableHeader({items}: { items: string[] }) {
    return (
        <thead className="bg-gray-200">
        <tr>
            {
                items?.map(function (item, i) {
                    return (
                        <th key={i} className={`border px-4 py-2 ${i === 0 ? 'w-12' : ''}`}>
                            {item}
                        </th>
                    )
                })
            }
        </tr>
        </thead>
    )
}