import React, {Component} from "react";
import CategoryGroupList from "../../components/categoryGroup/category-group-list";

export default class CategoryGroupListWrapper extends Component {

    render() {
        const { data } = this.props;

        return <CategoryGroupList data_list={data} />
    }
}