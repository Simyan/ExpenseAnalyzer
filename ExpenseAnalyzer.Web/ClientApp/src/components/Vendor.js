import React, { Component } from 'react';
import { Button, ButtonGroup } from 'reactstrap'

export class Vendor extends Component {
    static displayName = Vendor.name;

    constructor(props) {
        super(props);
        this.state = {
            loading: true, vendors: [], radioValue: 0, radioValues: new Map() , categories: [] };
        this.onCategorySelect = this.onCategorySelect.bind(this);
    }

    

    componentDidMount() {
        this.GetVendors();
    }

    static renderForecastsTable(vendors, radioVal, categories, onCategorySelect) {

        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Sl No.</th>
                            <th>Vendor</th>
                            <th>Category</th>
                            <th>Category Selection</th>
                        </tr>
                    </thead>
                    <tbody>
                        {vendors.map((item, index) => 
                            <tr key={item.uid}>
                                <td>{index + 1}</td>
                                <td>{item.description}</td>
                                <td>{item.categoryDescription}</td>


                                <td>
                                    <ButtonGroup>
                                        {
                                            categories.map((radio, i) => (
                                                <Button
                                                    key={i}
                                                    outline
                                                    color="primary"
                                                    onClick={() => onCategorySelect(item.uid, radio)}
                                                    active={ radioVal.get(item.uid) === radio }
                                                >
                                                    { radio }
                                                </Button>
                                            )
                                            )
                                        }
                                    </ButtonGroup>
                                </td>
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
            : Vendor.renderForecastsTable(this.state.vendors, this.state.radioValues, this.state.categories, this.onCategorySelect);

        return (
            <div>
                <h1 id="tabelLabel" >Vendors</h1>
                {contents}
            </div>
        );
    }


    async GetVendors() {
        const uid = 1;
        //Todo: need to add user detail
        const response = await fetch('transaction/GetVendorsByUser/1');
        //console.log(response.text);
        const data = await response.json();
        let cats = data.map(record => record.categoryDescription).filter(record => record !== null);
        let uniqueCategories = [...new Set(cats)]
        this.setState({ loading: false, vendors: data, categories: uniqueCategories })
    }

    onCategorySelect(uid, value) {
        console.log(this.state.radioValues);
        this.setState(prev => ( {
            radioValues: prev.radioValues.set(uid, value)
        }))
    }
}
