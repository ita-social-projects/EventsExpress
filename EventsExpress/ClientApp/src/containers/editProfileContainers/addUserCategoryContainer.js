import React from "react";
import { connect } from "react-redux";
import SelectCategoriesWrapper from '../SelectCategories';
import add_UserCategory from '../../actions/EditProfile/addUserCategory';

class AddUserCategory extends React.Component {
    submit = values => {
        this.props.add(values);
    };

    render() {
        return <SelectCategoriesWrapper callback={this.submit} />;
    }
}

const mapDispatchToProps = dispatch => {
    return {
        add: (data) => dispatch(add_UserCategory(data))
    };
}

export default connect(
    null,
    mapDispatchToProps
)(AddUserCategory);
