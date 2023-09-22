import {Base} from "@/app/models/Base";

export type Customer = Base & {
    name: string,
    numberOfProjects: number,
    totalTimeAllocated: number
}