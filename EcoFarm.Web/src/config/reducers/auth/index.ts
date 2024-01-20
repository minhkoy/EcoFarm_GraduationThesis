import { createSlice, type PayloadAction } from '@reduxjs/toolkit'

const initialState = {
  taxCode: '',
}

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setTaxCode: (state, action: PayloadAction<string>) => {
      state.taxCode = action.payload
    },
  },
})

export const { setTaxCode } = authSlice.actions
