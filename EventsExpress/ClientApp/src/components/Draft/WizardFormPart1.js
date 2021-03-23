import React, { Component } from 'react';
import { reduxForm, Field, getFormValues, reset, isPristine, initialize } from 'redux-form';
import { connect } from 'react-redux';
import moment from 'moment';
import { setEventPending, setEventSuccess, publish_event, edit_event_part1 } from '../../actions/event-add-action';
import { validateEventFormPart1 } from '../helpers/helpers'
import Button from "@material-ui/core/Button";
import 'react-widgets/dist/css/react-widgets.css'
import get_categories from '../../actions/category/category-list';
import momentLocaliser from 'react-widgets-moment';
import {
    renderMultiselect,
    renderTextArea,
    renderTextField,
    renderDatePicker,
} from '../helpers/helpers';
import { createBrowserHistory } from 'history';

momentLocaliser(moment);
const history = createBrowserHistory({ forceRefresh: true });

class Part1 extends Component {

    constructor(props) {
        super(props)
        props.get_categories()
    }       

    state = { initialized:false };

    onSubmit = () => {
        return this.props.add_event({ ...validateEventFormPart1(this.props.form_values), user_id: this.props.user_id, id: this.props.event.id });
    }

    initializeIfNeed() {
        if (this.props.event && !this.state.initialized) {
            let initialValues = {
                title: this.props.event.title,
                description: this.props.event.description,
                dateFrom: this.props.event.dateFrom,
                dateTo: this.props.event.dateTo,
                categories: this.props.event.categories,
            }
            this.props.initialize(initialValues);
            this.setState({ initialized: true });
        }
    }

    componentDidUpdate() {
        this.initializeIfNeed();
    }
    componentDidMount() {
        this.initializeIfNeed();
    }
    

    render() {

        

        

        const { form_values, all_categories, disabledDate, } = this.props;
        
        let values = form_values || this.props.initialValues;

        return (
            <form onSubmit={this.props.handleSubmit(this.onSubmit)}
                encType="multipart/form-data" autoComplete="off" >
                <div className="text text-2 pl-md-4">
                    <div className="mt-2">
                        <Field name='title'
                            component={renderTextField}
                            type="input"
                            label="Title"
                            inputProps={{ maxLength: 60 }}
                        />
                    </div>
                    <div className="meta-wrap m-2">
                        <span>From
                            <Field
                                name='dateFrom'
                                component={renderDatePicker}
                                disabled={disabledDate ? true : false}
                            />
                        </span>
                        {values && values.dateFrom &&
                            <span>To
                                <Field
                                    name='dateTo'
                                    minValue={values.dateFrom}
                                    component={renderDatePicker}
                                    disabled={disabledDate ? true : false}
                                />
                            </span>
                        }
                    </div>
                    <div className="mt-2">
                        <Field
                            name='description'
                            component={renderTextArea}
                            type="input"
                            label="Description"
                        />
                    </div>
                    <div className="mt-2">
                        <Field
                            name="categories"
                            component={renderMultiselect}
                            data={all_categories.data}
                            valueField={"id"}
                            textField={"name"}
                            className="form-control mt-2"
                            placeholder='#hashtags' />
                    </div>
                    <div className="col">
                        <Button
                            className="border"
                            fullWidth={true}
                            color="primary"
                            type="submit"
                        >
                            Save
                        </Button>
                    </div>
                </div>
         </form>
        );
    }
}

const mapStateToProps = (state) => ({
    initialData: state.event.data,
    user_id: state.user.id,
    all_categories: state.categories,
    form_values: getFormValues('Part1')(state),
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event_part1(data)),
        publish: (data) => dispatch(publish_event(data)),
        get_categories: () => dispatch(get_categories()),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('Part1'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
        }
    }
};

Part1 = connect(
    mapStateToProps,
    mapDispatchToProps,
)(Part1);

export default reduxForm({
    form: 'Part1',
    enableReinitialize: true
})(Part1);