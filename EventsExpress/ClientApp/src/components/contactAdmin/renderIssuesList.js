import React, { Component } from 'react';
import PagePagination from '../shared/pagePagination';

class RenderIssuesList extends Component {

    renderIssues = arr =>
        arr.map(item => this.props.renderSingleIssue(item));
    constructor(props)
    {
        super(props)
    }

    render() {
        const { page, totalPages, data_list } = this.props;

        return <>
                {data_list.length > 0
                ? this.renderIssues(data_list)
                    : <div id="notfound" className="w-100">
                        <div className="notfound">
                            <div className="notfound-404">
                                <div className="h1">No Results</div>
                            </div>
                        </div>
                    </div>}
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

export default RenderIssuesList;