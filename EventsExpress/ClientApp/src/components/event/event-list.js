import React, {Component} from 'react';
import EventsExpressService from '../../services/EventsExpressService';
import Event from './event-item';
import Pagination from "react-paginating";
const fruits = [
    ["apple", "orange"],
    ["banana", "avocado"],
    ["coconut", "blueberry"],
    ["payaya", "peach"],
    ["pear", "plum"]
];
const limit = 2;
const pageCount = 10;
const total = fruits.length * limit;


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
      
      const items = this.renderItems(data_list);
       const {currentPage} = this.state;
      return <>
          {items}
  
      <div>
              <ul>
                 
               
              </ul>
              <Pagination
                  total={total}
                  limit={limit}
                  pageCount={pageCount}
                  currentPage={currentPage}
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
                              <button
                                  {...getPageItemProps({
                                      pageValue: 1,
                                      onPageChange: this.handlePageChange
                                  })}
                              >
                                  first
                              </button>

                              {hasPreviousPage && (
                                  <button
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
                                      activePage = { backgroundColor: "#fdce09" };
                                  }
                                  return (
                                      <button
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

                              {hasNextPage && (
                                  <button
                                      {...getPageItemProps({
                                          pageValue: nextPage,
                                          onPageChange: this.handlePageChange
                                      })}
                                  >
                                      {">"}
                                  </button>
                              )}

                              <button
                                  {...getPageItemProps({
                                      pageValue: totalPages,
                                      onPageChange: this.handlePageChange
                                  })}
                              >
                                  last
                               </button>
                          </div>
                      )}
              </Pagination>
          </div>
          
      </>
  }
}