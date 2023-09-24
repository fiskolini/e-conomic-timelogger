import {Project} from "@/app/types/entities/Project";


export default function StatusLabel({project}: { project: Project }) {
    let text = 'Completed';
    let bg = 'bg-green-100';
    let color = 'text-green-800';
    const now = new Date();

    if (!project.completedAt) {
        if (project.deadline && new Date(project.deadline) < now) {
            text = 'Delayed';
            bg = 'bg-red-100';
            color = 'text-red-800';
        } else if (!project.deadline || new Date(project.deadline) > now) {
            text = 'Ongoing';
            bg = 'bg-gray-200';
            color = 'text-gray-800';
        }
    } else {
        text += ` at ${new Date(project.completedAt).toLocaleDateString()}`;
    }

    return (
        <span
            className={`${bg} ${color} text-xs font-medium mr-2 px-2.5 py-0.5 rounded`}>
            {text}
        </span>
    )
}