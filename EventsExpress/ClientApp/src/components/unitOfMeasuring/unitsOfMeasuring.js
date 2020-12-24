import React, { Component } from 'react';
//import CategoryAddWrapper from '../../containers/categories/category-add';
//import CategoryListWrapper from '../../containers/categories/category-list';
import Spinner from '../spinner';

//import get_categories from '../../actions/category/category-list';

import { connect } from 'react-redux';
import { getAllUnitsOfMeasuring } from '../../services/unitOfMeasuringService';

export default class UnitsOfMeasuring extends Component {


    //componentWillMount = () => this.props.get_categories();

    render() {
        //const { isPending, data } = this.props.categories;
        return (
            <div>
                <p>
                    {getAllUnitsOfMeasuring()}
                    OK
                </p>
            </div>
        )

    }
}

//const mapStateToProps = (state) => ({ categories: state.categories });

//