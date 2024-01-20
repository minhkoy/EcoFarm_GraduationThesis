import { format, formatDistance, parse } from 'date-fns'
import { enUS, vi } from 'date-fns/locale'

type DateFunction = (date: Date, formatStr?: string, locale?: string) => string

type DistanceFunction = (from: Date, to: Date, locale?: string) => string

type ParseFunction = (
  dateString: string,
  formatStr?: string,
  locale?: string,
) => Date

export const dateFormat: DateFunction = (date, formatStr = 'P', locale) =>
  format(date, formatStr, {
    locale: whichLocale(locale),
  })
  
export const dateFormatDistance: DistanceFunction = (from, to, locale) =>
  formatDistance(from, to, {
    locale: whichLocale(locale),
  })

export const dateParse: ParseFunction = (
  dateString,
  formatStr = 'P',
  locale,
) => {
  return parse(dateString, formatStr, new Date(), {
    locale: whichLocale(locale),
  })
}

const whichLocale = (locale?: string) => (locale === 'vi' ? vi : enUS)
