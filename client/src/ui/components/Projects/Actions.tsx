import {MouseEvent} from "react";

import {Project} from "@/app/types/entities/Project";
import EditIcon from "@/ui/icons/edit";
import CheckIcon from "@/ui/icons/check";
import TimeIcon from "@/ui/icons/time";
import CrossIcon from "@/ui/icons/cross";
import {deleteProject, updateProject} from "@/app/api/projects";
import toast from "react-hot-toast";
import {isEmpty, isNull} from "lodash";
import Link from "next/link";

export default function Actions({project, handleRefresh, loading}: {
    project: Project,
    handleRefresh: Function,
    loading: boolean
}) {

    /**
     * Update project handler
     */
    function handleUpdate(name: MouseEvent<HTMLButtonElement> | string | null = null, deadline?: string) {
        if (loading) return;

        const newName = prompt(
            `Rename the '${project.name}' project`, typeof name === "string" && name.length > 0 ? name : project.name
        );

        // User pressed cancel
        if (isNull(newName)) {
            return;
        }

        if (isEmpty(newName) || newName.length < 3) {
            alert("The project 'Name' must be at least 3 characters");
            handleUpdate(newName);
            return;
        }

        const data = {
            ...project,
            name: newName
        }

        const newDeadline = prompt(
            `Set a new deadline for the '${project.name}' project`, project.deadline || ''
        );


        // User pressed cancel
        if (isNull(newDeadline)) {
            return;
        } else if (!isEmpty(newDeadline)) {
            data.deadline = newDeadline;
        } else {
            data.deadline = null;
        }

        console.log(data);


        if (data.name === project.name && newDeadline === project.deadline) {
            toast.success("No update will be done");
            return;
        }

        updateProject(data).then(() => {
            handleRefresh.call({})
            toast.success("Project updated successfully!");
        });
    }

    /**
     * Complete project handler
     */
    function handleComplete() {
        if (loading) return;

        if (!confirm(`Are you sure about marking "${project.name}" project as complete?`)) {
            return;
        }

        const data = {
            ...project,
            completedAt: new Date().toLocaleDateString()
        }

        updateProject(data).then(() => {
            handleRefresh.call({})
            toast.success("Project completed successfully!");
        });
    }

    /**
     * Delete project handler
     */
    function handleDeleteProject() {
        if (!confirm(`Are you sure about deleting "${project.name}" project?`)) {
            return;
        }

        deleteProject(project).then(() => {
            handleRefresh.call({})
            toast.success(`Project '${project.name}' deleted successfully!`)
        })
    }

    return (
        <div className='flex justify-around'>
            <button className="cursor-pointer text-gray-400 hover:text-blue-400"
                    onClick={handleUpdate}
            >
                <EditIcon/>
            </button>

            <button
                className={`${isNull(project.completedAt) ? 'cursor-pointer hover:text-green-600 text-gray-400' : 'cursor-not-allowed text-gray-200'} `}
                disabled={!isNull(project.completedAt)}
                onClick={handleComplete}
            >
                <CheckIcon/>
            </button>

            <Link href={`${project.id}/times`}
                  className='cursor-pointer hover:text-text-yellow-500-600 text-gray-400'>
                <TimeIcon/>
            </Link>

            <button className='cursor-pointer text-gray-400 hover:text-red-500'>
                <CrossIcon onClick={handleDeleteProject}/>
            </button>
        </div>
    )
}

