import React, { Component } from 'react';
import PagePagination from '../shared/pagePagination';

class RenderList extends Component {

    renderItems = arr =>
        arr.map(item => this.props.renderSingleItem(item));

    constructor(props)
    {
        super(props)
    }

    render() {
        const { page, totalPages, data_list } = this.props;

        return <>
            <div className="row">
                {data_list.length > 0
                    ? this.renderItems(data_list)
                    : <div id="notfound" className="w-100">
                        <div className="notfound">
                            <div className="notfound-404">
                                <div className="h1">No Results</div>
                            </div>
                        </div>
                    </div>}
            </div>
            <br />
            {totalPages > 1 &&
                <PagePagination
                    currentPage={page}
                    totalPages={totalPages}
                    callback={this.props.handlePageChange}
                />
            }
        </>
    }
}

export default RenderList;