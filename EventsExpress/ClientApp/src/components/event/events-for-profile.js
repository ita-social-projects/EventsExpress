import React, { Component } from 'react';
import Event from './event-item';
import Pagination from "react-paginating";
import { Link } from 'react-router-dom'

const limit = 2;
const pageCount = 3;



export default class EventsForProfile extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }

    handlePageChange = (page, e) => {
        this.setState({
            currentPage: page
        });
        console.log(page);
        this.props.callback(page);

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
                                    <button className="btn btn-primary"
                                        {...getPageItemProps({
                                            pageValue: 1,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        first
                              </button>)}

                                {hasPreviousPage && (
                                    <button className="btn btn-primary"
                                       
                                        {...getPageItemProps({
                                            pageValue: previousPage,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        {"<"}
                                    </button>
                                )}

                                {pages.map(page => {
                                    let activePage = null;
                                    if (currentPage === page) {
                                        activePage = { backgroundColor: "	#ffffff", color: "#00BFFF" };
                                    }
                                    if (totalPages != 1) {
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
                                    } else {

                                        return (
                                    <></>
                                        );
                                    }
                                })}

                                {hasNextPage && (
                                    <button className="btn btn-primary"
                                        
                                        {...getPageItemProps({
                                            pageValue: nextPage,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        {">"}
                                    </button>
                                )}
                                {hasNextPage && (
                                    <button className="btn btn-primary"
                                        
                                        {...getPageItemProps({
                                            pageValue: this.props.totalPages,
                                            onPageChange: this.handlePageChange
                                        })}
                                    >
                                        last
                                </button>)}
                            </div>
                        )}
                </Pagination>
            </ul>

        </>
    }
}