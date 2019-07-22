import React from "react";
import CategoryItem from "../components/category/category-item";
import { connect } from "react-redux";
import deleteCat from "../actions/delete-category";
import Button from "@material-ui/core/Button";

import '../components/category/Category.css';

class categoryItemWrapper extends React.Component {
    submit = () => {
        let value = this.props.item.id;
        console.log('delete: ');
        console.log(value);
        this.props.deleteCat({ id: value });
    };
    render() {

        return(
            <div className="ItemCategory">
                < CategoryItem item = {this.props.item} />
                <div>
                    <Button
                        className="btn"
                        type="submit"
                        onClick={this.submit}
                        value="deleteCat"
                        color="primary">
                        Delete
                    </Button>
                </div>
            </div>
        )
    };
}
const mapStateToProps = state => ({ deleteCat: state.deleteCat });

const mapDispatchToProps = dispatch => {
    return {
        deleteCat: (id) => dispatch(deleteCat(id))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(categoryItemWrapper);