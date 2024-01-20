import { type PropsWithChildren } from 'react'
import NoSSR from 'react-no-ssr'
import Header from './header'

const MainLayout = ({ children }: PropsWithChildren) => {
  return (
    <NoSSR>
      <div className='flex h-screen w-screen flex-col'>
        <Header />
        <section className='flex-grow'>{children}</section>
      </div>
    </NoSSR>
  )
}

export default MainLayout
