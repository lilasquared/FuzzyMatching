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
      <th className="text-right text-nowrap">
        <IconButton
          size="sm"
          type="info"
          title="Download File"
          icon="download"
          onClick={downloadFile(id)}
        />
        &nbsp;
        <IconButton
          size="sm"
          type="danger"
          title="Delete Dataset"
          icon="trash"
          onClick={handleDelete}
        />
      </th>
    </tr>
  )
}
