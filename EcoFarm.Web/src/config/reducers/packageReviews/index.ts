import { type QueryPackageReviews } from "@/models/packageReview.model";
import { type PayloadAction, createSlice } from "@reduxjs/toolkit";
import { merge } from "lodash-es";

const initialState: QueryPackageReviews = {
    packageId: '',
    rating: undefined,
    page: 1,
}

export const packageReviewsSlice = createSlice({
    name: 'packageReviews',
    initialState,
    reducers: {
        setFilterParams: (state, action: PayloadAction<typeof initialState>) => {
            state = merge(state, action.payload)
        },
    },
})

export const { setFilterParams } = packageReviewsSlice.actions