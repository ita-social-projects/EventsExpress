import React, { Component } from 'react';
import Pagination from "react-paginating";

class PagePagination extends Component {
    constructor(props) {
        super(props);
        this.limit = 6;
        this.pageCount = 4;
        this.state = {
            currentPage: 1
        };
    }

    handlePageChange = (page, e) => {
        this.setState({
            currentPage: page
        });
        this.props.callback(page);
    };


    render() {
        return <ul className="pagination justify-content-center">
            <Pagination
                total={this.props.totalPages * this.limit}
                limit={this.limit}
                pageCount={this.pageCount}
                currentPage={this.props.currentPage}
            >
                {({
                    pages,
                    currentPage,
                    hasNextPage,
                    hasPreviousPage,
                    previousPage,
                    nextPage,
                    totalPages,
                    getPageItemProps
                }) => (
                        <div>
                            {hasPreviousPage &&
                                <>
                                    <button className="btn btn-primary"
                                        {...getPageItemProps({
                                            pageValue: 1,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        first
                                        </button>
                                    <button className="btn btn-primary"
                                        {...getPageItemProps({
                                            pageValue: previousPage,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        {"<"}
                                    </button>
                                </>
                            }

                            {pages.map(page => {
                                let activePage = null;

                                if (currentPage === page) {
                                    activePage = { backgroundColor: "	#ffffff", color: "#00BFFF" };
                                }

                                return (
                                    <button className="btn btn-primary"
                                        {...getPageItemProps({
                                            pageValue: page,
                                            key: page,
                                            style: activePage,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        {page}
                                    </button>
                                );
                            })}

                            {hasNextPage &&
                                <>
                                    <button className="btn btn-primary"
                                        {...getPageItemProps({
                                            pageValue: nextPage,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        {">"}
                                    </button>
                                    <button className="btn btn-primary"
                                        {...getPageItemProps({
                                            pageValue: totalPages,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        last
                                        </button>
                                </>
                            }
                        </div>
                    )}
            </Pagination>
        </ul>
    }
}

export default PagePagination;
