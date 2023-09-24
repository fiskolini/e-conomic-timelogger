import {MouseEvent, useState} from "react";
import {GetServerSidePropsResult} from "next/types";
import {useRouter} from "next/router";
import {GetServerSidePropsContext} from "next";
import toast from "react-hot-toast";

import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {createProject, getProjectsByCustomerId} from "@/app/api/projects";
import {getCustomerById} from "@/app/api/customers";
import {Project} from "@/app/types/entities/Project";
import {Customer} from "@/app/types/entities/Customer";
import PageHeader from "@/ui/components/PageHeader";
import ProjectsTable from "@/ui/components/Projects/ProjectsTable";
import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";
import isUndefined from "lodash/isUndefined";
import {isEmpty, isNull} from "lodash";
import {isValidFutureDate} from "@/app/libs/Time";
import {ApiErrorResponse} from "@/app/types/api/response/ApiErrorResponse";
import {CreateProjectRequest} from "@/app/types/entities/requests/CreateProjectRequest";

type Response = {
    data: ApiPagedResponse<Project>,
    customer: Customer
}


export async function getServerSideProps(context: GetServerSidePropsContext): Promise<GetServerSidePropsResult<Response>> {
    try {
        const {slug} = context.query;
        const request: ApiPagedRequest = {
            ...context.query
        }

        const customerData = await getCustomerById(Number(slug));
        const projectsList = await getProjectsByCustomerId(Number(slug), request);
        
        let response: Response = {
            data: projectsList.data,
            customer: customerData.data
        }

        return {
            props: response
        }
    } catch (error) {
        return {
            notFound: true
        }
    }
}

export default function ProjectsPage({data, customer}: { data: ApiPagedResponse<Project>, customer: Customer }) {
    const router = useRouter();
    let [showDeletedState, setShowDeletedState] = useState<boolean>(false);
    let [searchState, setSearchState] = useState<string>('');
    let [loadingState, setLoadingState] = useState<boolean>(false);
    let [currentPageState, setCurrentPageState] = useState<number>(1);


    function refreshState(page?: number | undefined) {
        let data: ApiPagedRequest = {};

        if (!isUndefined(page)) {
            data.pageNumber = page;
            setCurrentPageState(page);
        }

        changeRoute(data);
    }

    /**
     * Add project handler
     */
    function handleAddProject(defaultName?: string | undefined, defaultDeadline?: string | undefined) {
        let newName: string | null;
        let payload: CreateProjectRequest = {
            name: "",
            customerId: customer.id
        }

        if (isUndefined(defaultName)) {
            newName = prompt('Please insert a name for a the project to create.', defaultName || '');
        } else {
            // Prevent user typing again name after error on deadline
            newName = defaultName;
        }

        // Handle cancel button
        if (newName === null) {
            return;
        }

        // Handle invalid name
        if (isEmpty(newName) || newName.length < 3) {
            alert("The project 'Name' must be at least 3 characters");
            handleAddProject(newName);
            return;
        }

        payload.name = newName;

        const newDeadline = prompt('Set a new deadline for the new project', defaultDeadline);

        if (!isEmpty(newDeadline) && !isNull(newDeadline) && !isUndefined(newDeadline) && !isValidFutureDate(newDeadline)) {
            alert("The Deadline must be a valid future date in YYYY/MM/DD format.");
            handleAddProject(newName, newDeadline);
        } else if (!isEmpty(newDeadline)) {
            payload.deadline = newDeadline;
        } else {
            payload.deadline = null;
        }


        createProject(payload).then(() => {
            refreshState();
            toast.success("Project created successfully!");
        }).catch(({response}: { response: ApiErrorResponse }) => {
            response.data.errors.forEach((e) => toast.error(e))
        });
    }

    /**
     * Handle softDelete action
     */
    function handleSoftDeleted() {
        changeRoute({
            considerDeleted: !showDeletedState
        }).then(() => setShowDeletedState(!showDeletedState))
    }

    /**
     * Attach given args to the URL query params
     * @param data
     */
    async function changeRoute(data: object) {
        setLoadingState(true);
        await router.replace({
            pathname: router.pathname,
            query: {
                ...router.query,
                ...data
            }
        }, {}, {scroll: false});
        return setLoadingState(false);
    }

    /**
     * Handle search action
     * @param event
     */
    function handleSearch(event: MouseEvent<HTMLElement>) {
        event.preventDefault();
        changeRoute({search: searchState})
    }

    return (
        <>
            <PageHeader data={data} title='Projects'>
                <span>From <span className='font-bold'>{customer.name}</span> customer</span>
            </PageHeader>

            <div className="flex items-center my-6">
                <div className="w-1/2 space-x-2">
                    <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                            onClick={() => handleAddProject()}>
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

            <ProjectsTable data={data} loading={loadingState} loadData={refreshState}
                           currentPage={currentPageState}/>
        </>
    )
};