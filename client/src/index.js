import React from "react"
import ReactDOM from "react-dom"
import Site from "./Site"
import { BrowserRouter } from "react-router-dom"

ReactDOM.render(
  <BrowserRouter>
    <Site />
  </BrowserRouter>,
  document.getElementById("root")
)
