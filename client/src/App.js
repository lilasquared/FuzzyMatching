import React from "react"
import { usePolling, useResource, useDidMount } from "./hooks"
import axios from "axios"
import { Grid, Row, Col } from "react-bootstrap"
import AppControls from "./AppControls"
import UploadDatasetForm from "./UploadDatasetForm"
import StoredDatasets from "./StoredDatasets"
import StoredMatches from "./StoredMatches"

export default function App() {
  const [datasets, refreshDatasets, , tokenSource] = useResource(
    "/api/datasets"
  )

  const [matches, refreshMatches] = usePolling("/api/matches", 5000)
  useDidMount(() => {
    refreshDatasets().catch(() => {
      console.log("cancel useResource")
    })
    return () => tokenSource.cancel()
  })

  const deleteDataset = id => () => {
    return axios.delete(`/api/datasets/${id}`).then(refreshDatasets)
  }

  const deleteMatch = id => () => {
    return axios.delete(`/api/matches/${id}`).then(refreshMatches)
  }

  return (
    <Grid>
      <Row>
        <Col sm={12} md={6}>
          <Row>
            <Col sm={12}>
              <AppControls datasets={datasets} onSuccess={refreshMatches} />
            </Col>
          </Row>
          <Row>
            <Col sm={12}>
              <StoredMatches matches={matches} handleDelete={deleteMatch} />
            </Col>
          </Row>
        </Col>
        <Col sm={12} md={6}>
          <UploadDatasetForm onSuccess={refreshDatasets} />
          <StoredDatasets datasets={datasets} deleteDataset={deleteDataset} />
        </Col>
      </Row>
    </Grid>
  )
}
