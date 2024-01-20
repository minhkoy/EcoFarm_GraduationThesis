import {
  Button,
  Popover,
  PopoverContent,
  PopoverTrigger,
  type PopoverProps,
} from '@nextui-org/react'
import { CalendarIcon } from 'lucide-react'
import { Calendar, type CalendarProps } from '../calendar'

type DatepickerProps = CalendarProps & {
  placement: PopoverProps['placement']
}

export function Datepicker({ placement, ...props }: DatepickerProps) {
  return (
    <Popover shouldFlip placement={placement}>
      <PopoverTrigger>
        <Button isIconOnly radius='full' variant='light'>
          <CalendarIcon className='h-4 w-4' />
        </Button>
      </PopoverTrigger>
      <PopoverContent>
        <Calendar {...props} initialFocus />
      </PopoverContent>
    </Popover>
  )
}
