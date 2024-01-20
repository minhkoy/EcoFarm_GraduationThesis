// components/ThemeSwitcher.tsx
import { Button, type ButtonVariantProps } from '@nextui-org/react'
import { MoonIcon, SunIcon } from 'lucide-react'
import { useTheme } from 'next-themes'
import { useEffect, useMemo, useState } from 'react'

type Props = ButtonVariantProps

export const SwitchTheme = ({ variant, ...props }: Props) => {
  const [mounted, setMounted] = useState(false)
  const { theme, setTheme, resolvedTheme } = useTheme()
  const isDark = useMemo(
    () => theme === 'dark' || resolvedTheme === 'dark',
    [resolvedTheme, theme],
  )
  // useEffect only runs on the client, so now we can safely show the UI
  useEffect(() => {
    setMounted(true)
  }, [])

  if (!mounted) {
    return null
  }

  return (
    <Button
      isIconOnly
      variant={variant ?? 'light'}
      color='primary'
      onClick={() => setTheme(isDark ? 'light' : 'dark')}
      {...props}
    >
      {isDark ? <MoonIcon /> : <SunIcon />}
    </Button>
  )
}
