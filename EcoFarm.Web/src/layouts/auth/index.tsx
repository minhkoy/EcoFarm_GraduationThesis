import { SwitchLocale } from '@/components/ui/switch/SwitchLocale'
import { SwitchTheme } from '@/components/ui/switch/SwitchTheme'
import { Card } from '@nextui-org/react'
import { type ReactNode } from 'react'

export default function AuthLayout({ children }: { children: ReactNode }) {
  return (
    <div className='relative flex h-screen w-screen items-center justify-center bg-gradient-to-br from-[#2A9476] to-[#195658]'>
      <Card className='h-screen w-full max-w-2xl animate-appearance-in rounded-none p-3 sm:rounded-md md:h-fit md:w-1/2 md:max-w-lg'>
        {children}
      </Card>
      <div className='hidden animate-appearance-in md:absolute md:bottom-5 md:left-5 md:inline-flex md:flex-col md:gap-4'>
        <SwitchTheme radius='full' variant='shadow' color='default' />
        <SwitchLocale radius='full' variant='shadow' color='default' />
      </div>
    </div>
  )
}
