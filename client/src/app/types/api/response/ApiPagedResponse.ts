export type ApiPagedResponse<T> = {
    data: T[],
    pageNumber: number,
    pageSize: number,
    totalItems: number,
    totalPages: number
}