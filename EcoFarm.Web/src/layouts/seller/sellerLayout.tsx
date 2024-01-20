import { type PropsWithChildren } from 'react'
import NoSSR from 'react-no-ssr'
import SellerHeader from './header'

const SellerLayout = ({ children }: PropsWithChildren) => {
  return (
    <NoSSR>
      <div className='flex h-screen w-screen flex-col'>
        <SellerHeader />
        <section className='flex-grow'>{children}</section>
      </div>
    </NoSSR>
  )
}

export default SellerLayout
