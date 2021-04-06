import React from "react";
import { connect } from "react-redux";
import { reset } from 'redux-form';
import IconButton from "@material-ui/core/IconButton";
import {
    add_unitOfMeasuring,
    setUnitOfMeasuringPending,
    setUnitOfMeasuringSuccess,
    set_edited_unitOfMeasuring
} from "../../actions/unitOfMeasuring/unitOfMeasuring-add-action";
import UnitOfMeasuringEdit from "../../components/unitOfMeasuring/unitOfMeasuring-edit";

const pStyle = {
    margin: "0px"
};

class UnitOfMeasuringAddWrapper extends React.Component {
    submit = values => {
        return this.props.add({ ...values });
    };

    componentWillUpdate = () => {
        const {isUnitOfMeasuringSuccess } = this.props.status;

        if (isUnitOfMeasuringSuccess) {
            this.props.reset();
            this.props.edit_cancel();
        }
    }

    render() {
        return (
            this.props.item.id !== this.props.editedUnitOfMeasuring)
            ? <tr>
                <td className="align-middle align-items-stretch" width="20%">

                    <div className="d-flex align-items-center justify-content-left">
                        <p style={pStyle}>Add unit</p>
                        <IconButton
                            className="text-info"
                            onClick={this.props.set_unitOfMeasuring_edited}>
                            <i className="fas fa-plus-circle"></i>
                        </IconButton>
                    </div>
                </td>
            </tr>
            : <tr>
                <UnitOfMeasuringEdit
                    item={this.props.item}
                    onSubmit={this.submit}
                    cancel={this.props.edit_cancel}
                />
                <td></td>
            </tr>
    }
}


const mapStateToProps = state => {
    return {
        status: state.add_unitOfMeasuring,
        editedUnitOfMeasuring: state.unitsOfMeasuring.editedUnitOfMeasuring
    }
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        add: (data) => dispatch(add_unitOfMeasuring(data)),
        set_unitOfMeasuring_edited: () => dispatch(set_edited_unitOfMeasuring(props.item.id)),
        edit_cancel: () => {
            dispatch(set_edited_unitOfMeasuring(null));
        },
        reset: () => {
            dispatch(reset('add-form'));
            dispatch(setUnitOfMeasuringPending(false));
            dispatch(setUnitOfMeasuringSuccess(false));
        }
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(UnitOfMeasuringAddWrapper);