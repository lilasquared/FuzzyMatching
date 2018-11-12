import React, { useState } from "react"
import { Route } from "react-router"
import { Link } from "react-router-dom"
import {
  Container,
  Collapse,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  Nav,
  NavItem,
} from "reactstrap"

import MyLink from "./components/MyLink"
import Home from "./Home"
import App from "./App"
import Progress from "./Progress"

function MyNavbar(props) {
  const [isOpen, setIsOpen] = useState(false)
  return (
    <>
      <Navbar color="dark" dark expand="md">
        <Container>
          <NavbarBrand tag={Link} to="/">
            {props.title}
          </NavbarBrand>
          <NavbarToggler onClick={() => setIsOpen(prevOpen => !prevOpen)} />
          <Collapse isOpen={isOpen} navbar>
            <Nav navbar>{props.children}</Nav>
          </Collapse>
        </Container>
      </Navbar>
    </>
  )
}

function MySite(props) {
  return (
    <>
      <MyNavbar title={props.title}>{props.collapse}</MyNavbar>
      {props.children}
    </>
  )
}

function Site() {
  return (
    <>
      <MySite
        title="Fuzzy Matching"
        collapse={
          <>
            <NavItem>
              <MyLink to="/">Home</MyLink>
            </NavItem>
            <NavItem>
              <MyLink to="/app/controls">App</MyLink>
            </NavItem>
            <NavItem>
              <MyLink to="/progress">Progress</MyLink>
            </NavItem>
          </>
        }
      >
        <Route exact path="/" component={Home} />
        <Route path="/app" component={App} />
        <Route path="/progress" component={Progress} />
      </MySite>
    </>
  )
}

export default Site
