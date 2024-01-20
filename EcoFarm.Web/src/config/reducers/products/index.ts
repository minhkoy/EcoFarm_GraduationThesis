import { type QueryProducts } from "@/models/product.model"
import { createSlice, type PayloadAction } from "@reduxjs/toolkit"
import { merge } from "lodash-es"

const initialState: QueryProducts = {
  enterpriseId: '',
  isActive: true,
  keyword: '',
  page: 1,
  limit: 10,
  packageId: '',
  minimumQuantity: 0,
  maximumQuantity: 0,
  minimumPrice: 0,
  maximumPrice: 0,

}

export const productSlice = createSlice({
  name: 'product',
  initialState,
  reducers: {
    setFilterParams: (state, action: PayloadAction<typeof initialState>) => {
      state = merge(state, action.payload)
    },
  },
})

export const { setFilterParams } = productSlice.actions