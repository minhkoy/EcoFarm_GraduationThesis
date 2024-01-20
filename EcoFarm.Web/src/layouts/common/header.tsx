import DropdownUser from '@/components/features/navbar/DropdownUser'
import Notification from '@/components/features/navbar/Notification'
import { SwitchTheme } from '@/components/ui/switch/SwitchTheme'
import {
  Link,
  Navbar,
  NavbarBrand,
  NavbarContent,
  NavbarItem,
  NavbarMenuToggle,
} from '@nextui-org/react'
import { capitalize } from 'lodash-es'
import { HomeIcon, MessagesSquareIcon, PackageIcon, ShoppingCartIcon, VeganIcon } from 'lucide-react'
import { useTranslation } from 'next-i18next'
import Image from 'next/image'
import { useRouter } from 'next/router'
import { useMemo } from 'react'
import SidebarMenu from './component/SidebarMenu'

const Header = () => {
  const { pathname } = useRouter()
  const { t } = useTranslation(['common'])
  const items = useMemo(
    () => [
      {
        id: 'homepage',
        label: 'Trang chủ', //t('homepage'),
        href: '/homepage',
        icon: <HomeIcon className='' />,
      },
      {
        id: 'packages',
        label: 'Gói farming',
        href: '/packages',
        icon: <PackageIcon />,
      },
      {
        id: 'products',
        label: 'Sản phẩm', //XXX
        href: '/products',
        icon: <VeganIcon />,
      },
      {
        id: 'cart',
        label: 'Giỏ hàng',
        href: '/cart',
        icon: <ShoppingCartIcon />,
      },
      {
        id: 'chat',
        label: 'Trò chuyện',
        href: '/chat',
        icon: <MessagesSquareIcon />
      }
    ],
    [t],
  )
  return (
    <Navbar
      isBordered
      classNames={{
        item: [
          'flex',
          'relative',
          'h-full',
          'items-center',
          "data-[active=true]:after:content-['']",
          'data-[active=true]:after:absolute',
          'data-[active=true]:after:bottom-0',
          'data-[active=true]:after:left-0',
          'data-[active=true]:after:right-0',
          'data-[active=true]:after:h-[2px]',
          'data-[active=true]:after:rounded-[2px]',
          'data-[active=true]:after:bg-primary',
        ],
      }}
    >
      <NavbarMenuToggle className='sm:hidden' />
      <NavbarBrand>
        <Image
          src={'/assets/brands/logo.png'}
          alt='Logo-brand'
          width={45}
          height={45}
        />
        <p className='font-bold text-inherit'>Eco Farm</p>
      </NavbarBrand>
      {/* Center menu */}
      <NavbarContent className='hidden gap-unit-lg sm:flex' justify='center'>
        {items.map((item) => (
          <NavbarItem key={item.id} isActive={pathname.includes(item.href)}>
            <Link
              color={pathname.includes(item.href) ? 'primary' : 'foreground'}
              href={item.href}
              className='inline-flex'
            >
              {item.icon}
              <span className='ml-2'>{capitalize(item.label)}</span>
            </Link>
          </NavbarItem>
        ))}
      </NavbarContent>
      {/* Left menu */}
      <NavbarContent justify='end'>
        <Notification />
        <SwitchTheme />
        <DropdownUser />
      </NavbarContent>
      {/* Sidebar */}
      <SidebarMenu />
    </Navbar>
  )
}

export default Header
