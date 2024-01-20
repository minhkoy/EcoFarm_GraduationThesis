import { getCookie } from 'cookies-next'
import type { NextRequest } from 'next/server'
import { NextResponse } from 'next/server'
import { type ResponseAccount } from './models/account.model'
import { ACCESS_TOKEN, ACCOUNT_TYPE } from './utils/constants/enums'
import { COMMON_LINK, LINK_AUTH } from './utils/constants/links'

export const middleware = (req: NextRequest) => {
  const res = NextResponse.next()
  if (req.url.includes('chat') || req.url.includes('error')) {
    console.log('happen..')
    return res
  }
  const hasCookie = getCookie(ACCESS_TOKEN, { req, res })
  const isAuthPage = req.url.includes('auth')
  if (!hasCookie && !isAuthPage) {
    return NextResponse.redirect(new URL(LINK_AUTH.LOGIN, req.url))
  } else if (hasCookie && isAuthPage) {
    return NextResponse.redirect(new URL(COMMON_LINK.HOMEPAGE, req.url))
  } else if (hasCookie && !isAuthPage) {
    fetch(`https://localhost:7019/api/Account/GetMyAccountInfo`, {
      headers: {
        'Authorization': `Bearer ${hasCookie}`
      }
    })
      .then((data) => data.json() as Promise<ResponseAccount>)
      //getMyAccountInfoApi()
      .then((data) => {
        const accountType = data.value.accountType;
        console.log('XXX' + accountType)
        if (accountType === ACCOUNT_TYPE.SELLER && !req.url.includes('seller')) {
          return NextResponse.redirect(new URL("/error/forbidden", req.url));
        }
        if (accountType === ACCOUNT_TYPE.CUSTOMER && req.url.includes('seller')) {
          console.log('test')
          return NextResponse.redirect(new URL("/error/forbidden", req.url));//, req.url));
        }
        else
          return res
      })
      .catch((err) => {
        console.log('XXX', err);
        return NextResponse.redirect(new URL(LINK_AUTH.LOGIN, req.url))
      })
    //return res
  } else {
    return res
  }
}

export const config = {
  matcher: '/((?!api|_next/static|_next/image|assets|favicon.ico).*)',
}
