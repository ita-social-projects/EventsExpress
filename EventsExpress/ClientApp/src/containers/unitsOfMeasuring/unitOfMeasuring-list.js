import React, { Component } from 'react';
import UnitOfMeasuringList from '../../components/unitOfMeasuring/unitOfMeasuring-list';


export default class UnitOfMeasuringListWrapper extends Component {

    render() {
        const { data} = this.props;

        return <UnitOfMeasuringList data_list={data} /> 
    }
}