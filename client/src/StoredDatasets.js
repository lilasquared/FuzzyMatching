import React from "react"
import { Panel, Table } from "react-bootstrap"
import Dataset from "./Dataset"

export default function StoredDatasets(props) {
  const { datasets, datasetsRefreshing, deleteDataset } = props

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
    <Panel>
      <Panel.Heading>
        Stored Datasets {datasetsRefreshing && <span>Loading...</span>}
      </Panel.Heading>
      <Table condensed>
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
    </Panel>
  )
}
