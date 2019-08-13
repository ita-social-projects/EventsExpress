import React from "react";
import CommentItem from "../components/comment/comment-item";
import { connect } from "react-redux";
import deleteComm from "../actions/delete-comment";
import Fab from '@material-ui/core/Fab';
import '../components/comment/Comment.css';




class commentItemWrapper extends React.Component {
    constructor(props) {
        super(props);
    }
    submit = () => {
        let value = this.props.item;
        this.props.deleteComm({ id: value.id, eventId: this.props.eventId });
    };

    render() {
        const { id } = this.props.item;
        return (
            <div className="ItemComment">
                {!(this.props.item.userId === this.props.userId)
                    ? <CommentItem item={this.props.item} user={this.props.userId} />
                    : <div className="comment-container">
                        <div className="mybutton">
                            <Fab
                                size="small"
                                onClick={this.submit}
                                aria-label="Delete">
                                <i className="fa fa-trash"></i>
                            </Fab>
                        </div>
                        <CommentItem item={this.props.item} user={this.props.userId} />
                    </div>
                }
            </div>
        )
    }
};
const mapStateToProps = state => ({
    userId: state.user.id,
    eventId: state.event.data.id
});

const mapDispatchToProps = dispatch => {
    return {
        deleteComm: (value) => dispatch(deleteComm(value))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(commentItemWrapper);