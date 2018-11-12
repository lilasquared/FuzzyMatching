import React from "react"
import { Table } from "reactstrap"
import Dataset from "./Dataset"

export default function StoredDatasets(props) {
  const { datasets, deleteDataset } = props

  const datasetList = datasets.map(x => (
    <Dataset
      key={x.id}
      id={x.id}
      name={x.name}
      fileName={x.fileName}
      handleDelete={deleteDataset(x.id)}
    />
  ))

  return (
    <>
      <Table>
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>File</th>
            <th />
          </tr>
        </thead>
        <tbody>{datasetList}</tbody>
      </Table>
    </>
  )
}
