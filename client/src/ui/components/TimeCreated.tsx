import {DateTimeFormatOptions} from 'intl';


export default function TimeCreated({time}: { time: string }) {
    const date = new Date(time);
    const options: DateTimeFormatOptions = {
        year: "numeric",
        month: "short",
        day: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
        
    };
    
    return (
        <span className=''>
            {date.toLocaleString(undefined, options)}
        </span>
    )
}
