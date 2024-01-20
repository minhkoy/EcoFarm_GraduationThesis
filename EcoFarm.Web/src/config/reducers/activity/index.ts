import { type QueryActivities } from '@/models/activity.model'
import { createSlice, type PayloadAction } from '@reduxjs/toolkit'
import { merge } from 'lodash-es'

const initialState: QueryActivities = {
  packageId: '',
  fromDate: undefined,
  toDate: undefined,
  keyword: '',
  page: 1,
  limit: 10,
}

export const activitySlice = createSlice({
  name: 'activity',
  initialState,
  reducers: {
    setActivityFilterParams: (state, action: PayloadAction<typeof initialState>) => {
      state = merge(state, action.payload)
    },
  },
})

export const { setActivityFilterParams } = activitySlice.actions
