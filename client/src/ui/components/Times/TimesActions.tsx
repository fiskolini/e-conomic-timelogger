import toast from "react-hot-toast";
import {isEmpty, isNull} from "lodash";

import {Project} from "@/app/types/entities/Project";
import EditIcon from "@/ui/icons/edit";
import CrossIcon from "@/ui/icons/cross";
import {Time} from "@/app/types/entities/Time";
import {deleteTime, updateTime} from "@/app/api/times";

export default function TimeActions({time, handleRefresh, loading, project}: {
    time: Time,
    handleRefresh: Function,
    loading: boolean,
    project: Project
}) {

    /**
     * Update handler
     */
    function handleUpdate(minutes: string | null = null) {
        if (loading) return;

        const newMinutes = prompt(
            `Update the minutes`, minutes || time.minutes.toString()
        );

        // User pressed cancel
        if (isNull(newMinutes)) {
            return;
        }

        if (isEmpty(newMinutes) || parseInt(newMinutes) < 30) {
            alert("Minutes should be greater or equal than 30");
            handleUpdate(newMinutes);
            return;
        }

        const data = {
            ...time,
            minutes: parseInt(newMinutes)
        }


        if (data.minutes === time.minutes) {
            toast.success("No update will be done");
            return;
        }

        updateTime(data).then(() => {
            handleRefresh.call({})
            toast.success("Time updated successfully!");
        });
    }

    /**
     * Delete handler
     */
    function hadleDelete() {
        if (!confirm(`Are you sure about deleting this time?`)) {
            return;
        }

        deleteTime(time).then(() => {
            handleRefresh.call({})
            toast.success(`Time deleted successfully!`)
        })
    }

    return (
        <div className='flex justify-around'>
            <button
                className={`${isNull(project.completedAt) ? 'cursor-pointer text-gray-400 hover:text-blue-400' : 'cursor-not-allowed text-gray-200'}`}
                onClick={() => handleUpdate()}
                disabled={!isNull(project.completedAt)}
            >
                <EditIcon/>
            </button>

            <button className='cursor-pointer text-gray-400 hover:text-red-500'>
                <CrossIcon onClick={hadleDelete}/>
            </button>
        </div>
    )
}

