import React from "react";
import SelectCategoriesWrapper from '../SelectCategories';
import add_UserCategory from '../../actions/EditProfile/addUserCategory';
import { connect } from "react-redux";
import ChangeAvatar from '../../components/profile/editProfile/change-avatar';
import change_avatar from '../../actions/EditProfile/change-avatar';
class ChangeAvatarWrapper extends React.Component {
    submit = values => {

        this.props.change_avatar(values);
    };

    render() {
        return <ChangeAvatar onSubmit={this.submit} />;
    }
}

const mapStateToProps = () => { }

const mapDispatchToProps = dispatch => {
    return {
        change_avatar: (data) => dispatch(change_avatar(data))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ChangeAvatarWrapper);