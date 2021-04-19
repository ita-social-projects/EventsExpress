import React, {Component} from 'react';
import Button from "@material-ui/core/Button";
import {Field, reduxForm} from 'redux-form';
import {renderMultiselect} from "../helpers/helpers";


class TracksFilter extends Component {
    render() {
        const {entityNames, changesType} = this.props;

        return <>
            {entityNames && entityNames.length !== 0 &&
            <form className="box" onSubmit={this.props.handleSubmit}>
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
                    <Field
                        data={changesType}
                        component={renderMultiselect}
                        name="changesType"
                        valueField={"id"}
                        textField={"changesType"}
                        className="form-control mt-2"
                        placeholder='Changes type'
                    />
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

