import { Popover } from "@mantine/core"
import { useDisclosure } from "@mantine/hooks"

export default function MessageUser() {
  const [opened, { open, close }] = useDisclosure(false)
  return (
    <Popover opened={opened}>

    </Popover>
  )
}