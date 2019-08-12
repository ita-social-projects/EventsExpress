import React, { Component } from 'react';
import { connect } from 'react-redux';
import CommentList from '../components/comment/comment-list';
import Spinner from '../components/spinner';
import get_comments from '../actions/comment-list';
import BadRequest from '../components/Route guard/400'
import InternalServerError from '../components/Route guard/500'
import Unauthorized from '../components/Route guard/401'
import Forbidden from '../components/Route guard/403'



class CommentListWrapper extends Component {

    componentWillMount() {
        const { page } = this.props.match.params; 
        this.getComments(this.props.eventId, page);
   
    }
    getComments = (value, page) => this.props.get_comments(value,page);
    render() {

        const { data, isPending, isError } = this.props.comments;
        const spinner = isPending ? <Spinner /> : null;
      
       const errorMessage = isError.ErrorCode == '403' ? <Forbidden /> : isError.ErrorCode == '500' ? <InternalServerError /> : isError.ErrorCode == '401' ? <Unauthorized /> : isError.ErrorCode == '400' ? <BadRequest /> : null;

        const content = !isPending ? <CommentList evId={this.props.eventId} data_list={data.items} page={data.pageViewModel.pageNumber} totalPages={data.pageViewModel.totalPages} callback={this.getComments} /> : null;
        return <>
            {errorMessage}
            {spinner}
            {content}
        </>
    }
}

const mapStateToProps = state => ({
    comments: state.comments,
    eventId: state.event.data.id
});

const mapDispatchToProps = (dispatch) => {
    return {
        get_comments: (data, page) => dispatch(get_comments(data, page))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(CommentListWrapper);