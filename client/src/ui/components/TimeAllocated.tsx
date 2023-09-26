import {parseTime} from "@/app/libs/Time";

export default function TimeAllocated({time}: { time: number }) {
    const {hours, minutes} = parseTime(time);

    return (
        <span>
            {`${hours}h${minutes > 0 ? ` ${minutes}m` : ''}`}
            {time > 0 &&
                <small className='ml-2 text-xs font-light text-gray-400'>{time}m</small>
            }
        </span>
    )
}
