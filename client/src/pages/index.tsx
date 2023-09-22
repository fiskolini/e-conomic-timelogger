import dynamic from "next/dynamic";
import {MouseEvent, useEffect, useState} from "react";
import {ApiResponse} from "@/app/types/api/ApiResponse";
import {Customer} from "@/app/types/entities/Customer";
import {createCustomer, getAllCustomers} from "@/app/api/customers";
import {AxiosResponse} from "axios";
import toast from "react-hot-toast";
import {ApiErrorResponse} from "@/app/types/api/ApiErrorResponse";

const Table = dynamic(
    () => import('@/ui/components/Customers/Table').then(m => m.default));

export default function Home() {
    const [searchState, setSearchState] = useState<string>('');
    const [dataState, setDataState] = useState<ApiResponse<Customer>>();
    const [loadingState, setLoadingState] = useState<boolean>(false);
    const [currentPageState, setCurrentPageState] = useState<number>(1);

    useEffect(loadDataHandler, []);

    /**
     * Load data handler
     * @param page
     */
    function loadDataHandler(page: number = currentPageState) {
        setLoadingState(true);
        setTimeout(() => {
            getAllCustomers(page, searchState).then((res: AxiosResponse<ApiResponse<Customer>>) => {
                setDataState(res.data);
                setCurrentPageState(res.data.pageNumber);
                setLoadingState(false);
            });
        }, 500);
    }
    
    function handleSearch(event: MouseEvent<HTMLElement>){
        event.preventDefault();
        loadDataHandler();
    }

    /**
     * Add customer handler
     */
    function addCustomerHandler() {
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


    return (
        <>
            <div className="flex items-center my-6">
                <div className="w-1/2">
                    <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                            onClick={addCustomerHandler}>
                        Add entry
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

            <Table data={dataState} currentPage={currentPageState} loading={loadingState} loadData={loadDataHandler}/>
        </>
    );
}