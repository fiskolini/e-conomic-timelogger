import {Base} from "@/app/types/entities/Base";

export type Time = Base & {
    id: number,
    projectId: number,
    minutes: number
}