import { Toaster } from 'sonner'

export default function ToastProvider() {
  return (
    <Toaster
      duration={5000}
      richColors
      position='bottom-right'
      theme='system'
      closeButton
      visibleToasts={3}
      expand={false}
    />
  )
}
