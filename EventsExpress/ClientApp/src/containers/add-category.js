import React from "react";
import CategoryForm from '../components/category/category-form';
import { connect } from "react-redux";
import add from "../actions/add-category";

class CategoryWrapper extends React.Component {
    submit = values => {
        console.log(values);
        this.props.add({ ...values });
    };
    render() {
        return (
            <div>
                <CategoryForm categoryError={this.props.categoryError} onSubmit={this.submit} />
        </div>
        );
    }
}
const mapStateToProps = state => ({ categoryError: state.add_category.categoryError });

const mapDispatchToProps = dispatch => {
    return {
        add: (data) => dispatch(add(data))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CategoryWrapper);