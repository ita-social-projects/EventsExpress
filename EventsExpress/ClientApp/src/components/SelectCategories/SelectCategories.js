import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import Button from "@material-ui/core/Button";
import { renderMultiselect } from '../helpers/form-helpers';
import ErrorMessages from '../shared/errorMessage';

class SelectCategories extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.initialize({
            categories: this.props.initialValues,
        });
    }

    render() {
        const { handleSubmit, submitting, items, error } = this.props;

        return (
            <div >
                <form name = "SelectCategories" onSubmit={handleSubmit}>
                    <Field
                        name="categories"
                        component={renderMultiselect}
                        data={items}
                        valueField={"id"}
                        textField={"name"}
                        className="form-control mt-2"
                    />
                    {
                        error &&
                        <ErrorMessages error={error} className="text-center" />
                    }
                    <div>
                        <Button
                            type="submit"
                            color="primary"
                            disabled={submitting} >
                            Save
                        </Button >
                    </div>
                </form>
            </div>
        );
    }
}

export default reduxForm({
    form: 'SelectCategories',
})(SelectCategories)
