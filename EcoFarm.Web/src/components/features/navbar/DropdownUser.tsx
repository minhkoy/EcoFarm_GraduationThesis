import NextUiDropdown from '@/components/ui/dropdown'
import useAuth from '@/hooks/auth/useAuth'
import { ACCOUNT_TYPE } from '@/utils/constants/enums'
import {
  Avatar,
  DropdownItem,
  DropdownMenu,
  DropdownTrigger
} from '@nextui-org/react'
import { LogOutIcon, ShoppingCartIcon, UserCog2Icon } from 'lucide-react'
import { useTranslation } from 'next-i18next'
import { useRouter } from 'next/router'
import { useState } from 'react'

export default function DropdownUser() {
  const { locale, replace, pathname, push } = useRouter()
  const { t } = useTranslation(['common'])
  // const token = getCookie(ACCESS_TOKEN)
  const [isOpen, setIsOpen] = useState(false)
  const { logOut, accountInfo } = useAuth()
  console.log(accountInfo)
  if (pathname.includes('seller') && accountInfo?.accountType === ACCOUNT_TYPE.CUSTOMER) {
    void replace('/error/forbidden');
  }
  if (!pathname.includes('seller') && accountInfo?.accountType === ACCOUNT_TYPE.SELLER) {
    void replace('/error/forbidden');
  }
  return (
    <NextUiDropdown
      isOpen={isOpen}
      onOpenChange={setIsOpen}
      closeOnSelect={false}
      placement='bottom-end'
    >
      <DropdownTrigger>
        <Avatar
          isBordered
          as='button'
          className='transition-transform'
          name={accountInfo?.fullName ?? accountInfo?.username ?? accountInfo?.email}
          size='sm'
          color='primary'
          src={accountInfo?.avatarUrl ?? 'http://placekitten.com/g/200/300'}
        />
      </DropdownTrigger>
      <DropdownMenu
        variant='flat'
        color='primary'
      // onAction={(key) =>
      //   key.toString() === 'locale' &&
      //   replace(pathname, undefined, {
      //     locale: locale === 'vi' ? 'en' : 'vi',
      //   })
      // }
      >
        <DropdownItem
          showDivider
          key='welcome-text'
          className='h-14 cursor-default gap-2'
        >
          <p className='font-semibold'>
            {//upperFirst(
              // t('welcome', {
              //   ns: 'common',
              //   name:
              //     accountInfo?.fullName ?? accountInfo?.username ?? accountInfo?.email,
              // }),
              `Xin chào ${accountInfo?.fullName ?? accountInfo?.username ?? accountInfo?.email}`
            }
          </p>
        </DropdownItem>
        {/* <DropdownItem
          key='locale'
          endContent={
            <Image
              src={
                locale === 'vi'
                  ? '/assets/flags/vi.png'
                  : '/assets/flags/en.png'
              }
              alt={cn('logo-', locale)}
              width={20}
              height={20}
            />
          }
        >
          {locale === 'vi' ? 'Tiếng Việt' : 'English'}
        </DropdownItem> */}
        <DropdownItem
          key='profile'
          endContent={<UserCog2Icon className='h-4 w-4' />}
          onClick={() => {
            if (accountInfo?.accountType === ACCOUNT_TYPE.CUSTOMER)
              void push(`/user/profile`)
            else if (accountInfo?.accountType === ACCOUNT_TYPE.SELLER) {
              void push(`/seller/profile`)
            }
          }}
        >
          {/* {capitalize(
            t('profile', {
              ns: 'common',
            }),
          )} */}
          Tài khoản
        </DropdownItem>
        {
          accountInfo?.accountType === ACCOUNT_TYPE.CUSTOMER && (
            <DropdownItem
              key={'cart'}
              endContent={<ShoppingCartIcon className='h-4 w-4' />}
              onClick={() => {
                void push(`/cart`)
              }}
            >
              Giỏ hàng
            </DropdownItem>
          )
        }
        <DropdownItem
          key='logout'
          color='danger'
          endContent={<LogOutIcon className='h-4 w-4' />}
          onClick={() => {
            logOut()
          }}
        >
          Đăng xuất
          {/* {capitalize(
            t('log-out', {
              ns: 'common',
            }),
          )} */}
        </DropdownItem>
      </DropdownMenu>
    </NextUiDropdown>
  )
}
