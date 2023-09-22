import {ReactNode} from "react";

export default function Deadline({children}: { children: string | ReactNode }) {
    if (children === null || typeof children === "undefined") {
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