import React, { Component } from "react"
import axios from "axios"
import { Row, Col, Panel, Table, Glyphicon, Button } from "react-bootstrap"
import CreateDataset from "./CreateDataset"

class App extends Component {
  state = {
    datasets: [],
  }

  componentDidMount() {
    this.refreshDatasets()
  }

  refreshDatasets = () => {
    axios.get("/api/datasets").then(response => {
      this.setState({ datasets: response.data })
    })
  }

  deleteDataset = id => () => {
    return axios.delete(`/api/datasets/${id}`).then(this.refreshDatasets)
  }

  render() {
    return (
      <>
        <Row>
          <Col sm={6}>
            <Panel>
              <Panel.Heading>App Controls</Panel.Heading>
            </Panel>
          </Col>
          <Col sm={6}>
            <CreateDataset onSuccess={this.refreshDatasets} />
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
                  {this.state.datasets.map(x => (
                    <tr key={x.id}>
                      <td>{x.id}</td>
                      <td>{x.name}</td>
                      <td>{x.fileName}</td>
                      <th>
                        <Button
                          bsSize="xsmall"
                          bsStyle="danger"
                          onClick={this.deleteDataset(x.id)}
                        >
                          <Glyphicon glyph="remove" />
                        </Button>
                      </th>
                    </tr>
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
}

export default App
