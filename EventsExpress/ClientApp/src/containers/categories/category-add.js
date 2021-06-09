import React from "react";
import { connect } from "react-redux";
import { reset } from 'redux-form';
import add_category,
{
    setCategoryPending,
    setCategorySuccess,
    set_edited_category
} from '../../actions/category/category-add-action';
import CategoryEdit from "../../components/category/category-edit";


class CategoryAddWrapper extends React.Component {

    submit = values => {
        return this.props.add({ ...values });
    }

    componentWillUpdate = () => {
        const { isCategorySuccess } = this.props.status;

        if (isCategorySuccess) {
            this.props.reset();
            this.props.edit_cancel();
        }
    }

    render() {
        return (this.props.item.id !== this.props.editedCategory)
            ? <tr>
                <td className="align-middle align-items-stretch" width="20%">
                    <div className="d-flex align-items-center justify-content-left">
                        <button className="btn btn-outline-primary ml-0" onClick={this.props.set_category_edited}>
                            Add category
                        </button>
                    </div>
                </td>
                <td width="55%"></td>
            </tr>
            : <tr>
                <CategoryEdit
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
        status: state.add_category,
        editedCategory: state.categories.editedCategory
    }
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        add: (data) => dispatch(add_category(data)),
        set_category_edited: () => dispatch(set_edited_category(props.item.id)),
        edit_cancel: () => {
            dispatch(set_edited_category(null));
        },
        reset: () => {
            dispatch(reset('add-form'));
            dispatch(setCategoryPending(false));
            dispatch(setCategorySuccess(false));
        }
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoryAddWrapper);