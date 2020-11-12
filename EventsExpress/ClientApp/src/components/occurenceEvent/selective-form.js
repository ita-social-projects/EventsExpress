import React, { Component } from 'react';
import Button from "@material-ui/core/Button";
import { Link } from 'react-router-dom';
import Select from '@material-ui/core/Select'
import MenuItem from '@material-ui/core/MenuItem';
import AddFromParentEventWrapper from '../../containers/add-event-from-parent';
import Home from '../home';
import Tooltip from '@material-ui/core/Tooltip';
import { Redirect } from 'react-router-dom'
import IconButton from '@material-ui/core/IconButton';
import OccurenceEventModal from '../occurenceEvent/occurenceEvent-modal'
import Dropdown from 'react-bootstrap/Dropdown'
import DropdownButton from 'react-bootstrap/DropdownButton'
import { Popup } from 'semantic-ui-react'
import '../occurenceEvent/occurenceEvent.css'


export class SelectiveForm extends Component {
    constructor() {
        super()
        this.state = {
            isCreateOption: false,
            isEditOption: false,
            isCancelOnceOption: false,
            isCancelOption: false,
            show: false,
        };
        this.EditOption = this.EditOption.bind(this);
        this.showHandler = this.showHandler.bind(this);
    }

    componentDidMount = () => {
        console.log("did mount")
    }

    componentDidUpdate = () => {
        console.log("did up", this.state)
    }

    onClickConfirm = (e) => {
       this.CreateOption() ||
       this.EditOption() ||
       this.CancelOnceOption() ||
       this.CancelOption()
    }

    CreateOption = () => {
        console.log("Craete")
        this.setState({
            isCreateOption: true,
            show: true
        });
    }

    showHandler = () => {
        this.setState({
            isCreateOption: false,
            isEditOption: false,
            isCancelOnceOption: false,
            isCancelOption: false
        });
        console.log("showhandler",this.state);
    }

    EditOption = () => {
        console.log(this);
        this.setState(state => ({
            isEditOption: true   
        }));
        console.log(this.state);
    }

    CancelOnceOption = () => {
        console.log("Cancelonce")
        this.setState({
            isCancelOnceOption: true,
            show: true
        });
    }

    CancelOption = () => {
        console.log("Cancel")
        this.setState({
            isCancelOption: true,
            show: true
        });
    }

    resetForm = () => {
        this.setState({
            imagefile: [],
            isCreateOption: false,
            isEditOption: false,
            isCancelOnceOption: false,
            isCancelOption: false
        });
    }

    render() {

        return (
            <div className="shadow-lg p-3 mb-5 bg-white rounded">
                <form onSubmit={this.handleSubmit} encType="multipart/form-data">
                    <div className="row">
                        <div className="col-8">
                            <DropdownButton title="Select Options" className="rounded">
                                <Dropdown.Item eventKey="0" onClick={this.CreateOption}>Create without editing</Dropdown.Item>
                                <Dropdown.Item eventKey="1" onClick={this.EditOption}>Create with editing</Dropdown.Item>
                                <Dropdown.Item eventKey="2" onClick={this.CancelOnceOption}>Cancel once</Dropdown.Item>
                                <Dropdown.Item eventKey="3" onClick={this.CancelOption}>Cancel</Dropdown.Item>
                            </DropdownButton>
                        </div>
                        <div className="col-3" />
                        <div>
                            <Popup content='Add users to your feed' trigger={<i class="fas fa-info-circle"></i>} />
                        </div>
                    </div>
                </form>
                {this.state.isEditOption && <OccurenceEventModal showHandler={this.showHandler} />}
                {this.state.isCreateOption && <OccurenceEventModal showHandler={this.showHandler} />}
                {this.state.isCancelOnceOption && <OccurenceEventModal showHandler={this.showHandler} />}
                {this.state.isCancelOption && <OccurenceEventModal showHandler={this.showHandler} />}
                {/*{this.state.isEditOption &&
                    <div className="row shadow mt-5 p-5 mb-5 bg-white rounded">
                        <AddFromParentEventWrapper />
                    </div>
                }*/}
            </div>
        );
    }
}


export default SelectiveForm