import React, { Component } from 'react';

export class Dashboard extends Component {
    static displayName = Dashboard.name;

    constructor(props) {
        super(props);
        this.state = {loading: true, totalByCategories: [] };
    }

    componentDidMount() {
        this.GetTotalByCategory();
    }

    static renderForecastsTable(totalByCategories) {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Sl No.</th>
                            <th>Category</th>
                            <th>Amount</th>
                        </tr>
                    </thead>
                    <tbody>
                        {totalByCategories.map((item, index) =>
                            <tr key={item.category}>
                                <td>{index + 1}</td>
                                <td>{item.category}</td>
                                <td>{item.amount}</td>
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
            : Dashboard.renderForecastsTable(this.state.totalByCategories);

        return (
            <div>
                <h1 id="tabelLabel" >Dashboard</h1>
                {contents}
            </div>
        );
    }


    async GetTotalByCategory() {
        const response = await fetch('transaction/GetTotalByCategory');
        //console.log(response.text);
        const data = await response.json();
        this.setState({ loading: false, totalByCategories: data })
    }
}
