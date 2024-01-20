import { type QueryPackages } from '@/models/package.model'
import { createSlice, type PayloadAction } from '@reduxjs/toolkit'
import { merge } from 'lodash-es'

const initialState: QueryPackages = {
  enterpriseId: '',
  isEnded: undefined,
  isStarted: undefined,
  priceFrom: 0,
  priceTo: 0,
  keyword: '',
  page: 1,
  limit: 10,
}

export const packageSlice = createSlice({
  name: 'package',
  initialState,
  reducers: {
    setFilterParams: (state, action: PayloadAction<typeof initialState>) => {
      state = merge(state, action.payload)
    },
  },
})

export const { setFilterParams } = packageSlice.actions
