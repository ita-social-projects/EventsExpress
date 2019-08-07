import React, { Component } from "react";
import { connect } from "react-redux";
import IconButton from "@material-ui/core/IconButton";
import CategoryItem from "../components/category/category-item";
import CategoryEdit from "../components/category/category-edit";
import { delete_category, set_edited_category } from "../actions/delete-category";
import { add_category } from "../actions/add-category";

import '../components/category/Category.css';



class categoryItemWrapper extends Component {

    save = values => {
        this.props.save_category({ ...values, Id: this.props.item.id });
        //this.props.edit_cansel();
    };

    componentWillUpdate = () => {
        
        const {categoryError, isCategorySuccess } = this.props.status;
        
        if (!categoryError && isCategorySuccess){
            this.props.edit_cansel();
        }
    }
    render() {
        const { delete_category, set_category_edited, edit_cansel} = this.props;
        
        return <tr>
                {(this.props.item.id === this.props.editedCategory)
                    ? <CategoryEdit 
                        item={this.props.item} 
                        callback={this.save} 
                        cancel={edit_cansel} 
                        message={this.props.status.categoryError}
                    />
                    : <CategoryItem 
                        item={this.props.item} 
                        callback={set_category_edited} 
                    />
                }
                <td className="align-middle align-items-stretch">
                    <div className="d-flex align-items-center justify-content-center">
                        <IconButton className="text-danger" size="small" onClick={delete_category}>
                            <i className="fas fa-trash"></i>
                        </IconButton>
                    </div>
                </td>
                
            </tr>
    };
}

const mapStateToProps = state => { 
    return{        
        status: state.add_category,
        editedCategory: state.categories.editedCategory
    }
    
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        delete_category: () => {console.log(props.item.id); dispatch(delete_category(props.item.id))},
        save_category: (data) => dispatch(add_category(data)),
        set_category_edited: () => dispatch(set_edited_category(props.item.id)),
        edit_cansel: () => dispatch(set_edited_category(null))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(categoryItemWrapper);