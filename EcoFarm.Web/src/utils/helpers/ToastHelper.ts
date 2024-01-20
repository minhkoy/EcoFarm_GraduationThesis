import { capitalize } from 'lodash-es'
import { toast } from 'sonner'
export const ToastHelper = {
  success: (message: string, description: string, duration?: number) => {
    toast.success(capitalize(message), {
      description: capitalize(description),
      dismissible: true,
      duration,
    })
  },
  error: (message: string, description: string, duration?: number) => {
    toast.error(capitalize(message), {
      description: capitalize(description),
      dismissible: true,
      duration,
    })
  },
  info: (message: string, description: string, duration?: number) => {
    toast.info(capitalize(message), {
      description: capitalize(description),
      dismissible: true,
      duration,
    })
  },
}
