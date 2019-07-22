import { connect } from 'react-redux';
import React, { Component } from 'react';
import save from '../actions/SelectCategories';
import SelectCategories from '../components/SelectCategories/SelectCategories';


class SelectCategoriesWrapper extends Component {
    submit = values => {
        console.log(values);
       
      
    };
    render() {
        let { IsSelectCategoriesSeccess, IsSelectCategoriesError } = this.props
        console.log(this.props);

        return <SelectCategories items={this.props.allCategories.data} onSubmit={this.submit}  />;
    }
}
const mapStateToProps = state => {
    return {
        allCategories: state.categories,

    };
};


export default connect(
    mapStateToProps
  
)(SelectCategoriesWrapper);