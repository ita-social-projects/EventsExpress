import React, { Component } from "react";
import { Link } from 'react-router-dom';
import IconButton from "@material-ui/core/IconButton";
import SimpleModal from '../event/simple-modal';

export default class TrackItem extends Component {
    getChangesTypeText = (changesType, propertyChangesText) => {
        
        let changesTypeText = '';
        let test = JSON.parse(propertyChangesText);
        let data;
        switch(changesType)
        {
            case 0:
                changesTypeText = 'Undefined';
                data = test.map(x => (
                    <tr>
                        <td className="text-center">{x.Name}</td>
                        <td className="text-center">Old value: {x.OldValue}</td>
                        <td className="text-center">New value: {x.NewValue}</td>
                    </tr>
                ))
                return {
                    changesTypeText,
                    data
                };
            case 1:
                changesTypeText = 'Modified';
                data = test.map(x => (
                    <tr>
                        <td className="text-center">{x.Name}</td>
                        <td className="text-center">Old value: {x.OldValue}</td>
                        <td className="text-center">New value: {x.NewValue}</td>
                    </tr>
                ))
                return {
                    changesTypeText,
                    data
                };
            case 2:
                changesTypeText = 'Created';
                data = test.map(x => (
                    <tr>
                        <td className="text-center">{x.Name}</td>
                        <td className="text-center">New value: {x.NewValue}</td>
                    </tr>
                ))
                return {
                    changesTypeText,
                    data
                };
            case 3:
                changesTypeText = 'Deleted';
                data = test.map(x => (
                    <tr>
                        <td className="text-center">{x.Name}</td>
                        <td className="text-center">Old value: {x.OldValue}</td>
                    </tr>
                ))
                return {
                    changesTypeText,
                    data
                };
        }
    }

    filter = values => {
        this.props.filter({ ...values, id: this.props.item.id });
    };

    render() {
        const { propertyChangesText, entityKeys, time, name, changesType, user } = this.props.item;

        return (
            <tr>
                <td className="text-center">
                    {name}
                </td>
                <td className="text-center">
                    <Link to={`/user/${user.id}`}>{user.name}</Link>
                </td>
                <td className="text-center">
                    {new Date(time).toLocaleString()}
                </td>
                <td className="text-center">
                    {this.getChangesTypeText(changesType, propertyChangesText).changesTypeText}
                </td>
                <td className="text-center">
                    <SimpleModal
                        id = {user.id}
                        data = {this.getChangesTypeText(changesType, propertyChangesText).data}
                        button = {
                            <IconButton aria-label="delete">
                                <i className="fas fa-info-circle"/>
                            </IconButton>
                        }
                    />
                </td>
            </tr>
        );
    }
}