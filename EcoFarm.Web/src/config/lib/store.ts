import { configureStore } from '@reduxjs/toolkit'
import logger from 'redux-logger'
import rootReducers from '../reducers'

const store = configureStore({
  reducer: rootReducers,
  devTools: process.env.NODE_ENV !== 'production',
  // eslint-disable-next-line @typescript-eslint/ban-ts-comment
  //@ts-expect-error
  //middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(logger),
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(logger)//.concat(notificationMiddleware),

})

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch

export default store
