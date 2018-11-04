import React, { useState } from "react"
import { useDidMount } from "./hooks"
import { saveAs } from "file-saver"
import { parse } from "content-disposition"
import axios from "axios"
import { Row, Col, Panel, Table, Glyphicon, Button } from "react-bootstrap"
import CreateDataset from "./CreateDataset"

function Dataset(props) {
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
      <th>
        <Button
          bsSize="xsmall"
          bsStyle="danger"
          title="Delete"
          onClick={handleDelete}
        >
          <Glyphicon glyph="remove" />
        </Button>
        &nbsp;
        <Button
          bsSize="xsmall"
          bsStyle="info"
          title="Download File"
          onClick={downloadFile(id)}
        >
          <Glyphicon glyph="download-alt" />
        </Button>
      </th>
    </tr>
  )
}

export default function App() {
  const [datasets, setDatasets] = useState([])

  useDidMount(() => refreshDatasets())

  const refreshDatasets = () => {
    axios.get("/api/datasets").then(response => {
      setDatasets(response.data)
    })
  }

  const deleteDataset = id => () => {
    return axios.delete(`/api/datasets/${id}`).then(refreshDatasets)
  }
  return (
    <>
      <Row>
        <Col sm={6}>
          <Panel>
            <Panel.Heading>App Controls</Panel.Heading>
          </Panel>
        </Col>
        <Col sm={6}>
          <CreateDataset onSuccess={refreshDatasets} />
          <Panel>
            <Panel.Heading>Stored Datasets</Panel.Heading>
            <Table condensed>
              <thead>
                <tr>
                  <th>#</th>
                  <th>Name</th>
                  <th>File</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {datasets.map(x => (
                  <Dataset
                    key={x.id}
                    id={x.id}
                    name={x.name}
                    fileName={x.fileName}
                    handleDelete={deleteDataset(x.id)}
                  />
                ))}
              </tbody>
            </Table>
          </Panel>
        </Col>
      </Row>
      <Row>
        <Col sm={12}>
          <Panel>
            <Panel.Heading>Results</Panel.Heading>
          </Panel>
        </Col>
      </Row>
    </>
  )
}
