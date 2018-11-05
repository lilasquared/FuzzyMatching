import React from "react"
import { Button, Glyphicon } from "react-bootstrap"

export default function IconButton(props) {
  return (
    <Button
      bsSize={props.size}
      bsStyle={props.type}
      title={props.title}
      onClick={props.onClick}
    >
      <Glyphicon glyph={props.icon} />
    </Button>
  )
}
