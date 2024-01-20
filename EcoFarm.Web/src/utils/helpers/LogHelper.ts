import { commonColors } from '@nextui-org/react'

type LogHelperFunc = {
  info: (message: string) => void
  error: (message: string) => void
  warn: (message: string) => void
}

export const LogHelper: LogHelperFunc = {
  info: (message) =>
    console.log('%c' + message, 'color:' + commonColors.cyan['900']),
  error: (message) =>
    console.log('%c' + message, 'color:' + commonColors.red['900']),
  warn: (message) =>
    console.log('%c' + message, 'color:' + commonColors.yellow['900']),
}
