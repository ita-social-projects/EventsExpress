import React, { Component } from 'react';
import { connect } from 'react-redux';
import CategoryList from '../components/category/category-list';
import Spinner from '../components/spinner';
import get_categories from '../actions/category-list';


class CategoryListWrapper extends Component {

    componentWillMount = () => this.props.get_categories();

    render() {
        const { data, isPending } = this.props;

        return (!isPending)
            ? <CategoryList data_list={data} />
            : <Spinner />
    }
}

const mapStateToProps = (state) => (state.categories);

const mapDispatchToProps = (dispatch) => {
    return {
        get_categories: () => dispatch(get_categories())
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoryListWrapper);