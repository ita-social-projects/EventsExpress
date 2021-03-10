import React, { Component } from 'react';
import { connect } from 'react-redux';
import DraftList from '../components/Draft/Draft-list';
import Spinner from '../components/spinner';
import { get_drafts, reset_events } from '../actions/event-list-action';
import eventHelper from '../components/helpers/eventHelper';


class EventDraftListWrapper extends Component {
    constructor(props) {
        super(props);
        this.objCurrentQueryParams = Object.create(null);
    }

    componentDidMount() {

        const { page } = this.props.match.params.page;
        this.props.get_drafts(page);
    }

    componentDidUpdate(prevProps) {
        const objFilterParams = eventHelper.trimUndefinedKeys(this.props.events.filter);
        if (this.hasUpdateSearchParams(objFilterParams)) {
            this.objCurrentQueryParams = objFilterParams;
        }
    }

    hasUpdateSearchParams = objFilterParams => {
        return !eventHelper.compareObjects(objFilterParams, this.objCurrentQueryParams);
    }

    render() {
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data, isPending } = this.props.events;
        const { items } = this.props.events.data;
        const spinner = isPending ? <Spinner /> : null;
        const content = 
             <DraftList
                current_user={current_user}
                data_list={items}
                filter={this.props.events.filter}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                reset_events={this.props.reset_events}
                get_drafts={this.props.get_drafts}
                match={this.props.match}
            />
        return <>
            {
                 spinner || content
            }
        </>
    }
}

const mapStateToProps = (state) => {
    return {
        events: state.events,
        current_user: state.user
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_drafts: (page) => dispatch(get_drafts(page)),
        reset_events: () => dispatch(reset_events()),
    }
};

    export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EventDraftListWrapper);
