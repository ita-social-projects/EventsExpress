import React, { Component } from 'react';
import CategoryItemWrapper from '../../containers/categories/category-item';


export default class CategoryList extends Component {

    renderItems = arr => arr.map(item => <CategoryItemWrapper 
        key={item.id} 
        item={item} />);
    
    render() {
        const { data_list } = this.props;

        return this.renderItems(data_list)
               
    }
}