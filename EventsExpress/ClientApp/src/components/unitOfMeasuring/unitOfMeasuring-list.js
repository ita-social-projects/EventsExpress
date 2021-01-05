import React, { Component } from 'react';
import UnitOfMeasuringItemWrapper from '../../containers/unitsOfMeasuring/unitOfMeasuring-item';

export default class UnitOfMeasuringList extends Component {

    render() {
        const { data_list } = this.props;

        return (
            <>
            {/* <tr>
            <td className="align-middle w-100" width="75%">
                    <div style={divStyle} className="d-flex flex justify-content-around ">
                        <span>Unit name</span>
                        <span>Short name</span>
                    </div>
            </td>
            </tr> */}
            
                <tr>
                    <td width="36%">Unit name</td>
                    <td width="24%">Short name</td>
                    <td width="45%" colSpan="3"></td>
                </tr>
                {data_list.map(item => <UnitOfMeasuringItemWrapper                    
                    key={item.id}
                    item={item} />)}
          </>);
    }
}