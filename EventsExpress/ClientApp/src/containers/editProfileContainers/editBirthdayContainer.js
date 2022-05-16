import React from "react";
import { connect } from "react-redux";
import editBirthday from "../../actions/redactProfile/birthday-edit-action";
import EditProfilePropertyWrapper from "../../components/profile/editProfile/editProfilePropertyWrapper";
import moment from 'moment';
import { Field } from "redux-form";
import { renderDatePicker } from "../../components/helpers/form-helpers";
import { parseEuDate } from "../../components/helpers/form-helpers";
const today = () => moment().startOf('day');

const MIN_AGE = 14;
const MAX_AGE = 115;
const MIN_DATE = today().subtract(MAX_AGE, 'years');
const MAX_DATE = today().subtract(MIN_AGE, 'years');

class EditBirthdayContainer extends React.Component {
    submit = value => {
        return this.props.editBirthday(value).then(this.props.close);
    }
    render() {
        return <EditProfilePropertyWrapper onSubmit ={this.submit} onClose ={this.props.close}>
                <Field
                        name="birthday"
                        label="Birthday"
                        minValue={MIN_DATE}
                        maxValue={MAX_DATE}
                        component={renderDatePicker}
                        parse={parseEuDate}
                    />
            </EditProfilePropertyWrapper>;
    }
}

const mapStateToProps = state => {
    return state.editBirthday;
};

const mapDispatchToProps = dispatch => {
    return {
        editBirthday: (date) => dispatch(editBirthday(date))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditBirthdayContainer);