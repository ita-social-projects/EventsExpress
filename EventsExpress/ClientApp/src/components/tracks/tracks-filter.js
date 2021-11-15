import React, {Component} from 'react';
import Button from "@material-ui/core/Button";
import {Field, reduxForm} from 'redux-form';
import changesTypeEnum from "../../constants/changesTypeEnum";
import { MultiCheckbox, renderDatePicker, renderMultiselect, parseDate} from "../helpers/form-helpers";

class TracksFilter extends Component {
    render() {
        const {entityNames, form_values} = this.props;
        let values = form_values || {};
        let options = [
            {value: changesTypeEnum.Undefined, text: "Undefined"},
            {value: changesTypeEnum.Modified, text: "Modified"},
            {value: changesTypeEnum.Created, text: "Created"},
            {value: changesTypeEnum.Deleted, text: "Deleted"}
        ];

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
                            options={options}
                            component={MultiCheckbox}
                            name="changesType"
                            className="form-control mt-2"
                            placeholder='Changes type'
                        />
                    </div>
                    <div className="form-group">
                        <Field
                            name='dateFrom'
                            label='From'
                            component={renderDatePicker}
                            parse={parseDate}
                        />
                    </div>
                    <div className="form-group">
                        <Field
                            name='dateTo'
                            label='To'
                            minValue={new Date(values.dateFrom)}
                            component={renderDatePicker}
                            parse={parseDate}
                        />
                    </div>
                </div>
                <div className="form-group d-flex">
                    <Button
                        fullWidth={true}
                        color="primary"
                        onClick={this.props.onReset}
                        disabled={this.props.submitting}
                    >
                        Reset
                    </Button>
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

