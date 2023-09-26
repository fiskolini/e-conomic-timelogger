import {GetServerSidePropsResult} from "next/types";
import {GetServerSidePropsContext} from "next";

import {Project} from "@/app/types/entities/Project";
import {getProjectById} from "@/app/api/projects";
import {createTime, getTimesByProjectId} from "@/app/api/times";
import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {Time} from "@/app/types/entities/Time";
import {useRouter} from "next/router";
import {MouseEvent, useState} from "react";
import isUndefined from "lodash/isUndefined";
import toast from "react-hot-toast";
import {ApiErrorResponse} from "@/app/types/api/response/ApiErrorResponse";
import PageHeader from "@/ui/components/PageHeader";
import TimesTable from "@/ui/components/Times/TimesTable";
import Link from "next/link";
import {isNull} from "lodash";

type Response = {
    project: Project,
    times: ApiPagedResponse<Time>
}


export async function getServerSideProps(context: GetServerSidePropsContext): Promise<GetServerSidePropsResult<Response>> {
    const {slug} = context.query;
    const request: ApiPagedRequest = {
        ...context.query
    }

    // Get project from slug and validate if exists
    const project = await getProjectById(Number(slug));

    if (project.status !== 200) {
        return {
            notFound: true
        }
    }

    const times = await getTimesByProjectId(project.data.id, request);

    return {
        props: {
            project: project.data,
            times: times.data
        }
    }
}

export default function TimesPage({project, times}: { project: Project, times: ApiPagedResponse<Time> }) {
    const router = useRouter();
    let [showDeletedState, setShowDeletedState] = useState<boolean>(false);
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
     * Add time handler
     */
    function handleAddTime(time?: string | undefined) {
        let newTime: number | null;
        let payload = {
            projectId: project.id,
            minutes: 0
        }

        const addedTime = prompt('Please insert the amount of time to allocate', time || '');

        // Handle cancel button
        if (addedTime === null) {
            return;
        }

        newTime = parseInt(addedTime);

        // Validate time
        if (isNaN(newTime) || newTime < 30) {
            alert("The time to allocate has to be a valid number greater or equal than 30");
            handleAddTime(addedTime);
            return;
        }

        payload.minutes = newTime;


        createTime(payload).then(() => {
            refreshState();
            toast.success("Time created successfully!");
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

    return (
        <>
            <PageHeader data={times} title='Times'>
                <span>From <Link href={`/projects/${project.customerId}`} className='font-bold underline'>{project.name}</Link> projct</span>
            </PageHeader>

            <div className="flex items-center my-6">
                <div className="w-1/2 space-x-2">
                    <button className={`${!isNull(project.completedAt) ? 'cursor-not-allowed bg-gray-500 hover:bg-gray-700' : 'cursor-pointer bg-blue-500 hover:bg-blue-700'} text-white font-bold py-2 px-4 rounded`}
                            onClick={() => handleAddTime()}
                            disabled={!isNull(project.completedAt)}>
                        Add entry
                    </button>

                    <button
                        className={`${showDeletedState ? 'bg-red-600 hover:bg-red-800' : 'bg-green-600 hover:bg-green-800'} text-white font-bold py-2 px-4 rounded`}
                        onClick={handleSoftDeleted}>
                        {showDeletedState ? 'Hide' : 'Show'} Deleted
                    </button>
                </div>
            </div>

            <TimesTable project={project} data={times} loading={loadingState} loadData={refreshState}
                        currentPage={currentPageState}/>
        </>
    )
};