import {AxiosResponse} from "axios";
import {Project} from "@/app/types/entities/Project";
import {apiClient} from "@/app/api/apiClient";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";
import {mapPagedRequest} from "@/app/libs/PagedRequestmapper";

/**
 * Create a new Project
 * @param project
 */
export function createProject(project: {
    customerId: number,
    name: string;
    deadline?: string | null
}): Promise<AxiosResponse<ApiPagedResponse<Project>>> {
    return apiClient.post<ApiPagedResponse<Project>>(`/api/customers/${project.customerId}/projects`, project);
}

/**
 * Load all projects from the API
 * @param customerId
 * @param request
 */
export function getProjectsByCustomerId(customerId: number, request: ApiPagedRequest): Promise<AxiosResponse<ApiPagedResponse<Project>>> {
    let params: ApiPagedRequest = mapPagedRequest(request);
    return apiClient.get<ApiPagedResponse<Project>>(`/api/customers/${customerId}/projects`, {params});
}

/**
 * Update given project
 * @param data
 */
export function updateProject(data: Project): Promise<AxiosResponse<Project>> {
    return apiClient.patch<Project>(`/api/customers/${data.customerId}/projects/${data.id}`, data);
}

/**
 * Delete given project
 * @param project
 */
export function deleteProject(project: Project): Promise<AxiosResponse<ApiPagedResponse<Project>>> {
    return apiClient.delete<ApiPagedResponse<Project>>(`/api/customers/${project.customerId}/projects/${project.id}`);
}
