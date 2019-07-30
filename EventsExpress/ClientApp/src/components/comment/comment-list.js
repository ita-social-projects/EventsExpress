import React, { Component } from 'react';
import CommentItemWrapper from '../../containers/delete-comment';


export default class CommentList extends Component {

    renderItems = (arr) => {
        return arr.map((item) => {

            return (
                <CommentItemWrapper key={item.id} item={item} />
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