import { Text } from "@mantine/core";

const TextTitle = ({ children }) => {
  return (
    <Text size="xl" c='teal' fw='bold' className="uppercase">{children}</Text>
  )
}

export default TextTitle;