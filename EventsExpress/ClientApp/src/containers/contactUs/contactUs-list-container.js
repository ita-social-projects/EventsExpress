import React, { Component } from 'react';
import ContactUsList from '../../components/contactUs/contactUs-list-component';
import PagePagination from '../../components/shared/pagePagination';


export default class ContactUsListWrapper extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }

    handlePageChange = (page) => {
        this.props.callback(page);
        this.setState({
            currentPage: page
        });
    };
    render() {
        const { data } = this.props;
        const { page, totalPages } = this.props;
        return <>
            <ContactUsList data_list={data} />
            {totalPages > 1 &&
                <PagePagination
                    currentPage={page}
                    totalPages={totalPages}
                    callback={this.handlePageChange}
                />
            }
        </>
    }
}

