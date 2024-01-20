import { NextUIProvider } from '@nextui-org/react'
import { ThemeProvider } from 'next-themes'
import { useRouter } from 'next/router'
import { type ReactNode } from 'react'

export default function UiProvider({ children }: { children: ReactNode }) {
  const { push, locale } = useRouter()
  return (
    <NextUIProvider navigate={push} locale={locale}>
      <ThemeProvider
        attribute='class'
        storageKey='eco-farm-theme'
        enableColorScheme
        enableSystem
      >
        {children}
      </ThemeProvider>
    </NextUIProvider>
  )
}
