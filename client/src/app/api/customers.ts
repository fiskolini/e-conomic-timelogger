import {apiClient} from "@/app/api/apiClient";
import {AxiosResponse} from "axios";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {Customer} from "@/app/types/entities/Customer";
import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";
import {mapPagedRequest} from "@/app/libs/PagedRequestmapper";

/**
 * Create a new customer given customer
 * @param name
 */
export function createCustomer(name: string): Promise<AxiosResponse<ApiPagedResponse<Customer>>> {
    return apiClient.post<ApiPagedResponse<Customer>>('/api/customers/', {name});
}

/**
 * Load all customers
 * @param r
 */
export function getAllCustomers(r: ApiPagedRequest): Promise<AxiosResponse<ApiPagedResponse<Customer>>> {
    let params: ApiPagedRequest = mapPagedRequest(r);
    return apiClient.get<ApiPagedResponse<Customer>>('/api/customers', {params});
}

/**
 * Load customer
 * @param id
 */
export function getCustomerById(id: number): Promise<AxiosResponse<Customer>> {
    return apiClient.get<Customer>(`/api/customers/${id}`);
}

/**
 * Update given customer
 * @param data
 */
export function updateCustomer(data: Customer): Promise<AxiosResponse<Customer>> {
    return apiClient.patch<Customer>(`/api/customers/${data.id}`, data);
}

/**
 * Delete given customer
 * @param id
 */
export function deleteCustomer(id: number): Promise<AxiosResponse<ApiPagedResponse<Customer>>> {
    return apiClient.delete<ApiPagedResponse<Customer>>(`/api/customers/${id}`);
}