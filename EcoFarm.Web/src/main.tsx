import React from 'react'
import ReactDOM from 'react-dom/client'
import { NextUIProvider } from '@nextui-org/react'
import App from './App.tsx'
import './index.css'
//import 'tailwindcss/tailwind.css'
//import 'daisyui/dist/full.css'
//import { NextUIProvider } from '@nextui-org/react'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    {/* <NextUIProvider>
      <App />
    </NextUIProvider> */}
    <App />
  </React.StrictMode>,
)
