import React, { Component } from 'react';
import CategoryAddWrapper from '../../containers/categories/category-add';
import CategoryListWrapper from '../../containers/categories/category-list';
import SpinnerWrapper from '../../containers/spinner';
import get_categories from '../../actions/category/category-list-action';
import get_category_groups from '../../actions/categoryGroup/category-group-list-action';
import { connect } from 'react-redux';

class Categories extends Component {
    constructor(props) {
        super(props);
        props.get_category_groups();
        props.get_categories();
    }

    combineData = (categories, groups) => {
        return categories.map(item => {
            return {
                id: item.id,
                name: item.name,
                group: groups.find(g => g.id === item.categoryGroupId),
                countOfUser: item.countOfUser,
                countOfEvents: item.countOfEvents,
            }
        });
    };

    render() {
        const categories = this.props.categories.data;
        const groups = this.props.categoryGroups.data;

        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                    <CategoryAddWrapper
                        item={
                            {
                                name: "",
                                id: "00000000-0000-0000-0000-000000000000",
                                categoryGroupId: "00000000-0000-0000-0000-000000000000",
                            }
                        }
                        groups={groups}
                    />
                    <SpinnerWrapper showContent={categories != undefined && groups != undefined}>
                        <CategoryListWrapper data={this.combineData(categories, groups)} />
                    </SpinnerWrapper>
                </tbody>
            </table>
        </div>
    }
}

const mapStateToProps = (state) => ({
    categories: state.categories,
    categoryGroups: state.categoryGroups,
});


const mapDispatchToProps = (dispatch) => {
    return {
        get_categories: () => dispatch(get_categories()),
        get_category_groups: () => dispatch(get_category_groups()),
    }
};


export default connect(mapStateToProps, mapDispatchToProps)(Categories)