import React from "react";
import CategoryForm  from '../components/category/category-form';
import { connect } from "react-redux";
import add from "../actions/add-category";

class CategoryWrapper extends React.Component {
    submit = values => {
        console.log(values);
        this.props.add({ ...values });
    };
    render() {
        return <CategoryForm onSubmit={this.submit} />;
    }
}
const mapStateToProps = state => ({ add: state.add });

const mapDispatchToProps = dispatch => {
    return {
        add: (data) => dispatch(add(data))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CategoryWrapper);