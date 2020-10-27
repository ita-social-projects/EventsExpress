import React from "react";
import SelectCategoriesWrapper from '../SelectCategories';
import add_UserCategory from '../../actions/EditProfile/addUserCategory';
import { connect } from "react-redux";

class AddUserCategory extends React.Component {
    submit = values => {

        this.props.add(values);
    };

    render() {
        return <SelectCategoriesWrapper callback={this.submit} />;
    }
}

const mapStateToProps = () => ({ })

const mapDispatchToProps = dispatch => {
    return {
        add: (data) => dispatch(add_UserCategory(data))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(AddUserCategory);