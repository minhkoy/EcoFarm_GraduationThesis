import { LoadingOverlay } from "@mantine/core";

const DefaultOverlay = () => {
  return (
    <LoadingOverlay
      visible
      zIndex={1000}
      overlayProps={{ radius: 'sm', blur: 2 }}
      loaderProps={{ color: 'teal', type: 'bars' }}
    />
  )
}

export default DefaultOverlay;