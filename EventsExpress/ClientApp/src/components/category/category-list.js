import React, { Component } from 'react';
import CategoryItemWrapper from '../../containers/categories/category-item';

export default class CategoryList extends Component {

    renderItems = arr => arr.map(item => <CategoryItemWrapper
        key={item.id}
        item={item} />);

    render() {
        const { data_list } = this.props;

        return (
            <>
                <tr>

                    <td>Name</td>
                    <td className="d-flex align-items-center justify-content-center">Users</td>
                    <td className="justify-content-center">Events</td>

                </tr>
                {this.renderItems(data_list)}
            </>);
    }
}