import React, { Component } from 'react';
import UnitOfMeasuringItemWrapper from '../../containers/unitsOfMeasuring/unitOfMeasuring-item';


export default class UnitOfMeasuringList extends Component {

    render() {
        const { data_list } = this.props;

        return (
            <>
                <tr>
                    <td>Unit name</td>
                    <td className="d-flex align-items-center justify-content-center">Short name</td>                    
                </tr>
                {data_list.map(item => <UnitOfMeasuringItemWrapper                    
                    key={item.id}
                    item={item} />)}
          </>);
    }
}