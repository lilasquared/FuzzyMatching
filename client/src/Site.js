import React, { Component } from "react"
import { Route } from "react-router"
import { Link } from "react-router-dom"
import { LinkContainer } from "react-router-bootstrap"
import { Grid, Navbar, Nav, NavItem } from "react-bootstrap"

import Home from "./Home"
import App from "./App"
import Progress from "./Progress"

class Site extends Component {
  render() {
    return (
      <React.Fragment>
        <Navbar inverse>
          <Navbar.Header>
            <Navbar.Brand>
              <Link to="/">Fuzzy Matching</Link>
            </Navbar.Brand>
            <Navbar.Toggle />
          </Navbar.Header>
          <Navbar.Collapse>
            <Nav>
              <LinkContainer to="/app">
                <NavItem>App</NavItem>
              </LinkContainer>
              <LinkContainer to="/progress">
                <NavItem>Progress</NavItem>
              </LinkContainer>
            </Nav>
          </Navbar.Collapse>
        </Navbar>
        <Grid>
          <Route exact path="/" component={Home} />
          <Route path="/app" component={App} />
          <Route path="/progress" component={Progress} />
        </Grid>
      </React.Fragment>
    )
  }
}

export default Site
