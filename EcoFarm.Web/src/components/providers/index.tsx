import { nextFonts } from '@/config/lib/fonts'
import { cn } from '@nextui-org/react'
import { type PropsWithChildren } from 'react'
import ReactQueryProvider from './ReactQueryProvider'
import ReduxProvider from './ReduxProvider'
import ToastProvider from './ToastProvider'
import UiProvider from './UiProvider'

export default function Providers({ children }: PropsWithChildren) {
  return (
    <ReduxProvider>
      <ReactQueryProvider>
        <UiProvider>
          <main
            className={cn(
              nextFonts,
              'relative scroll-smooth font-sans text-foreground antialiased selection:bg-primary selection:text-primary-foreground bg-background',
            )}
          >
            {children}
          </main>
        </UiProvider>
        <ToastProvider />
      </ReactQueryProvider>
    </ReduxProvider>
  )
}
