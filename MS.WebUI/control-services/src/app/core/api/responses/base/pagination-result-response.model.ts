export interface PaginationReuslt<T> {
    items: T[],
    pageNumber: number,
    pageSize: number,
    firstPage: number,
    lastPage: number,
    totalPages: number,
    totalElements: number,
    hasPreviousPage: boolean,
    hasNextPage: boolean
}