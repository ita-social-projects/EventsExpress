import React, { Component } from 'react';
import EventsExpressService from '../../services/EventsExpressService';
import CategoryItemWrapper from '../../containers/category-item';


export default class CategoryList extends Component {

    renderItems = (arr) => arr.map((item) => 
        <CategoryItemWrapper key={item.id} item={item} />);
    
    render() {
        const { data_list } = this.props;

        const items = this.renderItems(data_list);

        return <table className="w-50 table">
                <tbody>
                    {items}
                </tbody>
            </table> 
        
    }
}