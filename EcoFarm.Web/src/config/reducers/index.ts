import { combineReducers } from '@reduxjs/toolkit'
import { activitySlice } from './activity'
import { addressSlice } from './address'
import { authSlice } from './auth'
import { enterpriseSlice } from './enterprise'
import { orderSlice } from './orders'
import { singlePackageSlice } from './package'
import { packageReviewsSlice } from './packageReviews'
import { packageSlice } from './packages'
import { productSlice } from './products'

const rootReducers = combineReducers({
  // Add reducers here
  [packageSlice.name]: packageSlice.reducer,
  [authSlice.name]: authSlice.reducer,
  [singlePackageSlice.name]: singlePackageSlice.reducer,
  [packageReviewsSlice.name]: packageReviewsSlice.reducer,
  [productSlice.name]: productSlice.reducer,
  [activitySlice.name]: activitySlice.reducer,
  [orderSlice.name]: orderSlice.reducer,
  [addressSlice.name]: addressSlice.reducer,
  [enterpriseSlice.name]: enterpriseSlice.reducer,
})

export default rootReducers
