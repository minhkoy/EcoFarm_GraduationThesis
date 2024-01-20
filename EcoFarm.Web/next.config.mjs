/**
 * Run `build` or `dev` with `SKIP_ENV_VALIDATION` to skip env validation. This is especially useful
 * for Docker builds.
 */
await import('./src/env.js')
import i18Config from './next-i18next.config.mjs'

/** @type {import("next").NextConfig} */
const config = {
  reactStrictMode: true,
  typescript: {
    ignoreBuildErrors: true,
  },
  redirects: async () => {
    return [
      {
        source: '/',
        destination: '/homepage',
        permanent: true,
      },
    ]
  },
  i18n: i18Config.i18n,
}

export default config
