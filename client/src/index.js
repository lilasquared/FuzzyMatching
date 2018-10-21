import React from "react"
import ReactDOM from "react-dom"
import Site from "./Site"
import { HashRouter } from "react-router-dom"

ReactDOM.render(
  <HashRouter>
    <Site />
  </HashRouter>,
  document.getElementById("root")
)
