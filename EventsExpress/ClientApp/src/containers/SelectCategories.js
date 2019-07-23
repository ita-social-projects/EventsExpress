import { connect } from 'react-redux';
import React, { Component } from 'react';
import save from '../actions/SelectCategories';
import SelectCategories from '../components/SelectCategories/SelectCategories';
import get_categories from '../actions/category-list'

class SelectCategoriesWrapper extends Component {
    componentDidMount = () => this.props.get_categories();
    submit = values => {
        console.log(values);
        if (this.props.callback) {
            this.props.callback(values);
        }
    };
    render() {
        let { IsSelectCategoriesSeccess, IsSelectCategoriesError } = this.props
        console.log(this.props);

        return <SelectCategories items={this.props.allCategories.data} onSubmit={this.submit} />;
    }
}
const mapStateToProps = state => {
    return {
        allCategories: state.categories,

    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_categories: () => dispatch(get_categories())
    }
};
export default connect(
    mapStateToProps,
   mapDispatchToProps
)(SelectCategoriesWrapper);