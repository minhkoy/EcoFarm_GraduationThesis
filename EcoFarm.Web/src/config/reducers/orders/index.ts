import { type QueryOrders } from '@/models/order.model'
import { EFX } from '@/utils/constants/constants'
import { createSlice, type PayloadAction } from '@reduxjs/toolkit'
import { merge } from 'lodash-es'

const initialState: QueryOrders = {
  createdFrom: undefined,
  createdTo: undefined,
  keyword: '',
  page: EFX.DEFAULT_PAGE,
  limit: EFX.DEFAULT_LIMIT,
  status: undefined,
}

export const orderSlice = createSlice({
  name: 'orders',
  initialState,
  reducers: {
    setOrderFilterParams: (state, action: PayloadAction<typeof initialState>) => {
      state = merge(state, action.payload)
    },
  },
})

export const { setOrderFilterParams } = orderSlice.actions
