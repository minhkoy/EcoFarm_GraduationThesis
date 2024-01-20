// components/ThemeSwitcher.tsx
import { Button, cn, type ButtonVariantProps } from '@nextui-org/react'
import Image from 'next/image'
import { useRouter } from 'next/router'

type Props = ButtonVariantProps

export const SwitchLocale = ({ variant, ...props }: Props) => {
  const { replace, pathname, locale } = useRouter()
  return (
    <Button
      isIconOnly
      variant={variant ?? 'light'}
      color='primary'
      onClick={() =>
        replace(pathname, undefined, {
          locale: locale === 'vi' ? 'en' : 'vi',
        })
      }
      {...props}
    >
      <Image
        src={locale === 'vi' ? '/assets/flags/vi.png' : '/assets/flags/en.png'}
        alt={cn('logo-', locale)}
        width={20}
        height={20}
      />
    </Button>
  )
}
