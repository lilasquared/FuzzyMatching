import React from "react"
import {
  Button,
  Panel,
  Form,
  FormGroup,
  FormControl,
  ControlLabel,
} from "react-bootstrap"

export default function AppControls(props) {
  const { datasets } = props

  const options = datasets.map(x => (
    <option key={x.id} value={x.id}>
      [{x.id}] {x.name} ({x.fileName})
    </option>
  ))

  const onSubmit = e => {
    e.preventDefault()
    console.log(e.target.dataset1.value)
    console.log(e.target.dataset2.value)
    console.log(e.target.threshold.value)
    e.target.reset()
  }

  const disableSubmit = datasets.length === 0

  return (
    <Panel>
      <Panel.Heading>Create a Match</Panel.Heading>
      <Panel.Body>
        <Form onSubmit={onSubmit}>
          <FormGroup controlId="dataset1">
            <ControlLabel>Choose a Dataset</ControlLabel>
            <FormControl componentClass="select" required>
              {options}
            </FormControl>
          </FormGroup>
          <FormGroup controlId="dataset2">
            <ControlLabel>Choose a Dataset</ControlLabel>
            <FormControl componentClass="select" required>
              {options}
            </FormControl>
          </FormGroup>
          <FormGroup controlId="threshold">
            <ControlLabel>Choose a Threshold</ControlLabel>
            <FormControl
              type="number"
              step="0.01"
              min="0.01"
              max="1.00"
              defaultValue="0.75"
              required
            />
          </FormGroup>
          <Button
            type="submit"
            bsStyle="primary"
            block
            disabled={disableSubmit}
          >
            Create Match
          </Button>
        </Form>
      </Panel.Body>
    </Panel>
  )
}
