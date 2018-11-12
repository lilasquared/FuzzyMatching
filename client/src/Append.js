import React from "react"
import { useState } from "react"
import axios from "axios"
import moment from "moment"
import IconButton from "./IconButton"
import AppendResults from "./AppendResults"
import { Progress } from "reactstrap"

export default function Append(props) {
  const {
    id,
    sourceId,
    lookupId,
    threshold,
    createdAt,
    status,
    progress,
    handleDelete,
  } = props
  const [expanded, setExpanded] = useState(false)
  const startMatch = () => {
    axios.post(`/api/appends/${id}/start`)
  }
  return (
    <>
      <tr>
        <td>{id}</td>
        <td>{sourceId}</td>
        <td>{lookupId}</td>
        <td>{threshold}</td>
        <td className="text-nowrap">{new moment(createdAt).fromNow()}</td>
        <td>{status}</td>
        <td className="text-right text-nowrap">
          {status === "Completed" && (
            <span>
              <IconButton
                size="sm"
                type="info"
                title="View Results"
                icon="table"
                onClick={() => setExpanded(!expanded)}
              />
              &nbsp;
            </span>
          )}
          {status === "Created" && (
            <span>
              <IconButton
                size="sm"
                type="success"
                title="Process Append"
                icon="play"
                onClick={startMatch}
              />
              &nbsp;
            </span>
          )}
          <IconButton
            size="sm"
            type="danger"
            title="Delete Append"
            icon="trash"
            onClick={handleDelete(id)}
          />
        </td>
      </tr>
      {status === "Processing" && (
        <tr>
          <td colSpan={7}>
            <div className="text-center">{progress.toFixed(1)}%</div>
            <Progress value={progress} />
          </td>
        </tr>
      )}
      {expanded && <AppendResults appendId={id} />}
    </>
  )
}
