import {TimeAllocated} from "@/app/models/TimeAllocated";

export type Project = {
    id: number,
    name: string,
    completedAt: string,
    deadline?: string,
    timeAllocated: TimeAllocated
}