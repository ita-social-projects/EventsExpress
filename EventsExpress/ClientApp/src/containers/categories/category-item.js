import React, { Component } from "react";
import { connect } from "react-redux";
import IconButton from "@material-ui/core/IconButton";
import CategoryItem from "../../components/category/category-item";
import CategoryEdit from "../../components/category/category-edit";
import add_category, { set_edited_category } from "../../actions/category/category-add-action";
import { delete_category } from "../../actions/category/category-delete-action";


class CategoryItemWrapper extends Component {

    save = values => {
        if (values.name === this.props.item.name) {
            this.props.edit_cansel();
        } else {
            return this.props.save_category({ ...values, id: this.props.item.id });
        }
    };

    componentWillUpdate = () => {
        const { isCategorySuccess } = this.props.status;

        if (isCategorySuccess) {
            this.props.edit_cansel();
        }
    }
    render() {
        const { delete_category, set_category_edited, edit_cansel } = this.props;

        return <tr>
            {(this.props.item.id === this.props.editedCategory)
                ? <CategoryEdit
                    key={this.props.item.id + this.props.editedCategory}
                    initialValues={this.props.item}
                    onSubmit={this.save}
                    cancel={edit_cansel}
                />
                : <CategoryItem
                    item={this.props.item}
                    callback={set_category_edited}
                />
            }
            <td className="align-middle align-items-stretch">
                <div className="d-flex align-items-center justify-content-center">
                    <IconButton className="text-danger" size="small" onClick={delete_category}>
                        <i className="fas fa-trash" />
                    </IconButton>
                </div>
            </td>
        </tr>
    };
}

const mapStateToProps = state => {
    return {
        status: state.add_category,
        editedCategory: state.categories.editedCategory
    }

};

const mapDispatchToProps = (dispatch, props) => {
    return {
        delete_category: () => dispatch(delete_category(props.item.id)),
        save_category: (data) => dispatch(add_category(data)),
        set_category_edited: () => dispatch(set_edited_category(props.item.id)),
        edit_cansel: () => dispatch(set_edited_category(null))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CategoryItemWrapper);
