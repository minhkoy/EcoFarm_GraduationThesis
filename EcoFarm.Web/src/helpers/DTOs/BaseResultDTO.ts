export default class BaseResultDTO<T> {
    public value?: T;
    public status?: number;
    public isSuccess?: boolean;
    public successMessage?: string;
    public correlationId?: string;
    public errors?: Array<string>;
    public validationErrors?: Array<ValidationError>;
}

class ValidationError {
    public identifier?: string;
    public errorMessage?: string;
    public errorCode?: string;
    public severity?: number;
}