import React from "react"
import { Route } from "react-router"
import { useResource } from "./hooks"
import { Container, Nav, NavItem } from "reactstrap"
import AppControls from "./AppControls"
import MyLink from "./components/MyLink"
import Datasets from "./Datasets"
import Appends from "./Appends"

export default function App() {
  const [datasets, refreshDatasets] = useResource("/api/datasets")

  return (
    <Container>
      <br />
      <Nav tabs>
        <NavItem>
          <MyLink to="/app/controls">Controls</MyLink>
        </NavItem>
        <NavItem>
          <MyLink to="/app/datasets">Datasets</MyLink>
        </NavItem>
        <NavItem>
          <MyLink to="/app/appends">Appends</MyLink>
        </NavItem>
      </Nav>
      <br />
      <Route
        path="/app/controls"
        render={() => <AppControls datasets={datasets} />}
      />
      <Route
        path="/app/datasets"
        render={() => (
          <Datasets datasets={datasets} refreshDatasets={refreshDatasets} />
        )}
      />
      <Route path="/app/appends" component={Appends} />
    </Container>
  )
}
