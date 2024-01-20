import { type QueryRequest } from "./helpers/query.model"
import { type ResponseModel } from "./helpers/response.model"

export type ActivityModel = {
  id: string
  code: string
  title: string
  shortDescription: string
  description: string
  packageId: string
  packageCode: string
  packageName: string
  createdTime: Date
  createdBy: string
  mediaUrl: Array<string>
  medias: Array<ActivityMedia>
}

export type ActivityMedia = {
  imageUrl: string
}

export type ResponseActivities = ResponseModel<Array<ActivityModel>>
export type ResponseActivity = ResponseModel<ActivityModel>

export type QueryActivities = QueryRequest<{
  id?: string;
  packageId?: string;
  keyword?: string;
  fromDate?: string;
  toDate?: string;
}>