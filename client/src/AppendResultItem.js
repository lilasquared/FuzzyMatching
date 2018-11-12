import React from "react"

export default function AppendResultItem(props) {
  const {
    sourceRecordId,
    sourceRecord,
    lookupRecordId,
    lookupRecord,
    ratio,
  } = props
  return (
    <tr>
      <td>{sourceRecordId}</td>
      <td>{sourceRecord}</td>
      <td>{lookupRecordId}</td>
      <td>{lookupRecord}</td>
      <td>{ratio}</td>
    </tr>
  )
}
