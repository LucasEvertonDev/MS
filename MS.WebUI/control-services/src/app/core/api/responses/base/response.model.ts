export interface ResponseDto<TResponseDto> {
    success: boolean,
    httpCode: number,
    errors: error[],
    content: TResponseDto
}

export interface error {
    context: string,
    message: string
}