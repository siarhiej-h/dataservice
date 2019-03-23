import React, { Component } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-material.css';
import { dataUrl } from './../constants'

class Grid extends Component {
  constructor(props) {
    super(props);
    this.state = {
      columnDefs: [],
      rowData: []
    }
  }

  componentDidMount() {
    fetch(dataUrl)
      .then(result => result.json())
      .then(rowData => {
          var columns = this.configureColumns(rowData);
          this.setState({rowData : rowData, columnDefs : columns})
      })
  }

  configureColumns(rowData) {
      var columnsData = []
      if (rowData.length > 0) {
          var obj = rowData[0];
          for (var key in obj) {
              var def = { 
                  headerName : this.capitalize(key),
                  field : key,
                  sortable: true,
                  editable: true,
                  resizable: true
              };
              columnsData.push(def);
          }
      }
      return columnsData;
  }

  capitalize(string) {
      return string.replace(/\b\w/g, l => l.toUpperCase());
  }

  cellValueChanged(event) {
      var row = this.rowData[event.rowIndex];
      console.log(row);

      var body = JSON.stringify(row);
      console.log(body);
      return fetch(dataUrl, {
          method: 'PUT',
          body : body,
          headers : { 'content-type': 'application/json' }
      })
      .catch(err => console.log(err));
  }

  render() {
    return (
      <div 
        className="ag-theme-material"
        style={{ 
        height: '500px', 
        width: '600px' }} 
      >
        <AgGridReact
          columnDefs={this.state.columnDefs}
          rowData={this.state.rowData}
          onCellValueChanged={this.cellValueChanged}
          stopEditingWhenGridLosesFocus={true}>
        </AgGridReact>
      </div>
    );
  }
}

export default Grid;