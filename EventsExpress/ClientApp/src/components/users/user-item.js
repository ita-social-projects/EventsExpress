import React, { Component } from 'react';
import UserInfoCard from '../user-info/User-info-card'
import Pagination from "react-paginating";
import { Link } from 'react-router-dom'

const limit = 2;
const pageCount = 3;


export default class UserItemList extends Component {
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


    renderUsers = (arr) => {
        return arr.map(user =>
            <UserInfoCard key={user.id} user={user} />);
    }

    render() {
        console.log(window.location);
        const { page, totalPages } = this.props;
        return <>
            {this.renderUsers(this.props.users)}

            <ul class="pagination justify-content-center">
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
                                    <Link class="btn btn-primary"
                                        to={window.location.search.replace(/(page=)\w/gm, 'page=' + 1)}
                                        {...getPageItemProps({
                                            pageValue: 1,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        first
                              </Link>)}

                                {hasPreviousPage && (
                                    <Link class="btn btn-primary"
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
                                        <Link class="btn btn-primary"
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
                                    <Link class="btn btn-primary"
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
                                    <Link class="btn btn-primary"
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