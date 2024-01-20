import { type RootState } from '@/config/lib/store'
import type { TypedUseSelectorHook } from 'react-redux'
import { useSelector } from 'react-redux'

// Use throughout your app instead of plain `useDispatch` and `useSelector`
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector
