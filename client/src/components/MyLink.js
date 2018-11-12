import React from "react"
import { NavLink } from "reactstrap"
import { Link } from "react-router-dom"
import useReactRouter from "use-react-router"

export default function MyLink(props) {
  const { location } = useReactRouter()
  return (
    <NavLink tag={Link} active={props.to === location.pathname} {...props} />
  )
}
