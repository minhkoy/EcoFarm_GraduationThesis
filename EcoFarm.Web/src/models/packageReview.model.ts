import { type QueryRequest } from "./helpers/query.model";
import { type ResponseModel } from "./helpers/response.model";

export type PackageReviewModel = {
    reviewId?: string;
    packageId?: string;
    userId?: string;
    username?: string;
    userFullname?: string;
    enterpriseId?: string;
    content?: string;
    createdAt?: Date;
    modifiedAt?: Date;
    rating?: number;
}

export type ResponsePackageReviews = ResponseModel<Array<PackageReviewModel>>
export type ResponsePackageReview = ResponseModel<PackageReviewModel>
export type QueryPackageReviews = QueryRequest<{
    packageId?: string;
    rating?: number;
}>