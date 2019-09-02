import React, { Component } from 'react';
import CommentItemWrapper from '../../containers/delete-comment';
import { Link } from 'react-router-dom'
import Pagination from "react-paginating";
const limit = 2;
const pageCount = 3;


export default class CommentList extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }
    handlePageChange = (page,  e) => {
        this.props.callback(this.props.evId, page);
        this.setState({
            currentPage: page
        });
    };

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
        const { page, totalPages } = this.props;
        return <>
           
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
                                    to={window.location.pathname.replace(/[/].$/g, '/' + page)}
                                    {...getPageItemProps({
                                        pageValue: 1,
                                        onPageChange: this.handlePageChange
                                    })}
                                >
                                    first
                              </Link>)}

                                {hasPreviousPage && (
                                    <Link className="btn btn-primary"
                                        to={window.location.pathname.replace(/[/].$/g, '/' + page)}

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
                                    if (totalPages != 1) {
                                        return (
                                            <Link className="btn btn-primary"
                                                to={window.location.pathname.replace(/[/].$/g, '/' + page)}

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
                                    } else {
                                        return (
                                            <Link 
                                                to={window.location.pathname.replace(/[/].$/g, '/' + page)}

                                                {...getPageItemProps({
                                                    pageValue: page,
                                                    key: page,
                                                    style: activePage,
                                                    onPageChange: this.handlePageChange
                                                })}
                                            >
                                              
                                            </Link>
                                        );
                                    }
                                })}

                                {hasNextPage && (
                                    <Link className="btn btn-primary"
                                        to={window.location.pathname.replace(/[/].$/g, '/' + page)}

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
                                    to={window.location.pathname.replace(/[/].$/g, '/' + page)}

                                    {...getPageItemProps({
                                        pageValue: this.props.totalPages,
                                        onPageChange: this.handlePageChange
                                    })}
                                >
                                    last
                                </Link>  )}
                            </div>
                        )}
                </Pagination>
            </ul>
            {items}
        </>
    }
}