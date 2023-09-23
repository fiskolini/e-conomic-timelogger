import {Base} from "@/app/types/entities/Base";

export type Customer = Base & {
    name: string,
    numberOfProjects: number,
    totalTimeAllocated: number
}