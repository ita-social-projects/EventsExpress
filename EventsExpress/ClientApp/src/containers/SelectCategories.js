import React, { Component } from 'react';
import { connect } from 'react-redux';
import SelectCategories from '../components/SelectCategories/SelectCategories';
import get_categories from '../actions/category-list'

class SelectCategoriesWrapper extends Component {
    componentDidMount = () => this.props.get_categories();

    submit = values => {
        if (this.props.callback) {
            this.props.callback(values);
        }
    };

    render() {
        return <SelectCategories
            items={this.props.allCategories.data}
            initialValues={this.props.selectedCategories}
            onSubmit={this.submit}
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
        get_categories: () => dispatch(get_categories())
    }
};
export default connect(mapStateToProps, mapDispatchToProps)(SelectCategoriesWrapper);