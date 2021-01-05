import React, { Component } from 'react';
import { connect } from 'react-redux';
import SelectCategories from '../../components/SelectCategories/SelectCategories';
import setUserCategory from '../../actions/EditProfile/addUserCategory';
import get_categories from '../../actions/category/category-list';

class SelectCategoriesWrapper extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount = () => this.props.get_categories();

    handleSubmit(event) {
        this.props.setUserCategory({
            categories: event.categories
        });
    }

    render() {
        return <SelectCategories
            items={this.props.allCategories.data}
            initialValues={this.props.user.categories}
            onSubmit={this.handleSubmit}
        />;
    }
}
const mapStateToProps = state => {
    return {
        allCategories: state.categories,
        user: state.user,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        setUserCategory: (data) => dispatch(setUserCategory(data)),
        get_categories: () => dispatch(get_categories())
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SelectCategoriesWrapper)
