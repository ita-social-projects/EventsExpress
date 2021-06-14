import React, { Component } from 'react';
import CategoryAddWrapper from '../../containers/categories/category-add';
import CategoryListWrapper from '../../containers/categories/category-list';
import Spinner from '../spinner';
import get_categories from '../../actions/category/category-list-action';
import { connect } from 'react-redux';

class Categories extends Component {
    componentWillMount = () => this.props.get_categories();

    render() {
        const { data } = this.props.categories;

        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                    <CategoryAddWrapper
                        item={{ name: "", id: "00000000-0000-0000-0000-000000000000" }}
                    />
                    <Spinner showContent={data != undefined}>
                        <CategoryListWrapper data={data} />
                    </Spinner>
                </tbody>
            </table>
        </div>
    }
}

const mapStateToProps = (state) => ({
    categories: state.categories,
});


const mapDispatchToProps = (dispatch) => {
    return {
        get_categories: () => dispatch(get_categories())
    }
};


export default connect(mapStateToProps, mapDispatchToProps)(Categories)