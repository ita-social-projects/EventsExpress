import React, { Component } from "react";
import { connect } from "react-redux";
import IconButton from "@material-ui/core/IconButton";
import UnitOfMeasuringItem from "../../components/unitOfMeasuring/unitOfMeasuring-item";
import UnitOfMeasuringEdit from "../../components/unitOfMeasuring/unitOfMeasuring-edit";
import { add_unitOfMeasuring, set_edited_unitOfMeasuring } from "../../actions/unitOfMeasuring/unitOfMeasuring-add-action";
import { delete_unitOfMeasuring } from "../../actions/unitOfMeasuring/unitOfMeasuring-delete-action";
import { confirmAlert } from 'react-confirm-alert';
import get_categoriesOfMeasuring from "../../actions/categoryOfMeasuring/categoryOfMeasuring-list-action";
import 'react-confirm-alert/src/react-confirm-alert.css';

class UnitOfMeasuringItemWrapper extends Component {

    save = values => {
        if (values.unitName === this.props.item.unitName &&
            values.shortName === this.props.item.shortName &&
            values.categoryId === this.props.item.category) {
            this.props.edit_cancel();
        } else {
           return this.props.save_unitOfMeasuring({ ...values, id: this.props.item.id });
        }
    };

    componentWillMount() {
        this.props.get_categoriesOfMeasuring();
    }

    componentWillUpdate = () => {
        const {isUnitOfMeasuringSuccess } = this.props.status;

        if (isUnitOfMeasuringSuccess) {
            this.props.edit_cancel();
        }
    }

    isDeleteConfirm = () => {
        const { unitName, shortName, id } = this.props.item;
        confirmAlert({
            title: 'Do you really want to remove this Unit Of Measuring?',
            message: <div>
                Unit name is {unitName}<br />
            Short name is {shortName}
            </div>,
            buttons: [
                {
                    label: 'Yes',
                    onClick: () => { this.props.delete_unitOfMeasuring(id); }
                },
                {
                    label: 'No',
                }
            ]
        });

    }
    render() {
        const { set_unitOfMeasuring_edited, edit_cancel } = this.props;

        return <tr>
            {(this.props.item.id === this.props.editedUnitOfMeasuring)
                ? <UnitOfMeasuringEdit
                    key={this.props.item.id + this.props.editedUnitOfMeasuring}
                    initialValues={this.props.item}
                    onSubmit={this.save}
                    cancel={edit_cancel}
                    all_categories={this.props.all_categories}
                />
                : <UnitOfMeasuringItem
                    item={this.props.item}
                    callback={set_unitOfMeasuring_edited}
                />
            }
            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center justify-content-center" width="15%">
                    <IconButton className="text-danger" size="small" onClick={this.isDeleteConfirm}>
                        <i className="fas fa-trash" />
                    </IconButton>
                </div>
            </td>
        </tr>
    }
}

const mapStateToProps = state => {
    return {
        all_categories: state.categoriesOfMeasuring,
        status: state.add_unitOfMeasuring,
        editedUnitOfMeasuring: state.unitsOfMeasuring.editedUnitOfMeasuring
    }

};

const mapDispatchToProps = (dispatch, props) => {
    return {
        get_categoriesOfMeasuring: () => dispatch(get_categoriesOfMeasuring()),
        delete_unitOfMeasuring: () => dispatch(delete_unitOfMeasuring(props.item.id)),
        save_unitOfMeasuring: (data) => dispatch(add_unitOfMeasuring(data)),
        set_unitOfMeasuring_edited: () => dispatch(set_edited_unitOfMeasuring(props.item.id)),
        edit_cancel: () => dispatch(set_edited_unitOfMeasuring(null))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UnitOfMeasuringItemWrapper);
