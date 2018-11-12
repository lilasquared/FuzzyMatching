import React from "react"
import ReactDOM from "react-dom"
import Site from "./Site"
import { BrowserRouter } from "react-router-dom"

import { library } from "@fortawesome/fontawesome-svg-core"
import {
  faPlay,
  faTrash,
  faDownload,
  faTable,
} from "@fortawesome/free-solid-svg-icons"

import "bootstrap/dist/css/bootstrap.css"

library.add(faPlay, faTrash, faDownload, faTable)

ReactDOM.render(
  <BrowserRouter>
    <Site />
  </BrowserRouter>,
  document.getElementById("root")
)
