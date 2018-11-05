import { useState } from "react"
import axios from "axios"

export default function useResource(url) {
  const [resource, setResource] = useState([])
  const [refreshing, setRefreshing] = useState(false)

  const tokenSource = axios.CancelToken.source()

  const refreshResource = async () => {
    setRefreshing(true)
    const response = await axios.get(url, { cancelToken: tokenSource.token })
    setResource(response.data)
    setRefreshing(false)
  }

  return [resource, refreshResource, refreshing, tokenSource]
}
