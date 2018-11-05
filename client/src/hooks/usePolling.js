import { useState, useEffect } from "react"
import { useResource } from "./index"

export default function usePolling(url, interval) {
  const [timestamp, setTimestamp] = useState(new Date())
  const [resource, refreshResource, , cancelToken] = useResource(url)

  useEffect(
    () => {
      let timeoutHandle
      refreshResource()
        .then(() => {
          timeoutHandle = setTimeout(() => setTimestamp(new Date()), interval)
        })
        .catch(() => {
          console.log("cancel usePolling")
        })

      return () => {
        cancelToken.cancel("usePolling")
        if (timeoutHandle) clearTimeout(timeoutHandle)
      }
    },
    [timestamp]
  )

  return [resource, refreshResource]
}
