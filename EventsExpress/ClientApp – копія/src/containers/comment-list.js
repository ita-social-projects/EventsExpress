import React, { Component } from 'react';
import { connect } from 'react-redux';
import CommentList from '../components/comment/comment-list';
import Spinner from '../components/spinner';
import get_comments from '../actions/comment-list';



class CommentListWrapper extends Component {

    componentWillMount() {
        const { page } = this.props.match.params; 
        this.getComments(this.props.eventId, page);
   
    }
    getComments = (value, page) => this.props.get_comments(value,page);
    
    render() {

        const { data, isPending } = this.props.comments;

        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending 
            ? <CommentList 
                evId={this.props.eventId} 
                data_list={data.items} 
                page={data.pageViewModel.pageNumber} 
                totalPages={data.pageViewModel.totalPages} 
                callback={this.getComments} 
            /> 
            : null;

    return <>
            {spinner}
            {content}
        </>
    }
}

const mapStateToProps = state => ({
    comments: state.comments,
    eventId: state.event.data.id
});

const mapDispatchToProps = dispatch => ({
    get_comments: (data, page) => dispatch(get_comments(data, page))
});

export default connect(mapStateToProps, mapDispatchToProps)(CommentListWrapper);