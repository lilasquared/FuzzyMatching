import React from "react"
import axios from "axios"
import { Row, Col } from "reactstrap"

import { usePolling } from "./hooks"
import StoredMatches from "./StoredMatches"

export default function Appends() {
  const [matches, refreshMatches] = usePolling("/api/appends", 5000)

  const deleteMatch = id => () => {
    return axios.delete(`/api/appends/${id}`).then(refreshMatches)
  }
  return (
    <>
      <Row>
        <Col sm={12}>
          <StoredMatches matches={matches} handleDelete={deleteMatch} />
        </Col>
      </Row>
    </>
  )
}
