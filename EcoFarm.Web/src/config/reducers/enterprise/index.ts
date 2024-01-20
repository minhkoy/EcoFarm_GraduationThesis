import { type QueryEnterprises } from '@/models/enterprise.model'
import { EFX } from '@/utils/constants/constants'
import { createSlice, type PayloadAction } from '@reduxjs/toolkit'
import { merge } from 'lodash-es'

const initialState: QueryEnterprises = {
  keyword: '',
  page: EFX.DEFAULT_PAGE,
  limit: EFX.DEFAULT_LIMIT,
  id: '',
  accountId: '',
}

export const enterpriseSlice = createSlice({
  name: 'enterprises',
  initialState,
  reducers: {
    setEnterpriseFilterParams: (state, action: PayloadAction<typeof initialState>) => {
      state = merge(state, action.payload)
    },
  },
})

export const { setEnterpriseFilterParams } = enterpriseSlice.actions
