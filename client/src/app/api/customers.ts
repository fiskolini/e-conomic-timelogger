import {apiClient} from "@/app/api/apiClient";
import {AxiosResponse} from "axios";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {Customer} from "@/app/types/entities/Customer";
import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";

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
    let params: ApiPagedRequest = {};

    if (r.pageNumber !== null) {
        params.pageNumber = r.pageNumber
    }

    if (r.search !== null) {
        params.search = r.search;
    }

    if (r.considerDeleted !== null) {
        params.considerDeleted = r.considerDeleted;
    }
    
    return apiClient.get<ApiPagedResponse<Customer>>('/api/customers', {params});
}

/**
 * Update given customer
 * @param id
 * @param data
 */
export function updateCustomer(id: number, data: Customer): Promise<AxiosResponse<ApiPagedResponse<Customer>>> {
    return apiClient.patch<ApiPagedResponse<Customer>>(`/api/customers/${id}`, data);
}

/**
 * Delete given customer
 * @param id
 */
export function deleteCustomer(id: number): Promise<AxiosResponse<ApiPagedResponse<Customer>>> {
    return apiClient.delete<ApiPagedResponse<Customer>>(`/api/customers/${id}`);
}