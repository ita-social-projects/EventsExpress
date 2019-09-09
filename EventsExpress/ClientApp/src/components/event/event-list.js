import React, { Component } from 'react';
import Event from './event-item';
import Pagination from "react-paginating";
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import {reset_events} from '../../actions/event-list';
const limit = 2;
const pageCount = 3;

class EventList extends Component {
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }

    componentWillUnmount = () => {
        this.props.reset_events();
    }

    handlePageChange = (page, e) => {
        this.props.callback(window.location.search.replace(/(page=)[0-9]+/gm, 'page=' + page));
        this.setState({
            currentPage: page
        });
    };

    renderItems = arr => 
        arr.map(item => (
            <Event 
                key={item.id+item.isBlocked} 
                item={item} 
                current_user={this.props.current_user}                   
            />
    ));
        
 
    render() {
        const { data_list } = this.props;
        const items = this.renderItems(data_list);
        const { page, totalPages } = this.props;
        
        return <>
                <div className="row">
                        {data_list.length > 0 ? items : <div id="notfound" className="w-100"><div className="notfound"> <div className="notfound-404"><div className="h1">No Results</div></div> </div></div>}
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

const mapDispatchToProps = (dispatch) => { 
    return {
        reset_events: () => dispatch(reset_events())
    } 
};

export default connect(null, mapDispatchToProps)(EventList);