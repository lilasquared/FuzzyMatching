import React from "react"
import axios from "axios"

import UploadDatasetForm from "./UploadDatasetForm"
import StoredDatasets from "./StoredDatasets"

export default function Datasets({ datasets, refreshDatasets }) {
  const deleteDataset = id => async () => {
    let refreshDatasets = await axios.delete(`/api/datasets/${id}`)
    return refreshDatasets(refreshDatasets)
  }

  return (
    <>
      <UploadDatasetForm onSuccess={refreshDatasets} />
      <StoredDatasets datasets={datasets} deleteDataset={deleteDataset} />
    </>
  )
}
