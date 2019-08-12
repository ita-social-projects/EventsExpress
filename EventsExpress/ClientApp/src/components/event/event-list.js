import React, { Component } from 'react';
import Event from './event-item';
import Pagination from "react-paginating";
import { Link } from 'react-router-dom'

const limit = 2;
const pageCount = 3;



export default class EventList extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }

    handlePageChange = (page, e) => {
        this.props.callback(window.location.search.replace(/(page=)\w/gm, 'page=' + page));
        this.setState({
            currentPage: page
        });
    };


    renderItems = (arr) => {
        return arr.map((item) => {

            return (
                <Event key={item.id} item={item} />
            );
        });
    }

    render() {
        const { data_list } = this.props;
        const items = this.renderItems(data_list);
        const { page, totalPages } = this.props;

        return <>
            <div className="row">
                {items}
            </div>
            <br />
            <ul className="pagination justify-content-center">
                <Pagination
                    total={totalPages * limit}
                    limit={limit}
                    pageCount={pageCount}
                    currentPage={page}
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
                                {hasPreviousPage && (
                                    <Link className="btn btn-primary"
                                        to={window.location.search.replace(/(page=)\w/gm, 'page=' + 1)}
                                        {...getPageItemProps({
                                            pageValue: 1,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        first
                              </Link>)}

                                {hasPreviousPage && (
                                    <Link className="btn btn-primary"
                                        to={window.location.search.replace(/(page=)\w/gm, 'page=' + (page - 1))}

                                        {...getPageItemProps({
                                            pageValue: previousPage,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        {"<"}
                                    </Link>
                                )}

                                {pages.map(page => {
                                    let activePage = null;
                                    if (currentPage === page) {
                                        activePage = { backgroundColor: "	#ffffff", color: "#00BFFF" };
                                    }
                                    return (
                                        <Link className="btn btn-primary"
                                            to={window.location.search.replace(/(page=)\w/gm, 'page=' + page)}

                                            {...getPageItemProps({
                                                pageValue: page,
                                                key: page,
                                                style: activePage,
                                                onPageChange: this.handlePageChange
                                            })}
                                        >
                                            {page}
                                        </Link>
                                    );
                                })}

                                {hasNextPage && (
                                    <Link className="btn btn-primary"
                                        to={window.location.search.replace(/(page=)\w/gm, 'page=' + (page + 1))}

                                        {...getPageItemProps({
                                            pageValue: nextPage,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        {">"}
                                    </Link>
                                )}
                                {hasNextPage && (
                                    <Link className="btn btn-primary"
                                        to={window.location.search.replace(/(page=)\w/gm, 'page=' + this.props.totalPages)}

                                        {...getPageItemProps({
                                            pageValue: this.props.totalPages,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        last
                                </Link>)}
                            </div>
                        )}
                </Pagination>
            </ul>

        </>
    }
}