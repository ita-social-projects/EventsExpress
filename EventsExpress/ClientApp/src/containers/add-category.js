import React from "react";
import CategoryForm from '../components/category/category-form';
import { connect } from "react-redux";
import { add_category } from "../actions/add-category";
import { reset } from 'redux-form';
import { setCategoryError, setCategoryPending, setCategorySuccess } from '../actions/add-category';
import { delete_category, set_edited_category } from "../actions/delete-category";
import IconButton from "@material-ui/core/IconButton";
import CategoryEdit from "../components/category/category-edit";


class CategoryWrapper extends React.Component {
    constructor(props) {
        super(props);
    }

    submit = values => {
        this.props.add({ ...values });
        
        const {categoryError, isCategorySuccess } = this.props.status;

    };

    componentWillUpdate = () => {
        
        const {categoryError, isCategorySuccess } = this.props.status;
        
        if (!categoryError && isCategorySuccess){
            this.props.reset();
        }
    }

    render() {
        return (this.props.item.id !== this.props.editedCategory) 
            ? <tr>
                <td width="75%"></td>
                
                
                <td className="align-middle align-items-stretch" width="15%">
                    <div className="d-flex align-items-center justify-content-center">
                        <IconButton className="text-info" onClick={this.props.set_category_edited}>
                            <i className="fas fa-plus-circle" ></i>
                        </IconButton> 
                    </div>
                </td>
                <td  ></td>

            </tr>
            : <tr>
                <CategoryEdit 
                    item={this.props.item}
                    callback={this.submit} 
                    cancel={this.props.edit_cansel}
                    message={this.props.status.categoryError}
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
        edit_cansel: () => dispatch(set_edited_category(null)),
        reset: () => {
            dispatch(reset('add-form'));
            dispatch(setCategoryPending(false));
            dispatch(setCategorySuccess(false));
            dispatch(setCategoryError(null));
            //set_edited_category(null);
        }
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoryWrapper);