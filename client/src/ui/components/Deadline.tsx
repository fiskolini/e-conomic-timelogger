import {Project} from "@/app/models/Project";

export default function Deadline(project: Project) {
    if (project.deadline === null) {
        return 'N/A';
    }

    const d = new Date(project.deadline || '');
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