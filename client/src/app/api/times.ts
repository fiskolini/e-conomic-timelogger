import {AxiosResponse} from "axios";

import {apiClient} from "@/app/api/apiClient";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";
import {mapPagedRequest} from "@/app/libs/PagedRequestmapper";
import {Time} from "@/app/types/entities/Time";

export const RESOURCE_URL = '/api/times'

/**
 * Create a new time
 * @param project
 */
export function createTime(project: {
    projectId: number,
    minutes: number
}): Promise<AxiosResponse<Time>> {
    return apiClient.post<Time>(RESOURCE_URL, project);
}

/**
 * Load time by its given id
 * @param id
 */
export function getTimeById(id: number): Promise<AxiosResponse<Time>> {
    return apiClient.get<Time>(`${RESOURCE_URL}/${id}`);
}

/**
 * Load all times from the API
 * @param projectId
 * @param request
 */
export function getTimesByProjectId(projectId: number, request: ApiPagedRequest): Promise<AxiosResponse<ApiPagedResponse<Time>>> {
    let params: ApiPagedRequest = mapPagedRequest(request);
    return apiClient.get<ApiPagedResponse<Time>>(`${RESOURCE_URL}?projectId=${projectId}`, {params});
}

/**
 * Update given time
 * @param time
 */
export function updateTime(time: Time): Promise<AxiosResponse<Time>> {
    return apiClient.patch<Time>(`${RESOURCE_URL}/${time.id}`, time);
}

/**
 * Delete given time
 * @param time
 */
export function deleteTime(time: Time): Promise<AxiosResponse<ApiPagedResponse<Time>>> {
    return apiClient.delete<ApiPagedResponse<Time>>(`${RESOURCE_URL}/${time.id}`);
}
