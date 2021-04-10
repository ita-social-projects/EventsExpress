import React, {Component} from 'react';
import Button from "@material-ui/core/Button";
import Radio from '@material-ui/core/Radio';
import {reduxForm, Field} from 'redux-form';
import PagePagination from '../shared/pagePagination';
import TrackItem from './track-item';
import {
    radioButton,
    renderDatePicker,
    renderMultiselect
} from '../helpers/helpers';
import Multiselect from 'react-widgets/lib/Multiselect';

class TrackList extends Component {

    handlePageChange = (page) => {
        this.props.onSearch(page);
    };

    renderItems = arr => {
        return arr.map(item => (
            <TrackItem
                item={item}
            />
        ));
    }

    render() {
        const {data_list, entityNames} = this.props;

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
                            <th />
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
                        callback={this.handlePageChange}
                    />
                    }
                </div>
                <div className="w-25">
                    {entityNames && entityNames.length !== 0 &&
                    <form className="box" onSubmit={() => this.props.onSubmit(arguments)}>
                        <div className="form-group">
                            <Multiselect
                                data={entityNames}
                                valueField={"id"}
                                textField={"entityName"}
                                className="form-control mt-2"
                                placeholder='Entity name'
                                onChange={(items) => {
                                    this.props.onEntitiesSelected(items.map(x => x.name))
                                }}
                            />
                        </div>
                        <div className="form-group">
                            <Button
                                fullWidth={true}
                                color="primary"
                                disabled={this.props.pristine || this.props.submitting}
                                onClick={() => this.props.onSearch(1)}
                            >
                                Search
                            </Button>
                        </div>
                    </form>
                    }
                </div>
            </div>
        </>);
    }
}

export default TrackList;
