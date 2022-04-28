import React, { Component } from 'react';
import CategoryGroupItemWrapper from "../../containers/categoriesGroups/category-group-item-wrapper";

export default class CategoryGroupList extends Component {

    renderItems = arr => arr.map(item => <CategoryGroupItemWrapper
        key={item.id}
        item={item} />);

    render() {
        const { data_list } = this.props;

        return (
            <>
                <tr>
                    <td>Name</td>
                    <td></td>
                    <td></td>
                </tr>
                {this.renderItems(data_list)}
            </>);
    }
}