import React, {Component} from 'react';
import {connect} from 'react-redux';
import {Field, reduxForm} from "redux-form";
import {minLength10, minLength20} from "../../components/helpers/validators/min-max-length-validators";
import {Button, TextField} from "@material-ui/core";
import NotificationTemplateForm from "../../components/notification-template/notification-template-form";
import MuiThemeProvider from "material-ui/styles/MuiThemeProvider";
import ChangeAvatarWrapper from "../editProfileContainers/change-avatar";
import DropZoneField from "../../components/helpers/DropZoneField";
import PhotoService from "../../services/PhotoService";
const photoService = new PhotoService();


class CategoryGroupInfoWrapper extends Component {

    constructor(props) {
        super(props)
        this.state = { copiedPropName: null };
    }

    renderField = ({ input, meta: { error }, ...props }) => {
        return (
            <div className="form-group">
                <TextField
                    {...input}
                    {...props}
                    error={Boolean(error)}
                    helperText={error}
                />
            </div>
        );
    }

    render() {
        const { handleSubmit, submitting, reset, pristine } = this.props;
        const { renderField } = this;

        return (
            <div className="d-flex">
                <form role="form" className="d-flex flex-grow-1 flex-column mt-3 ml-0 w-100 float-left"
                      onSubmit={handleSubmit}>
                    <Field
                        name="title"
                        type="text"
                        component={renderField}
                        label="Title"
                        InputProps={{
                            readOnly: false,
                        }}
                    />
                    {/*<Field*/}
                    {/*    id="image-field"*/}
                    {/*    name="photo"*/}
                    {/*    component={DropZoneField}*/}
                    {/*    type="file"*/}
                    {/*    crop={true}*/}
                    {/*    cropShape="rect"*/}
                    {/*    loadImage={() => photoService.getUserPhoto(props.initialValues.userId)}*/}
                    {/*/>*/}
                    <div className="align-self-end">
                        <Button type="submit" disabled={submitting} color="primary">Save</Button>
                        <Button type="button" color="secondary" disabled={pristine || submitting} onClick={reset}>
                            Reset
                        </Button>
                    </div>
                </form>
            </div>
        )
    }
}

CategoryGroupInfoWrapper = reduxForm({
    form: 'CategoryGroupInfoForm',
    enableReinitialize: true
})(CategoryGroupInfoWrapper);
export default CategoryGroupInfoWrapper;



