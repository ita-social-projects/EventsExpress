import React, { Component } from 'react';
import UnitOfMeasuringItemWrapper from '../../containers/unitsOfMeasuring/unitOfMeasuring-item';

export default class UnitOfMeasuringList extends Component {

    render() {
        const { data_list } = this.props;

        return (
            <>
                <tr className="font-weight-bold">
                    <td width="20%">Group</td>
                    <td width="20%">Unit name</td>
                    <td width="20%">Short name</td>
                    <td width="45%" colSpan="3"></td>
                </tr>
                {data_list.map(item => <UnitOfMeasuringItemWrapper
                    key={item.id}
                    item={item} />)}
            </>);
    }
}