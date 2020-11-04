import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './users.css';
import UserInfoWpapper from '../../containers/user-info';
import Pagination from "react-paginating";

const limit = 2;
const pageCount = 3;

export default class Users extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }

    handlePageChange = (page, e) => {
        this.props.callback(window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + page));
        this.setState({
            currentPage: page
        });
    };

    renderUsers = (arr) => {
        return arr.map(user =>
            <UserInfoWpapper
                key={user.id + user.isBlocked + user.role.id}
                user={user}
            />
        );
    }

    render() {
        const { page, totalPages } = this.props;
        return <>
            <table className="table">
                <tbody>
                    {this.renderUsers(this.props.users)}
                </tbody>
            </table>
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
                                        to={window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + 1)}
                                        {...getPageItemProps({
                                            pageValue: 1,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        first
                                    </Link>)}
                                {hasPreviousPage && (
                                    <Link className="btn btn-primary"
                                        to={window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + (page - 1))}
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
                                                to={window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + page)}
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
                                                to={window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + page)}
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
                                        to={window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + (page + 1))}
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
                                        to={window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + this.props.totalPages)}
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
