import React, { Component } from 'react';
import { reduxForm, Field, getFormValues, reset, Form, isPristine } from 'redux-form';
import { compose } from 'redux'
import { connect } from 'react-redux';
import { setEventPending, setEventSuccess, publish_event, edit_event_part1 } from '../../actions/event-add-action';
import { validateEventFormPart1 } from '../helpers/helpers'
import 'react-widgets/dist/css/react-widgets.css'
import get_categories from '../../actions/category/category-list';
import periodicity from '../../constants/PeriodicityConstants'
import submit from './submit';
import {
    renderMultiselect,
    renderTextArea,
    renderTextField,
    renderDatePicker,
    renderSelectPeriodicityField,
    renderCheckbox
} from '../helpers/helpers';
import { warn } from './Validator';



class Part1 extends Component {
    
    
    constructor(props) {
        super(props)
        props.get_categories()
    }       

    state = { checked:false };

    
    handleChange = () => {
        this.setState(state => ({
            checked: !state.checked,
        }));

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
    state = { initialized: false };
    componentDidUpdate() {
        this.initializeIfNeed();

    }
    componentDidMount() {
        this.initializeIfNeed();  
    }

    renderErrors = (error) => {
        const keys = Object.keys(error);
        let i = 0;
        const part1Errors = [];
        while (i < keys.length) {
            if (keys[i] === "title" || keys[i] === "description" || keys[i] === "dateFrom" || keys[i] === "dateTo" || keys[i] === "categories") {
                part1Errors.push(keys[i]);
            }
            i++;
        }
        return part1Errors.map(k => <div className="text-warning">{k}:{error[k][0]}</div>)
    }

    

    onSubmit = (values) => {
        return this.props.add_event({ ...validateEventFormPart1(this.props.form_values), user_id: this.props.user_id, id: this.props.event.id });
    }


    
    render() {

        

        const { checked } = this.state;
        
        const { test ,form_values, all_categories, disabledDate, } = this.props;
        
        let values = form_values || this.props.initialValues;
        const { error, handleSubmit, pristine } = this.props;

        return (
            <form onSubmit={handleSubmit}
                encType="multipart/form-data" autoComplete="off" >
                <div className="text text-2 pl-md-4">
                    <div className={"mt-2"}>
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
                        <br />
                        <Field
                            type="checkbox"
                            label="Recurrent Event"
                            name='isReccurent'
                            component={renderCheckbox}
                            checked={checked}
                            onChange={this.handleChange} />
                    </div>
                    {checked &&
                        <div>
                            <div className="mt-2">
                                <Field
                                    name="periodicity"
                                    text="Periodicity"
                                    data={periodicity}
                                    component={renderSelectPeriodicityField} />
                            </div>
                            <div className="mt-2">
                                <Field
                                    name='frequency'
                                    type="number"
                                    component={renderTextField} />
                            </div>
                        </div>
                    }

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
    errors: state.publishErrors.data,
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

export default compose(
    connect(mapStateToProps, mapDispatchToProps),
    reduxForm({ form: 'Part1', warn, onSubmit: submit })
)(Part1);