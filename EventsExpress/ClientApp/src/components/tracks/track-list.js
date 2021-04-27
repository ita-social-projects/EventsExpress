import React, {Component} from 'react';
import PagePagination from '../shared/pagePagination';
import TrackItem from './track-item';

class TrackList extends Component {
    
    renderItems = arr => {
        return arr.map(item => (
            <TrackItem
                item={item}
            />
        ));
    }

    render() {
        const {data_list, handlePageChange} = this.props;

        return (<>
            <div className="d-flex">
                <div className="table-responsive">
                    <table className="table">
                        <thead>
                        <tr>
                            <th scope="col" className="text-center">Entity Name</th>
                            <th scope="col" className="text-center">User name</th>
                            <th scope="col" className="text-center">Date</th>
                            <th scope="col" className="text-center">Changes type</th>
                            <th/>
                        </tr>
                        </thead>
                        <tbody>
                        {data_list.items.length > 0
                            ? this.renderItems(data_list.items)
                            : <div id="notfound" className="w-100">
                                <div className="notfound">
                                    <div className="notfound-404">
                                        <div className="h1">No Results</div>
                                    </div>
                                </div>
                            </div>
                        }
                        </tbody>
                    </table>
                    {data_list.pageViewModel.totalPages > 1 &&
                    <PagePagination
                        currentPage={data_list.pageViewModel.pageNumber}
                        totalPages={data_list.pageViewModel.totalPages}
                        callback={handlePageChange}
                    />
                    }
                </div>
            </div>
        </>);
    }
}

export default TrackList;