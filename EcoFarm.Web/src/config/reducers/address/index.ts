import { type QueryAddresses } from '@/models/address.model'
import { EFX } from '@/utils/constants/constants'
import { createSlice, type PayloadAction } from '@reduxjs/toolkit'
import { merge } from 'lodash-es'

const initialState: QueryAddresses = {
  keyword: '',
  page: EFX.DEFAULT_PAGE,
  limit: EFX.DEFAULT_LIMIT,
}

export const addressSlice = createSlice({
  name: 'addresses',
  initialState,
  reducers: {
    setAddressFilterParams: (state, action: PayloadAction<typeof initialState>) => {
      state = merge(state, action.payload)
    },
  },
})

export const { setAddressFilterParams } = addressSlice.actions
