import Link from "next/link";
import {useRouter} from "next/router";

import {GetServerSidePropsContext} from "next";
import {ApiPagedResponse} from "@/app/types/api/response/ApiPagedResponse";
import {getProjectsByCustomerId} from "@/app/api/projects";
import {GetServerSidePropsResult} from "next/types";
import {Project} from "@/app/types/entities/Project";


export async function getServerSideProps(context: GetServerSidePropsContext): Promise<GetServerSidePropsResult<ApiPagedResponse<Project>>> {
    try {
        const {slug, page= 1} = context.query;
        const {data} = await getProjectsByCustomerId(Number(page), Number(slug));

        return {props: data}
    } catch (error) {
        return {
            notFound: true
        }
    }
}

export default function Category(props: GetServerSidePropsResult<ApiPagedResponse<Project>>) {
    const {query} = useRouter();

    return (
        <code>
            <Link href={{query: {...query, page: 3}}}>
                ee
            </Link>
            {JSON.stringify(props)}
        </code>
    )
};