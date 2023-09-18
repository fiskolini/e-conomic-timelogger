import {Project} from "@/app/models/project";
import useRequest from "@/app/libs/useRequest";
import {Return} from "@/app/types/ApiReturnType";

export function getAllProjects(): Return<Project[], unknown> {
    return useRequest<Project[]>({
        url: '/api/projects'
    });
}
