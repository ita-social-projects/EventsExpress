import React, {Component} from 'react';
import Button from "@material-ui/core/Button";
import {Field, reduxForm} from 'redux-form';
import {renderTracksDatePicker, renderMultiselect} from "../helpers/helpers";
import changesTypeEnum from "../../constants/changesTypeEnum";
import EventFilterStatus from "../event/event-filter-status";

class TracksFilter extends Component {
    render() {
        const {entityNames, form_values} = this.props;
        let values = form_values || {};

        return <>
            {entityNames && entityNames.length !== 0 &&
            <form className="box" onSubmit={this.props.handleSubmit}>
                <div className="form-group">
                    <div className="form-group">
                        <Field
                            data={entityNames}
                            component={renderMultiselect}
                            name="entityNames"
                            valueField={"id"}
                            textField={"entityName"}
                            className="form-control mt-2"
                            placeholder='Entity name'
                        />
                    </div>
                    <div className="form-group">
                        <Field
                            options={[changesTypeEnum.Undefined, changesTypeEnum.Modified,
                                changesTypeEnum.Created, changesTypeEnum.Deleted]}
                            component={EventFilterStatus}
                            name="changesType"
                            className="form-control mt-2"
                            placeholder='Changes type'
                        />
                    </div>
                    <div className="form-group">
                        <Field
                            name='dateFrom'
                            label='From'
                            minValue={new Date()}
                            component={renderTracksDatePicker}
                        />
                    </div>
                    <div className="form-group">
                        <Field
                            name='dateTo'
                            label='To'
                            minValue={new Date(values.dateFrom)}
                            component={renderTracksDatePicker}
                        />
                    </div>
                </div>
                <div className="form-group">
                    <Button
                        fullWidth={true}
                        type="submit"
                        color="primary"
                        disabled={this.props.pristine || this.props.submitting}
                    >
                        Search
                    </Button>
                </div>
            </form>
            }
        </>
    }
}

TracksFilter = reduxForm({form: 'tracks-filter-form'})(TracksFilter);

export default (TracksFilter);

