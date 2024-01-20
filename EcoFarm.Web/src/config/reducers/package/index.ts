import { createSlice, type PayloadAction } from '@reduxjs/toolkit';
import type { QuerySinglePackage } from 'src/models/package.model';

const initialState: QuerySinglePackage = {
  id: '',
  code: '',
}

export const singlePackageSlice = createSlice({
  name: 'singlePackage',
  initialState,
  reducers: {
    setPackageId: (state, action: PayloadAction<string>) => {
      //if (!state.id && !state.code) return;
      state.id = action.payload
      //state.code = action.payload
    }
  }
})

export const { setPackageId } = singlePackageSlice.actions
