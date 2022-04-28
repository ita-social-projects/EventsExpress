import React, { Component } from "react";
import SpinnerWrapper from "../../containers/spinner";
import get_category_groups from "../../actions/categoryGroup/category-group-list-action";
import { connect } from "react-redux";
import CategoryAddWrapper from "../../containers/categories/category-add";
import CategoryGroupListWrapper from "../../containers/categoriesGroups/categories-groups-list";

class CategoriesGroups extends Component {
    constructor(props) {
        super(props);
        props.get_category_groups();
    }

    render() {
        const groups = this.props.categoryGroups.data;
        return (
            <div>
                <table className="table w-100 m-auto">
                    <tbody>
                    {/*<CategoryAddWrapper*/}
                    {/*    item={{*/}
                    {/*        name: "",*/}
                    {/*        id: "00000000-0000-0000-0000-000000000000",*/}
                    {/*        categoryGroupId: {*/}
                    {/*            id: "00000000-0000-0000-0000-000000000000",*/}
                    {/*            title: "",*/}
                    {/*        },*/}
                    {/*    }}*/}
                    {/*    groups={groups}*/}
                    {/*/> */}
                    <SpinnerWrapper
                        showContent={groups != undefined}>
                        <CategoryGroupListWrapper data={groups} />
                    </SpinnerWrapper>

                    </tbody>
                </table>
            </div>
        );
    }
}

const mapStateToProps = (state) => ({
    categoryGroups: state.categoryGroups,
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_category_groups: () => dispatch(get_category_groups()),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoriesGroups);
