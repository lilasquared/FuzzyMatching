import React from "react"
import { Table } from "reactstrap"
import Append from "./Append"

export default function StoredMatches(props) {
  const { matches, handleDelete } = props

  const matchList = matches.map(x => (
    <Append key={x.id} {...x} handleDelete={handleDelete} />
  ))

  return (
    <>
      <Table size="sm">
        <thead>
          <tr>
            <th>#</th>
            <th>Source Id</th>
            <th>Lookup Id</th>
            <th>Threshold</th>
            <th>Created</th>
            <th>Status</th>
            <th />
          </tr>
        </thead>
        <tbody>{matchList}</tbody>
      </Table>
    </>
  )
}
