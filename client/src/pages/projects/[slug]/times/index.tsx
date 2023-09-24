import {GetServerSidePropsResult} from "next/types";
import {GetServerSidePropsContext} from "next";

import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {Project} from "@/app/types/entities/Project";
import {Customer} from "@/app/types/entities/Customer";

type Response = {
    data: ApiPagedResponse<Project>,
    customer: Customer
}


export async function getServerSideProps(context: GetServerSidePropsContext): Promise<GetServerSidePropsResult<Response>> {
    const {slug} = context.query;
    try {
        console.log(context);

        return {
            props: {
                data: context.query
            }
        }
    } catch (error) {
        return {
            notFound: true
        }
    }
}

export default function TimesPage({data, customer}: { data: ApiPagedResponse<Project>, customer: Customer }) {

    return (
        <>
            {JSON.stringify(data)};
        </>
    )
};