import {Project} from "@/app/types/entities/Project";
import {apiClient} from "@/app/api/apiClient";
import {AxiosResponse} from "axios";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";


/**
 * Load all projects from the API
 * @param page
 * @param customerId
 */
export function getProjectsByCustomerId(page: number, customerId: number): Promise<AxiosResponse<ApiPagedResponse<Project>>> {
    return apiClient.get<ApiPagedResponse<Project>>(`/api/customers/${customerId}/projects?pageNumber=${page}`);
}

/**
 * Delete given project
 * @param projectId
 */
export function deleteProject(projectId: number): Promise<AxiosResponse<ApiPagedResponse<Project>>> {
    return apiClient.delete<ApiPagedResponse<Project>>(`/api/projects/${projectId}`);
}
