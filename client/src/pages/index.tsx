import {MouseEvent, useEffect, useState} from "react";
import dynamic from "next/dynamic";
import {AxiosResponse} from "axios";
import toast from "react-hot-toast";

import {createCustomer, getAllCustomers} from "@/app/api/customers";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {Customer} from "@/app/types/entities/Customer";
import {ApiErrorResponse} from "@/app/types/api/response/ApiErrorResponse";
import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";
import PageHeader from "@/ui/components/PageHeader";


const CustomersTable = dynamic(
    () => import('@/ui/components/Customers/CustomersTable').then(m => m.default));

export default function CustomersPage() {
    let [showDeletedState, setShowDeletedState] = useState<boolean>(false);
    let [searchState, setSearchState] = useState<string>('');
    let [dataState, setDataState] = useState<ApiPagedResponse<Customer>>();
    let [loadingState, setLoadingState] = useState<boolean>(false);
    let [currentPageState, setCurrentPageState] = useState<number>(1);

    useEffect(loadDataHandler, []);

    /**
     * Load data handler
     */
    function loadDataHandler(page = currentPageState) {
        setLoadingState(true);

        let request: ApiPagedRequest = {
            pageNumber: page,
            search: searchState,
            considerDeleted: showDeletedState
        }

        setTimeout(() => {
            getAllCustomers(request).then((res: AxiosResponse<ApiPagedResponse<Customer>>) => {
                setDataState(res.data);
                setCurrentPageState(res.data.pageNumber);
                setLoadingState(false);
            });
        }, 500);
    }

    /**
     * Add customer handler
     */
    function handleAddCustomer() {
        const newName = prompt('Please insert a name for a new customer');

        if (newName === null) {
            return;
        }

        if (newName.length === 0) {
            toast.error('Name is a mandatory field for a customer.')
            return;
        }

        createCustomer(newName).then(() => {
            loadDataHandler();
            toast.success("Customer created successfully!");
        }).catch(({response}: { response: ApiErrorResponse }) => {
            response.data.errors.forEach((e) => toast.error(e))
        });
    }

    /**
     * Handle search action
     */
    function handleSoftDeleted() {
        setShowDeletedState(showDeletedState = !showDeletedState);
        loadDataHandler();
    }

    /**
     * Handle search action
     * @param event
     */
    function handleSearch(event: MouseEvent<HTMLElement>) {
        event.preventDefault();
        
        // At this stage, we must reset page state
        // as someone could trigger the search functionality
        // having 'currentPageState' > 1
        loadDataHandler(1);
    }


    return (
        <>
            <PageHeader data={dataState} title='Customers'/>
            <div className="flex items-center my-6">
                <div className="w-1/2 space-x-2">
                    <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                            onClick={handleAddCustomer}>
                        Add entry
                    </button>

                    <button
                        className={`${showDeletedState ? 'bg-red-600 hover:bg-red-800' : 'bg-green-600 hover:bg-green-800'} text-white font-bold py-2 px-4 rounded`}
                        onClick={handleSoftDeleted}>
                        {showDeletedState ? 'Hide' : 'Show'} Deleted
                    </button>
                </div>

                <div className="w-1/2 flex justify-end">
                    <form>
                        <input
                            className="border border-gray-200 rounded-full placeholder-gray-400 py-2 px-4 "
                            placeholder="Search"
                            value={searchState}
                            onChange={(e) => setSearchState(e.target.value)}
                        />
                        <button
                            className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2"
                            type="submit"
                            onClick={handleSearch}
                        >
                            Search
                        </button>
                    </form>
                </div>
            </div>

            <CustomersTable data={dataState} currentPage={currentPageState} loading={loadingState}
                            loadData={loadDataHandler}/>
        </>
    );
}