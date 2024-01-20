import { isArray, isUndefined } from 'lodash-es'
import { type ParsedUrlQuery } from 'querystring'

export const splitDigits = (number?: number) => {
  if (!number) return 0
  const numberString = number.toString()

  // Use a regular expression to insert commas every three digits
  return numberString.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
}

export const getQueryUrlValue = (query: ParsedUrlQuery, index = 0) => {
  if (isArray(query.packageId)) {
    return isUndefined(query.packageId[index])
      ? ''
      : query.packageId[index]
  } else {
    return isUndefined(query.packageId) ? '' : query.packageId
  }
}
