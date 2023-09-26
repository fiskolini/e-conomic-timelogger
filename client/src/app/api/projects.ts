import {AxiosResponse} from "axios";

import {Project} from "@/app/types/entities/Project";
import {apiClient} from "@/app/api/apiClient";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";
import {mapPagedRequest} from "@/app/libs/PagedRequestmapper";

export const RESOURCE_URL = '/api/projects'

/**
 * Create a new Project
 * @param project
 */
export function createProject(project: {
    customerId: number,
    name: string;
    deadline?: string | null
}): Promise<AxiosResponse<Project>> {
    return apiClient.post<Project>(RESOURCE_URL, project);
}

/**
 * Load project by its given id
 * @param projectId
 */
export function getProjectById(projectId: number): Promise<AxiosResponse<Project>> {
    return apiClient.get<Project>(`${RESOURCE_URL}/${projectId}`);
}

/**
 * Load all projects from the API
 * @param customerId
 * @param request
 */
export function getProjectsByCustomerId(customerId: number, request: ApiPagedRequest): Promise<AxiosResponse<ApiPagedResponse<Project>>> {
    let params: ApiPagedRequest = mapPagedRequest(request);
    console.log(params, request);
    
    
    return apiClient.get<ApiPagedResponse<Project>>(`${RESOURCE_URL}?customerId=${customerId}`, {params});
}

/**
 * Update given project
 * @param project
 */
export function updateProject(project: Project): Promise<AxiosResponse<Project>> {
    return apiClient.patch<Project>(`${RESOURCE_URL}/${project.id}`, project);
}

/**
 * Delete given project
 * @param project
 */
export function deleteProject(project: Project): Promise<AxiosResponse<Project>> {
    return apiClient.delete<Project>(`${RESOURCE_URL}/${project.id}`);
}
