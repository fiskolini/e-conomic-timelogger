import {ReactNode} from "react";
import isUndefined from "lodash/isUndefined";

export default function Deadline({children}: { children: string | ReactNode }) {
    if (children === null || isUndefined(children)) {
        return 'N/A';
    }

    const d = new Date(children.toString() || '');
    let color = '';

    if (new Date() >= d) {
        color = 'text-red-400';
    }

    return (
        <span className={color}>
            {d.toDateString()}
        </span>
    )
}