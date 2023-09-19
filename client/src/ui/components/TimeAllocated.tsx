import {TimeAllocated} from "@/app/models/TimeAllocated";
import {parseTime} from "@/app/libs/Time";

export default function TimeAllocated(time: TimeAllocated) {
    return (
        <span>{parseTime(time.hours)}:{parseTime(time.minutes)}:
            <small className='text-xs'>{parseTime(time.seconds)}</small>
        </span>
    )
}