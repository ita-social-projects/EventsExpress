import React from "react";
import { connect } from "react-redux";
import { reset } from 'redux-form';
import IconButton from "@material-ui/core/IconButton";
import add_category,
{
    set_edited_category
} from '../../actions/category/category-add-action';

import { getRequestInc, getRequestDec } from '../../actions/request-count-action';
import CategoryEdit from "../../components/category/category-edit";


class CategoryAddWrapper extends React.Component {

    submit = values => {
        return this.props.add({ ...values });
    }

    render() {
        return (this.props.item.id !== this.props.editedCategory)
            ? <tr>
                <td className="align-middle align-items-stretch" width="20%">
                    <div className="d-flex align-items-center justify-content-center">
                        <IconButton
                            className="text-info"
                            onClick={this.props.set_category_edited}
                        >
                            <i className="fas fa-plus-circle"></i>
                        </IconButton>
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
        editedCategory: state.categories.editedCategory,
        counter: state.requestCount.counter
    }
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        add: (data) => dispatch(add_category(data)),
        set_category_edited: () => dispatch(set_edited_category(props.item.id)),
        edit_cancel: () => {
            dispatch(set_edited_category(null));
        },
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoryAddWrapper);