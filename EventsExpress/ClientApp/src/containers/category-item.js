import React from "react";
import CategoryItem from "../components/category/category-item";
import CategoryEdit from "../components/category/category-edit";
import { connect } from "react-redux";
import { delete_category, set_edited_category } from "../actions/delete-category";
import save from "../actions/add-category";
import Fab from '@material-ui/core/Fab';
import '../components/category/Category.css';


import IconButton from "@material-ui/core/IconButton";


class categoryItemWrapper extends React.Component {

    save = values => {
        this.props.save_category({ ...values, Id: this.props.item.id });
        this.props.edit_cansel();
    };


    render() {
        
        const { delete_category, set_category_edited, edit_cansel} = this.props;
        return <tr>
                {(this.props.item.id === this.props.editedCategory)
                    ? <CategoryEdit 
                        item={this.props.item} 
                        callback={this.save} 
                        cancel={edit_cansel} 
                    />
                    : <CategoryItem 
                        item={this.props.item} 
                        callback={set_category_edited} 
                    />
                    
                }
                <td>
                    <IconButton className="text-danger" size="small" onClick={delete_category}>
                        <i className="fas fa-trash"></i>
                    </IconButton>
                </td>
            </tr>
    };
}

const mapStateToProps = state => { 
    return{
        editedCategory: state.categories.editedCategory
    }
    
};

const mapDispatchToProps = (dispatch, props) => {

    return {
        delete_category: () => {console.log(props.item.id); dispatch(delete_category(props.item.id))},
        save_category: (data) => dispatch(save(data)),
        set_category_edited: () => dispatch(set_edited_category(props.item.id)),
        edit_cansel: () => dispatch(set_edited_category(null))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(categoryItemWrapper);