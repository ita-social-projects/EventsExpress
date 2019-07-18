import { connect } from 'react-redux';
import React, { Component } from 'react';
import save from '../actions/SelectCategories';
import SelectCategories from '../components/SelectCategories/SelectCategories';

class SelectCategoriesWrapper extends Component {
    submit = values => {
        console.log(values);
        this.props.save(values.categories);
    };
    render() {
        let { IsSelectCategoriesSeccess, IsSelectCategoriesError } = this.props
        return <SelectCategories onSubmit={this.submit} />;
    }
}
const mapStateToProps = state => {
    return state.save;
};

const mapDispatchToProps = dispatch => {
    return {
        save: (categories) => dispatch(save(categories))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SelectCategoriesWrapper);