import React from "react";
import CategoryItem from "./categoryItem";
import { connect } from "react-redux";
import deleteCat from "../actions/delete-category";

class categoryItemWrapper extends React.Component {
    submit = value => {
        console.log(this.props.id);
        this.props.deleteCat({ ...value });
    };
    render() {
        const { id, Title } = this.props;
        return <CategoryItem click={this.submit} id={id} Title={Title} />;
    }
}
const mapStateToProps = state => ({ add: state.add });

const mapDispatchToProps = dispatch => {
    return {
        deleteCat: (id) => dispatch(deleteCat(id))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(categoryItemWrapper);