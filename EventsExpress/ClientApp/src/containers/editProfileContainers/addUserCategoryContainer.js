import React from "react";
import CategoryForm from '../components/category/category-form';
import { connect } from "react-redux";
import add from "../actions/add-category";

class AddUserCategory extends React.Component {
    submit = values => {
        console.log(values);
        add(values);
    };

    render() {
        return <SelectCategoriesWrapper callback={this.submit} />;
    }
}

const mapDispatchToProps = dispatch => {
    return {
        add: (data) => dispatch(add_UserCategory(data))
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(AddUserCategory);