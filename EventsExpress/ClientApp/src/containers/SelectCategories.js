import React, { Component } from 'react';
import { connect } from 'react-redux';
import SelectCategories from '../components/SelectCategories/SelectCategories';
import add_UserCategory from '../actions/EditProfile/addUserCategory';
import get_categories from '../actions/category-list';

class SelectCategoriesWrapper extends Component {
    componentDidMount = () => this.props.get_categories();

    render() {
        return <SelectCategories
            items={this.props.allCategories.data}
            initialValues={this.props.selectedCategories}
            onSubmit={this.props.add}
        />;
    }
}
const mapStateToProps = state => {
    return {
        allCategories: state.categories,
        selectedCategories: state.user.categories,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        add: (data) => dispatch(add_UserCategory(data)),
        get_categories: () => dispatch(get_categories())
    }
};
export default connect(mapStateToProps, mapDispatchToProps)(SelectCategoriesWrapper);