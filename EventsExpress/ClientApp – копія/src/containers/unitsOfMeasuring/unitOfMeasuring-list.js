import React, { Component } from 'react';
import UnitOfMeasuringList from '../../components/unitOfMeasuring/unitOfMeasuring-list';


export default class UnitOfMeasuringListWrapper extends Component {

    render() {
        const { data,add_unitOfMeasuring } = this.props;

        return <UnitOfMeasuringList data_list={data} /> 
    }
}