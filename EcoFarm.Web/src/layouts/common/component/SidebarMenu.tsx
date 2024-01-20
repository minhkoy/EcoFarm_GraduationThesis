import { Link, NavbarMenu, NavbarMenuItem } from '@nextui-org/react'
import { capitalize } from 'lodash-es'
import { BellIcon, HomeIcon, PackageIcon, User2Icon } from 'lucide-react'
import { useRouter } from 'next/router'
import { useMemo } from 'react'
import { useTranslation } from 'react-i18next'

export default function SidebarMenu() {
  const { pathname } = useRouter()
  const { t } = useTranslation(['common'])
  const items = useMemo(
    () => [
      {
        id: 'homepage',
        label: t('homepage'),
        href: '/homepage',
        icon: <HomeIcon className='' />,
      },
      {
        id: 'packages',
        label: t('packages'),
        href: '/packages',
        icon: <PackageIcon />,
      },
      {
        id: 'notification',
        label: t('notification'),
        href: '/notification',
        icon: <BellIcon />,
      },
      {
        id: 'profile',
        label: t('profile'),
        href: '/profile',
        icon: <User2Icon />,
      },
    ],
    [t],
  )
  return (
    <NavbarMenu>
      {items.map((item) => (
        <NavbarMenuItem key={item.id} isActive={pathname.includes(item.href)}>
          <Link
            color={pathname.includes(item.href) ? 'primary' : 'foreground'}
            href={item.href}
            className='flex w-full justify-between'
            size='lg'
          >
            <span>{capitalize(item.label)}</span>
            {item.icon}
          </Link>
        </NavbarMenuItem>
      ))}
    </NavbarMenu>
  )
}
