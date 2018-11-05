import React from "react"
import axios from "axios"
import { parse } from "content-disposition"
import { saveAs } from "file-saver"
import IconButton from "./IconButton"

export default function Dataset(props) {
  const downloadFile = id => () => {
    axios
      .get(`/api/datasets/${id}/file`, {
        responseType: "blob",
      })
      .then(response => {
        const {
          parameters: { filename },
        } = parse(response.headers["content-disposition"])
        saveAs(response.data, filename)
      })
  }

  const { id, name, fileName, handleDelete } = props
  return (
    <tr>
      <td>{id}</td>
      <td>{name}</td>
      <td>{fileName}</td>
      <th className="text-right">
        <IconButton
          size="xsmall"
          type="danger"
          title="Delete Dataset"
          onClick={handleDelete}
          icon="remove"
        />
        &nbsp;
        <IconButton
          size="xsmall"
          type="info"
          title="Download File"
          onClick={downloadFile(id)}
          icon="download-alt"
        />
      </th>
    </tr>
  )
}
