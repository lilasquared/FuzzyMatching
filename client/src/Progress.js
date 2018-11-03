import React, { Component } from "react"
import {
  Panel,
  Form,
  FormGroup,
  FormControl,
  ControlLabel,
  Checkbox,
  Col,
} from "react-bootstrap"
import { toDateInputValue } from "./utils"

class Progress extends Component {
  render() {
    return (
      <>
        <h1>Progress</h1>
        <Panel>
          <Panel.Heading>Add Update</Panel.Heading>
          <Panel.Body>
            <UpdateForm />
          </Panel.Body>
        </Panel>
      </>
    )
  }
}

class UpdateForm extends Component {
  state = {
    date: toDateInputValue(new Date()),
    title: "",
    description: "",
    participants: [],
  }

  setInputState = key => e => {
    this.setState({ [key]: e.target.value })
  }

  render() {
    return (
      <Form horizontal>
        <FormGroup controlId="date">
          <Col componentClass={ControlLabel} sm={2}>
            Date
          </Col>
          <Col sm={10}>
            <FormControl
              type="date"
              value={this.state.date}
              onChange={this.setInputState("date")}
            />
          </Col>
        </FormGroup>
        <FormGroup controlId="title">
          <Col componentClass={ControlLabel} sm={2}>
            Title
          </Col>
          <Col sm={10}>
            <FormControl
              type="text"
              placeholder="Meaningful Title"
              value={this.state.title}
              onChange={this.setInputState("title")}
            />
          </Col>
        </FormGroup>
        <FormGroup controlId="description">
          <Col componentClass={ControlLabel} sm={2}>
            Description
          </Col>
          <Col sm={10}>
            <FormControl
              componentClass="textarea"
              placeholder="Notes or description of the update"
              value={this.state.description}
              onChange={this.setInputState("description")}
            />
          </Col>
        </FormGroup>
        <FormGroup controlId="participants">
          <Col componentClass={ControlLabel} sm={2}>
            Participant(s)
          </Col>
          <Col sm={10}>
            <Checkbox>Aaron Roberts</Checkbox>
            <Checkbox>Chase Wood</Checkbox>
            <Checkbox>Daniel Childers</Checkbox>
            <Checkbox>Jonathan Miu</Checkbox>
            <Checkbox>Lea Nooyen</Checkbox>
          </Col>
        </FormGroup>
        <FormGroup controlId="links">
          <Col componentClass={ControlLabel} sm={2}>
            Description
          </Col>
          <Col sm={10}>
            <FormControl
              componentClass="textarea"
              placeholder="Notes or description of the update"
              value={this.state.description}
              onChange={this.setInputState("description")}
            />
          </Col>
        </FormGroup>
      </Form>
    )
  }
}

export default Progress
