import React from "react"
import axios from "axios"
import { Button, Form, FormGroup, Label, Input, Row, Col } from "reactstrap"

export default function AppControls(props) {
  const { datasets, onSuccess } = props

  const options = datasets.map(x => (
    <option key={x.id} value={x.id}>
      [{x.id}] {x.name} ({x.fileName})
    </option>
  ))

  const onSubmit = e => {
    e.preventDefault()

    axios
      .post("/api/appends", {
        sourceId: e.target.source.value,
        lookupId: e.target.lookup.value,
        threshold: e.target.threshold.value,
      })
      .then(onSuccess)
    e.target.reset()
  }

  const disableSubmit = datasets.length === 0

  return (
    <>
      <Row>
        <Col sm={12}>
          <h2>Create an Append</h2>
        </Col>
      </Row>
      <Row>
        <Col
          sm={{
            size: 6,
            offset: 3,
          }}
        >
          <Form onSubmit={onSubmit}>
            <FormGroup>
              <Label>Choose a Source Dataset</Label>
              <Input type="select" name="source" required>
                {options}
              </Input>
            </FormGroup>
            <FormGroup>
              <Label>Choose a Lookup Dataset</Label>
              <Input type="select" name="lookup" required>
                {options}
              </Input>
            </FormGroup>
            <FormGroup>
              <Label>Choose a Threshold</Label>
              <Input
                type="number"
                step="0.01"
                min="0.01"
                max="1.00"
                defaultValue="0.75"
                name="threshold"
                required
              />
            </FormGroup>
            <Button
              type="submit"
              color="primary"
              block
              disabled={disableSubmit}
            >
              Create Append
            </Button>
          </Form>
        </Col>
      </Row>
    </>
  )
}
