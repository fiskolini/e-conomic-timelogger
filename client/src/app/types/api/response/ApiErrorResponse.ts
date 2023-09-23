export type ApiErrorResponse = {
    data: ApiErrorResponseData
}

export type ApiErrorResponseData = {
    statusCode: number,
    message: string,
    errors: string[]
}