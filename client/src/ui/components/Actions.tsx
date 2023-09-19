import {Project} from "@/app/models/Project";
import EditIcon from "@/ui/icons/edit";
import CheckIcon from "@/ui/icons/check";
import TimeIcon from "@/ui/icons/time";
import CrossIcon from "@/ui/icons/cross";

export default function Actions(project: Project) {
    return (
        <div className='flex justify-around'>
            <a className="cursor-pointer text-gray-400 hover:text-blue-400" target="_blank">
                <EditIcon/>
            </a>

            <a className="cursor-pointer text-gray-400 hover:text-green-600" target="_blank">
                <CheckIcon/>
            </a>
            
            <a className="cursor-pointer text-gray-400 hover:text-yellow-500" target="_blank">
                <TimeIcon/>
            </a>
            
            <a className="cursor-pointer text-gray-400 hover:text-red-500" target="_blank">
                <CrossIcon/>
            </a>
        </div>
    )
}

