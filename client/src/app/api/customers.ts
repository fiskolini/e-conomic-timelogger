import {apiClient} from "@/app/api/apiClient";
import {AxiosResponse} from "axios";
import {ApiResponse} from "@/app/types/api/ApiResponse";
import {Customer} from "@/app/types/entities/Customer";


/**
 * Create a new customer given customer
 * @param name
 */
export function createCustomer(name: string): Promise<AxiosResponse<ApiResponse<Customer>>> {
    return apiClient.post<ApiResponse<Customer>>('/api/customers/', {name});
}

/**
 * Load all customers
 * @param page
 * @param needle
 */
export function getAllCustomers(page: number, needle: string | null): Promise<AxiosResponse<ApiResponse<Customer>>> {
    let url = `/api/customers?pageNumber=${page}`;
    if (needle !== null) {
        url += `&search=${needle}`
    }
    return apiClient.get<ApiResponse<Customer>>(url);
}

/**
 * Update given customer
 * @param id
 * @param data
 */
export function updateCustomer(id: number, data: Customer): Promise<AxiosResponse<ApiResponse<Customer>>> {
    return apiClient.patch<ApiResponse<Customer>>(`/api/customers/${id}`, data);
}

/**
 * Delete given customer
 * @param id
 */
export function deleteCustomer(id: number): Promise<AxiosResponse<ApiResponse<Customer>>> {
    return apiClient.delete<ApiResponse<Customer>>(`/api/customers/${id}`);
}