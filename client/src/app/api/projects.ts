import {Project} from "@/app/models/Project";
import {apiClient} from "@/app/api/apiClient";
import {AxiosResponse} from "axios";

type GetUsersResponse = {
    data: Project[];
};

export function getAllProjects(): Promise<AxiosResponse<GetUsersResponse>> {
    return apiClient.get<GetUsersResponse>('/api/projects');
}
