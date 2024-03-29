﻿import React, { Component } from 'react';
import { Button, ButtonGroup } from 'reactstrap'

export class Vendor extends Component {
    static displayName = Vendor.name;

    constructor(props) {
        super(props);
        this.state = {
            loading: true, vendors: [], unsavedChanges: [] , categories: [], initialVendors: [] };
        this.onCategorySelect = this.onCategorySelect.bind(this);
        this.submitVendors = this.submitVendors.bind(this);
        
    }

    

    componentDidMount() {
        this.Refresh();
        //this.GetVendors();
    }

    

    static renderForecastsTable(vendors, radioVal, categories, onCategorySelect, submitVendors) {

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
                                            categories.map((cat, i) => (
                                                <Button
                                                    key={i}
                                                    outline
                                                    color="primary"
                                                    onClick={() => onCategorySelect(item.uid, cat.uid, cat.description)}
                                                    active={item.CategoryMasterUid === cat.uid }
                                                >
                                                    {cat.description}
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
                <Button
                    color="primary"
                    onClick={() => submitVendors() }
                >
                    Save
                </Button>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Vendor.renderForecastsTable(this.state.vendors, this.state.unsavedVendors, this.state.categories, this.onCategorySelect, this.submitVendors);

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
        return await response.json();
        //let cats = data.map(record => record.categoryDescription).filter(record => record !== null);
        //let uniqueCategories = [...new Set(cats)]

        //this.setState({ loading: false, vendors: data, categories: uniqueCategories })
    }

    async GetCategories() {
        let response = await fetch('transaction/GetCategories');
        return await response.json(); 
    };

    onCategorySelect(uid, value, description) {
        const s = this.state;
        this.setState(prev => ({
            vendors: prev.vendors.map(
                obj => (obj.uid === uid ? Object.assign(obj, { categoryDescription: description, categoryMasterUid: value}) : obj)
            ),
            
        }));  

        
    }

    async Refresh() {
        let vendors = await this.GetVendors();
        let categories = await this.GetCategories();
        this.setState({
            loading: false, vendors: vendors, categories: categories,
            initialVendors: vendors.map(a => ({ ...a }))
        })
    }

    submitVendors() {
        let diff = this.state.vendors.filter(
            x => !this.state.initialVendors.some(
                y => (x.categoryMasterUid === y.categoryMasterUid && x.uid === y.uid)
            )
        );

        const result = fetch('transaction/SubmitVendors', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(diff),
        })
            .then(response => response.json())
            .then(data => {
                console.log('Success:', data);
            })
            .catch((error) => {
                console.error('Error', error);
            });
    }


      

}
