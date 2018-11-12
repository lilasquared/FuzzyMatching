import React, { Component } from "react"
import { Form, FormGroup, Input, Label, Col } from "reactstrap"
import { toDateInputValue } from "./utils"

class Progress extends Component {
  render() {
    return (
      <>
        <h1>Progress</h1>
        <h3>Add Update</h3>
        <UpdateForm />
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
          <Col componentClass={Label} sm={2}>
            Date
          </Col>
          <Col sm={10}>
            <Input
              type="date"
              value={this.state.date}
              onChange={this.setInputState("date")}
            />
          </Col>
        </FormGroup>
        <FormGroup controlId="title">
          <Col componentClass={Label} sm={2}>
            Title
          </Col>
          <Col sm={10}>
            <Input
              type="text"
              placeholder="Meaningful Title"
              value={this.state.title}
              onChange={this.setInputState("title")}
            />
          </Col>
        </FormGroup>
        <FormGroup controlId="description">
          <Col componentClass={Label} sm={2}>
            Description
          </Col>
          <Col sm={10}>
            <Input
              componentClass="textarea"
              placeholder="Notes or description of the update"
              value={this.state.description}
              onChange={this.setInputState("description")}
            />
          </Col>
        </FormGroup>
        <FormGroup controlId="participants">
          <Col componentClass={Label} sm={2}>
            Participant(s)
          </Col>
          <Col sm={10}>
            <Input type="checkbox" />
            Aaron Roberts
            <Input type="checkbox" />
            Chase Wood
            <Input type="checkbox" />
            Daniel Childers
            <Input type="checkbox" />
            Jonathan Miu
            <Input type="checkbox" />
            Lea Nooyen
          </Col>
        </FormGroup>
        <FormGroup controlId="links">
          <Col componentClass={Label} sm={2}>
            Description
          </Col>
          <Col sm={10}>
            <Input
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
