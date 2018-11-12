import React, { useState } from "react"
import axios from "axios"
import { Button, Alert, Form, FormGroup, Input } from "reactstrap"

export default function CreateDataset(props) {
  const [error, setError] = useState("")

  const onSubmit = async e => {
    e.preventDefault()
    setError("")
    const url = "/api/datasets"
    const formData = new FormData()
    formData.append("file", e.target.file.files[0])
    formData.append("name", e.target.name.value)
    e.target.reset()
    try {
      await axios.post(url, formData)
      props.onSuccess()
    } catch (error) {
      setError(error.response.data)
    }
  }

  return (
    <>
      <h2>Upload Dataset</h2>
      {error && <Alert color="danger">{error}</Alert>}
      <Form onSubmit={onSubmit}>
        <FormGroup>
          <Input type="text" name="name" placeholder="Dataset Name" required />
        </FormGroup>
        <FormGroup>
          <Input type="file" name="file" required />
        </FormGroup>
        <Button type="submit" color="primary" block>
          Upload
        </Button>
      </Form>
    </>
  )
}
