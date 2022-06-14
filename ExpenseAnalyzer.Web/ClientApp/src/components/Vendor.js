import React, { Component } from 'react';

export class Vendor extends Component {
    static displayName = Vendor.name;

    constructor(props) {
        super(props);
        this.state = { loading: true, vendors: [] };
    }

    componentDidMount() {
        this.GetVendors();
    }

    static renderForecastsTable(vendors) {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Sl No.</th>
                            <th>Vendor</th>
                            <th>Category</th>
                        </tr>
                    </thead>
                    <tbody>
                        {vendors.map((item, index) =>
                            <tr key={item.uid}>
                                <td>{index + 1}</td>
                                <td>{item.description}</td>
                                <td>{item.categoryDescription}</td>
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
            : Vendor.renderForecastsTable(this.state.vendors);

        return (
            <div>
                <h1 id="tabelLabel" >Vendors</h1>
                {contents}
            </div>
        );
    }


    async GetVendors() {
        const uid = 1;
        //need to add user detail
        const response = await fetch('transaction/GetVendorsByUser/1');
        //console.log(response.text);
        const data = await response.json();
        this.setState({ loading: false, vendors: data })
    }
}
