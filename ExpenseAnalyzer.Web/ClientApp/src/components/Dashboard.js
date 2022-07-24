import React, { Component } from 'react';
//import './NavMenu.css';
import { Form, FormGroup, FormText, Label, Input, Button, Table, Row, Container, Col } from 'reactstrap'

export class Dashboard extends Component {
    static displayName = Dashboard.name;

    constructor(props) {
        super(props);
        this.state = {
            loading: true, totalByCategories: [], files: []
        };
        this.uploadFile = this.uploadFile.bind(this);
        this.submitFiles = this.submitFiles.bind(this);
    }

    componentDidMount() {
        this.GetTotalByCategory();
    }

    static renderView(totalByCategories, uploadFile, submitFiles, files) {
        return (
            <div>
                <div>
                    <Form>
                        <FormGroup>
                            <Label for="fileUploadInput">
                                Upload
                            </Label>
                            <Input
                                id="fileUploadInput"
                                name="file"
                                type="file"
                                onChange={uploadFile}
                            />
                        </FormGroup>

                        <FormGroup>
                            {files.length > 0 &&
                                <Container>
                                    <Row xs='3'>
                                        <Col  >
                                            <Table
                                                striped
                                            >
                                                <thead>
                                                    <tr>
                                                        <th>Sl No.</th>
                                                        <th>File Name</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {files.map((item, index) =>
                                                        <tr scope='row' key={index}>
                                                            <td>{index + 1}</td>
                                                            <td>{item.name}</td>
                                                        </tr>
                                                    )}
                                                </tbody>
                                            </Table>

                                            <Button
                                                color="primary"
                                                onClick={() => submitFiles()}
                                            >
                                                Submit
                                            </Button>
                                        </Col>
                                    </Row>
                                </Container>


                            }



                        </FormGroup>
                    </Form>


                </div>


                <table className='table' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Sl No.</th>
                            <th>Category</th>
                            <th>Amount</th>
                        </tr>
                    </thead>
                    <tbody>
                        {totalByCategories.map((item, index) =>
                            <tr key={item.category} >
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
            : Dashboard.renderView(this.state.totalByCategories, this.uploadFile, this.submitFiles, this.state.files);

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


    async submitFiles() {
        try {
            const formData = new FormData();
            this.state.files.forEach(item => {
                formData.append("files", item, item.name);
            });

            const response = await fetch("transaction/UploadFile", {
                method: 'POST',
                body: formData
            });

            if (response.ok) {
                window.location.href = '/';
            }
            //resultElement.value = 'Result: ' + response.status + ' ' +
            //  response.errorMessage;
        } catch (error) {
            console.error('Error:', error);
        }
    }

    async uploadFile(request) {
        this.setState({
            files: [...this.state.files, request.target.files[0]]
        })
    }


}
