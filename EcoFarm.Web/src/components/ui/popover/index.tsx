import { fontSansStyle } from '@/config/lib/fonts'
import { Popover, type PopoverProps } from '@nextui-org/react'

type Props = PopoverProps

export default function NextUiPopover(props: Props) {
  return (
    <Popover
      isOpen={props.isOpen}
      onOpenChange={props.onOpenChange}
      placement={props.placement ?? 'bottom'}
      style={fontSansStyle}
    >
      {props.children}
    </Popover>
  )
}
