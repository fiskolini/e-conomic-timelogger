import {Base} from "@/app/types/entities/Base";

export type Project = Base & {
    id: number,
    name: string,
    completedAt?: string | null,
    deadline?: string | null,
    timeAllocated: number,
    customerId: number,
}