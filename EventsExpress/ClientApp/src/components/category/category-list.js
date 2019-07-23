import React, { Component } from 'react';
import EventsExpressService from '../../services/EventsExpressService';
import CategoryItemWrapper from '../../containers/delete-category';


export default class CategoryList extends Component {

    renderItems = (arr) => {
         return arr.map((item) => {
     
           return (
               <CategoryItemWrapper key={item.id} item={item} />
           );
         });
    }
    render() {
        const { data_list } = this.props;

        const items = this.renderItems(data_list);

        return <>
            <div className="listbox">
                {items}
                </div>
        </>
    }
}