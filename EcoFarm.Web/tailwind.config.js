/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      backgroundImage: {
        'bg-auth': "url('assets/eco-background.webp')"
      }
    },
  },
  plugins: [],
}

