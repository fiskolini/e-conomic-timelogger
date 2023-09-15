const BASE_URL = process.env.NEXT_PUBLIC_BACKEND_URL;

export async function getAll() {
    const response = await fetch(`${BASE_URL}/projects`);
    return response.json();
}
