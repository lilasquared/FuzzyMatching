import React from "react"
import moment from "moment"
import { Panel, Table } from "react-bootstrap"
import IconButton from "./IconButton"

export default function StoredMatches(props) {
  const { matches } = props

  const matchList = matches.map(x => (
    <tr key={x.id}>
      <td>{x.id}</td>
      <td>{x.sourceId}</td>
      <td>{x.lookupId}</td>
      <td>{x.threshold}</td>
      <td>{new moment(x.createdAt).fromNow()}</td>
      <td>{x.status}</td>
      <td className="text-right">
        <IconButton
          size="xsmall"
          type="success"
          title="Process Match"
          icon="play"
        />
      </td>
    </tr>
  ))

  return (
    <Panel>
      <Panel.Heading>Stored Matches</Panel.Heading>
      <Table condensed>
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
    </Panel>
  )
}
