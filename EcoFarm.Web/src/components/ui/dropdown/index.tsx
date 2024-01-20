import { fontSansStyle } from '@/config/lib/fonts'
import { Dropdown, type DropdownProps } from '@nextui-org/react'

type Props = DropdownProps

export default function NextUiDropdown(props: Props) {
  return (
    <Dropdown
      isOpen={props.isOpen}
      onOpenChange={props.onOpenChange}
      style={{
        ...fontSansStyle,
      }}
      placement={props.placement ?? 'bottom'}
      closeOnSelect={props.closeOnSelect ?? true}
    >
      {props.children}
    </Dropdown>
  )
}
