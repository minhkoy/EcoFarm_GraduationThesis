import { ToastHelper } from '@/utils/helpers/ToastHelper'
import {
  MutationCache,
  QueryCache,
  QueryClient,
  QueryClientProvider,
  keepPreviousData,
} from '@tanstack/react-query'
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import axios from 'axios'
import { useState, type ReactNode } from 'react'

export default function ReactQueryProvider({
  children,
}: {
  children: ReactNode
}) {
  const [queryClient] = useState(
    () =>
      new QueryClient({
        defaultOptions: {
          queries: {
            staleTime: 1000 * 60 * 1,
            refetchIntervalInBackground: true,
            placeholderData: keepPreviousData,
            retryDelay: 2000,
            retry(failureCount) {
              return failureCount < 2
            },
          },
        },
        queryCache: new QueryCache({
          onError: (err) => {
            if (axios.isAxiosError(err)) return // Ignore axios error
            ToastHelper.error(err.name, err.message)
          },
        }),
        mutationCache: new MutationCache({
          onError: (err) => {
            if (axios.isAxiosError(err)) return // Ignore axios error
            ToastHelper.error(err.name, err.message)
          },
        }),
      }),
  )
  return (
    <QueryClientProvider client={queryClient}>
      {children}
      <ReactQueryDevtools
        initialIsOpen={false}
        position='bottom'
        buttonPosition='bottom-right'
      />
    </QueryClientProvider>
  )
}
