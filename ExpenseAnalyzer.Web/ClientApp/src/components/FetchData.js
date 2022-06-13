import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true , totalByCategories : []};
  }

  componentDidMount() {
      this.populateWeatherData();
      this.GetTotalByCategory();
  }

    static renderForecastsTable(forecasts, totalByCategories) {
      return (
          <div>
          <table className='table table-striped' aria-labelledby="tabelLabel">
              <thead>
                  <tr>
                      <th>Date</th>
                      <th>Temp. (C)</th>
                      <th>Temp. (F)</th>
                      <th>Summary</th>
                  </tr>
              </thead>
              <tbody>
                  {forecasts.map(forecast =>
                      <tr key={forecast.date}>
                          <td>{forecast.date}</td>
                          <td>{forecast.temperatureC}</td>
                          <td>{forecast.temperatureF}</td>
                          <td>{forecast.summary}</td>
                      </tr>
                  )}
              </tbody>
              </table>

                <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                  <tr>
                     <th>Category</th>
                     <th>Amount</th>
                  </tr>
                </thead>
                <tbody>
                  {totalByCategories.map((item, index) =>
                    <tr key={item.dummyKey}>
                      <td>{index}</td>
                      <td>{item.dummyKey}</td>
                      <td>{item.dummyValue}</td>
                    </tr>
                  )}
                </tbody>
              </table>
        </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts, this.state.totalByCategories);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('weatherforecast/get');
   // const response = await fetch('api/get');
   //   console.log(response.text);
     const data = await response.json();
      this.setState({ forecasts: data});
    }

    async GetTotalByCategory() {
        const response = await fetch('weatherforecast/GetTotalByCategory');
        //console.log(response.text);
        const data = await response.json();
        this.setState({ loading: false, totalByCategories: data})
    }
}
