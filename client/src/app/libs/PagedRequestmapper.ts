import {ApiPagedRequest} from "@/app/types/api/request/ApiPagedRequest";

export function mapPagedRequest(request: ApiPagedRequest) {
    let params: ApiPagedRequest = {};

    if (request.pageNumber !== null) {
        params.pageNumber = request.pageNumber
    }

    if (request.search !== null) {
        params.search = request.search;
    }

    if (request.considerDeleted !== null) {
        params.considerDeleted = request.considerDeleted;
    }

    return params;
}