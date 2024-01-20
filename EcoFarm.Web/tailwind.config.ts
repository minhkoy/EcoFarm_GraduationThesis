import { nextui } from '@nextui-org/react'
import { type Config } from 'tailwindcss'
import { fontFamily } from 'tailwindcss/defaultTheme'

export default {
  content: [
    './src/**/*.tsx',
    './node_modules/@nextui-org/theme/dist/**/*.{js,ts,jsx,tsx}',
  ],
  theme: {
    extend: {
      fontFamily: {
        sans: ['var(--font-sans)', ...fontFamily.sans],
        mono: ['var(--font-mono)', ...fontFamily.mono],
      },
    },
  },
  darkMode: 'class',
  plugins: [
    nextui({
      prefix: 'eco-farm-ui',
      layout: {
        fontSize: {
          tiny: '11px',
          small: '13px',
          medium: '14px',
          large: '24px',
        },
      },
      themes: {
        light: {
          colors: {
            primary: {
              DEFAULT: '#44A093',
              foreground: '#f3faf8',
              '50': '#f3faf8',
              '100': '#d8efe9',
              '200': '#b0dfd5',
              '300': '#81c7ba',
              '400': '#57aa9d',
              '500': '#44a093',
              '600': '#2f726a',
              '700': '#295c56',
              '800': '#244b47',
              '900': '#223f3c',
            },
            secondary: {
              DEFAULT: '#ACC981',
              foreground: '#f5f8ed',
              '50': '#f5f8ed',
              '100': '#e7efd8',
              '200': '#d0e1b5',
              '300': '#acc981',
              '400': '#95b764',
              '500': '#779c46',
              '600': '#5b7b35',
              '700': '#475f2c',
              '800': '#3b4d27',
              '900': '#344225',
            },
          },
        },
        dark: {
          colors: {
            primary: {
              DEFAULT: '#44A093',
              foreground: '#f3faf8',
              '50': '#f3faf8',
              '100': '#d8efe9',
              '200': '#b0dfd5',
              '300': '#81c7ba',
              '400': '#57aa9d',
              '500': '#44a093',
              '600': '#2f726a',
              '700': '#295c56',
              '800': '#244b47',
              '900': '#223f3c',
            },
            secondary: {
              DEFAULT: '#ACC981',
              foreground: '#f5f8ed',
              '50': '#f5f8ed',
              '100': '#e7efd8',
              '200': '#d0e1b5',
              '300': '#acc981',
              '400': '#95b764',
              '500': '#779c46',
              '600': '#5b7b35',
              '700': '#475f2c',
              '800': '#3b4d27',
              '900': '#344225',
            },
          },
        },
      },
      addCommonColors: false,
    }),
  ],
} satisfies Config
