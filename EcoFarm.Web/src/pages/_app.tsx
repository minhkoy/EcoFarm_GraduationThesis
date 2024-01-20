import Providers from '@/components/providers'
import '@/styles/globals.css'
import { MantineProvider } from '@mantine/core'
import '@mantine/core/styles.css'
import '@mantine/dates/styles.css'
import { type NextPage } from 'next'
import { appWithTranslation } from 'next-i18next'
import { type AppProps } from 'next/dist/shared/lib/router/router'
import Head from 'next/head'
import { type ReactElement, type ReactNode } from 'react'
import NoSSR from 'react-no-ssr'
import i18Config from '../../next-i18next.config.mjs'

export type NextPageWithLayout<P = unknown, IP = P> = NextPage<P, IP> & {
  getLayout?: (page: ReactElement) => ReactNode
}

type AppPropsWithLayout = AppProps & {
  Component: NextPageWithLayout
}

const MyApp = ({ Component, pageProps }: AppPropsWithLayout) => {
  // Use the layout defined at the page level, if available
  const getLayout = Component.getLayout ?? ((page) => page)
  return (
    <NoSSR>
      <MantineProvider>
        <Head>
          <title>Eco Farm</title>
          <link rel='icon' href='/favicon.ico' />
        </Head>
        <Providers>{getLayout(<Component {...pageProps} />)}</Providers>
      </MantineProvider>
    </NoSSR>
  )
}

export default appWithTranslation(MyApp, i18Config)
