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
            <tr>
                <td className="text-center">Entity Name</td>
                <td className="text-center">User name</td>
                <td className="text-center">Date</td>
                <td className="text-center">Changes type</td>
                <td></td>
                <td rowSpan="4">
                    {!Object.keys(entityNames).length == 0 &&
                    <form className="box" onSubmit={() => this.props.onSubmit(arguments)}>
                        {/* <Field name="status" component={radioButton} labels={'', ''}>
                                <Radio value="true" label="AG" />
                                <Radio value="true" label="Active" />
                                <Radio value="true" label="Blocked" />
                            </Field>
                            <div>From</div>
                            <Field
                                name='dateFrom'
                                component={renderDatePicker}
                            />
                            <div>To</div>
                            <Field
                                name='dateTo'
                                defaultValue={values.dateFrom}
                                minValue={values.dateFrom}
                                component={renderDatePicker}
                            /> */}
                        <div className="form-group">
                            {/* <Field
                                    name="entityName"
                                    component={renderMultiselect}
                                    data={data_list.items}
                                    valueField={"id"}
                                    textField={"name"}
                                    
                                    // onChange={(items) => { 
                                    //     var x = items
                                    // }}
                                /> */}
                            <Multiselect
                                data={entityNames}
                                valueField={"id"}
                                textField={"name"}
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
                                onClick={() => this.props.onSearch(1) }
                            >
                                Search
                            </Button>
                        </div>
                    </form>
                    }
                </td>
            </tr>
            {data_list.items.length > 0
                ? this.renderItems(data_list.items)
                : <div id="notfound" className="w-100">
                    <div className="notfound">
                        <div className="notfound-404">
                            <div className="h1">No Results</div>
                        </div>
                    </div>
                </div>}

            {data_list.pageViewModel.totalPages > 1 &&
            <PagePagination
                currentPage={data_list.pageViewModel.pageNumber}
                totalPages={data_list.pageViewModel.totalPages}
                callback={this.handlePageChange}
            />
            }
        </>);
    }
}

export default TrackList;
