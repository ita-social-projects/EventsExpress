import React, { Component } from 'react';
import EventsExpressService from '../../services/EventsExpressService';
import Category from './category-item';

export default class CategoryList extends Component {

    renderItems = (arr) => {
         return arr.map((item) => {
     
           return (
               <Category key={item.id} item={item} />
           );
         });
    }
    render() {
        const { data_list } = this.props;

        const items = this.renderItems(data_list);

        return <>
            {items}
        </>
    }
}