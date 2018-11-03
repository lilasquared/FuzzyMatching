import React, { Component } from "react"
import {
  Panel,
  Form,
  FormGroup,
  FormControl,
  Button,
  Alert,
} from "react-bootstrap"
import { post } from "axios"

class CreateDataset extends Component {
  state = {
    error: "",
  }

  onSubmit = e => {
    e.preventDefault()
    this.setState({ error: "" })
    const url = "/api/datasets"
    const formData = new FormData()
    formData.append("file", e.target.file.files[0])
    formData.append("name", e.target.name.value)
    e.target.reset()
    post(url, formData).then(this.props.onSuccess, error => {
      this.setState({ error: error.response.data })
    })
  }

  render() {
    return (
      <Panel>
        <Panel.Heading>Upload Dataset</Panel.Heading>
        <Panel.Body>
          {this.state.error && (
            <Alert bsStyle="danger">{this.state.error}</Alert>
          )}
          <Form onSubmit={this.onSubmit}>
            <FormGroup>
              <FormControl
                type="text"
                name="name"
                placeholder="Dataset Name"
                required
              />
            </FormGroup>
            <FormGroup>
              <FormControl type="file" name="file" required />
            </FormGroup>
            <Button type="submit" bsStyle="primary" block>
              Upload
            </Button>
          </Form>
        </Panel.Body>
      </Panel>
    )
  }
}

export default CreateDataset
