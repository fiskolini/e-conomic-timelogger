import {getProjectsByCustomerId} from "@/app/api/projects";

export async function getServerSideProps(context) {
    const {slug} = context.query;

    const {data} = await getProjectsByCustomerId(1, slug);
    

    return { props: { data } }
}

export default function Home({data}) {
    console.log(arguments);
    return (
        <>
            Hello!
        </>
    );
}