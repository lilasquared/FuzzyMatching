import React from "react"
import { Table } from "reactstrap"
import { useResource } from "./hooks"
import AppendResultItem from "./AppendResultItem"

export default function AppendResults(props) {
  const { appendId } = props
  const [append] = useResource(`/api/appends/${appendId}`)

  return (
    <tr>
      <td colSpan={8}>
        <Table>
          <thead>
            <tr>
              <th />
              <th>Source</th>
              <th />
              <th>Lookup</th>
              <th>Ratio</th>
            </tr>
          </thead>
          <tbody>
            {append.results &&
              append.results.map((x, i) => <AppendResultItem key={i} {...x} />)}
          </tbody>
        </Table>
      </td>
    </tr>
  )
}
