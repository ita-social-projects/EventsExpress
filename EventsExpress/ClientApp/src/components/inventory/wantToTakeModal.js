import React, { Component } from 'react';
import Modal from 'react-bootstrap/Modal'
import Button from '@material-ui/core/Button';
import WantToTakeForm from './wantToTakeForm';

export default class WantToTakeModal extends Component {

    render() {
        const { show, inventories } = this.props;
        console.log(this.props);
        return(
            <Modal
                size="lg"
                show={show}
                onHide={() => this.props.onCancel()}
                keyboard={false}
            >
                <Modal.Header closeButton>
                    <Modal.Title>
                        What do you want to take?
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                <div className="row p-2">
                    <div className="col col-md-1 float-right"> </div>
                    <div className="col col-md-5"><b>Item name</b></div>
                    <div className="col"><b>Count</b></div>
                    <div className="col"><b>Measuring unit</b></div>
                </div>
                {inventories.items.map(item => {
                    return (       
                        <div className="row p-2">
                           <WantToTakeForm item={item}/>
                        </div>
                    )
                })}          
                </Modal.Body>
                <Modal.Footer>
                    <Button color="primary" variant="secondary" onClick={() => this.props.onCancel()}>
                        Cancel
                    </Button>
                    {/* <Button color="primary" variant="primary" onClick={() => this.props.handleSubmit()}>Submit</Button> */}
                </Modal.Footer>
            </Modal>
        );
    }
}