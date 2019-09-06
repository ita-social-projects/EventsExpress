import React, { Component } from 'react';
import CategoryList from '../../components/category/category-list';


export default class CategoryListWrapper extends Component {

    render() {
        const { data } = this.props;

        return <CategoryList data_list={data} /> 
    }
}