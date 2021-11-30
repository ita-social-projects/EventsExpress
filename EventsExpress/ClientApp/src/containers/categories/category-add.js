import React from "react";
import { connect } from "react-redux";
import add_category, {
  setCategoryEdited,
} from "../../actions/category/category-add-action";
import CategoryEdit from "../../components/category/category-edit";

class CategoryAddWrapper extends React.Component {
  submit = (values) => {
    values.categoryGroup = JSON.parse(values.categoryGroup);
    return this.props.add({ ...values });
  };

  render() {
    return this.props.item.id !== this.props.editedCategory ? (
      <tr>
        <td className="align-middle align-items-stretch" width="20%">
          <div className="d-flex align-items-center justify-content-left">
            <button
              className="btn btn-outline-primary ml-0"
              onClick={this.props.set_category_edited}
            >
              Add category
            </button>
          </div>
        </td>
        <td width="80%"></td>
      </tr>
    ) : (
      <tr>
        <CategoryEdit
          item={this.props.item}
          groups={this.props.groups}
          onSubmit={this.submit}
          cancel={this.props.edit_cancel}
        />
        <td></td>
      </tr>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    editedCategory: state.categories.editedCategory,
    counter: state.requestCount.counter,
  };
};

const mapDispatchToProps = (dispatch, props) => {
  return {
    add: (data) => dispatch(add_category(data)),
    set_category_edited: () => dispatch(setCategoryEdited(props.item.id)),
    edit_cancel: () => {
      dispatch(setCategoryEdited(null));
    },
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoryAddWrapper);
