import React, { Component } from "react";
import { connect } from "react-redux";
import IconButton from "@material-ui/core/IconButton";
import CategoryItem from "../../components/category/category-item";
import CategoryEdit from "../../components/category/category-edit";
import add_category, {
    setCategoryEdited,
} from "../../actions/category/category-add-action";
import { delete_category } from "../../actions/category/category-delete-action";
import CategoryGroupItem from "../../components/categoryGroup/category-group-item";

class CategoryGroupItemWrapper extends Component {

    render() {
        const { set_category_edited } = this.props;

        return (
            <tr>
                    <CategoryGroupItem item={this.props.item} callback={set_category_edited} />
            </tr>
        );
    }
}

export default CategoryGroupItemWrapper;
