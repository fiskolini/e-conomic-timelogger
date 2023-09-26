import {ReactNode} from "react";
import isUndefined from "lodash/isUndefined";

export default function Deadline({children}: { children: string | ReactNode }) {
    if (children === null || isUndefined(children)) {
        return 'N/A';
    }

    const date = new Date(children.toString() || '');
    const today = new Date();
    let color = '';


    if (date <= today) {
        color = 'text-red-400';
    }

    return (
        <span className={color}>
            {date.toDateString()}
        </span>
    )
}