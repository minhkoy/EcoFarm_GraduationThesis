/** @type {import('prettier').Config & import('prettier-plugin-tailwindcss').PluginOptions} */
const config = {
  plugins: ['prettier-plugin-tailwindcss'],
  semi: false,
  singleQuote: true,
  jsxSingleQuote: true,
  endOfLine: 'crlf',
  tabWidth: 2,
  useTabs: false,
  printWidth: 80,
}

export default config
