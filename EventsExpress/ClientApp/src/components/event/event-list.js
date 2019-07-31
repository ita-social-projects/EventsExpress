import React, {Component} from 'react';
import Event from './event-item';
import Pagination from "react-paginating";
import { Link } from 'react-router-dom'

const limit = 2;
const pageCount = 3;



export default class EventList extends Component{
    constructor() {
        super();
        this.state = {
            currentPage: 1
        };
    }

    handlePageChange = (page, e) => {
        console.log("chanhe page"+page);
        this.props.callback(page);
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

  render()
  {
      const { data_list } = this.props;
      console.log(window.location.pathname.replace(/[/].$/g, "/stas"));

      const items = this.renderItems(data_list);
      const { page, totalPages } = this.props;
      return <>
          {items}

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
                              <Link class="btn btn-primary"
                                  to={window.location.pathname.replace(/[/].$/g, '/'+page)}
                                  {...getPageItemProps({
                                      pageValue: 1,
                                      onPageChange: this.handlePageChange
                                  })}
                              >
                                  first
                              </Link>

                              {hasPreviousPage && (
                                  <Link class="btn btn-primary"
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
                                      activePage = { backgroundColor: "	#ffffff", color: "#00BFFF"};
                                  }
                                  return (
                                      <Link class="btn btn-primary"
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
                              })}

                              {hasNextPage && (
                                  <Link class="btn btn-primary"
                                      to={window.location.pathname.replace(/[/].$/g, '/' + page)}
                                  
                                      {...getPageItemProps({
                                          pageValue: nextPage,
                                          onPageChange: this.handlePageChange
                                      })}
                                  >
                                      {">"}
                                  </Link>
                              )}
                              <Link class="btn btn-primary"
                                  to={window.location.pathname.replace(/[/].$/g, '/' + page)}
                                 
                                  {...getPageItemProps({
                                      pageValue: this.props.totalPages,
                                      onPageChange: this.handlePageChange
                                  })}
                              >
                                  last
                                </Link>
                          </div>
                      )}
              </Pagination>
          </ul>
          
      </>
  }
}