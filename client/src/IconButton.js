import React from "react"
import { Button } from "reactstrap"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"

export default function IconButton(props) {
  return (
    <Button
      size={props.size}
      color={props.type}
      title={props.title}
      onClick={props.onClick}
    >
      <FontAwesomeIcon icon={props.icon} />
    </Button>
  )
}
