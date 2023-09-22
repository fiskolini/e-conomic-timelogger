import {Project} from "@/app/models/Project";


export default function StatusLabel({project}: { project: Project }) {
    let text = '';
    let bg = '';
    let color = '';

    if (
        (project.completedAt !== null && new Date(project.completedAt) < new Date())
        || project.completedAt === null && project.deadline !== null && new Date(project.deadline || '') < new Date()
    ) {
        text = 'Delayed';
        bg = 'bg-red-100';
        color = 'text-red-800';
    } else if (project.deadline === null) {
        text = 'Ongoing';
        bg = 'bg-gray-200';
        color = 'text-gray-800';
    } else {
        text = 'Complete';
        bg = 'bg-green-100';
        color = 'text-green-800';
    }

    return (
        <span
            className={`${bg} ${color} text-xs font-medium mr-2 px-2.5 py-0.5 rounded`}>
            {text}
        </span>
    )
}