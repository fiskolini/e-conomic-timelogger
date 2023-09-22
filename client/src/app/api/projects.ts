import {Project} from "@/app/models/Project";
import {apiClient} from "@/app/api/apiClient";
import {AxiosResponse} from "axios";
import {ApiResponse} from "@/app/models/ApiResponse";


/**
 * Load all projects from the API
 * @param page
 * @param customerId
 */
export function getProjectsByCustomerId(page: number, customerId: number): Promise<AxiosResponse<ApiResponse<Project>>> {
    return apiClient.get<ApiResponse<Project>>(`/api/customers/${customerId}/projects?pageNumber=${page}`);
}

/**
 * Delete given project
 * @param projectId
 */
export function deleteProject(projectId: number): Promise<AxiosResponse<ApiResponse<Project>>> {
    return apiClient.delete<ApiResponse<Project>>(`/api/projects/${projectId}`);
}
