import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';
import OccurenceEventList from '../components/occurenceEvent/occurenceEvent-list';
import Spinner from '../components/spinner';
import { getOccurenceEvents } from '../actions/occurenceEvent-list';
import BadRequest from '../components/Route guard/400';
import Unauthorized from '../components/Route guard/401';
import Forbidden from '../components/Route guard/403';

class OccurenceEventListWrapper extends Component {
    constructor(props) {
        super(props);
        this.props.getOccurenceEvents();
    }

    render() {
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data, isPending, isError } = this.props.occurenceEvents;
        const { items } = this.props.occurenceEvents.data;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <Redirect
                    from="*"
                    to={{
                        pathname: "/home/occurenceEvents",
                    }}
                />
                : isError.ErrorCode == '401'
                    ? <Unauthorized />
                    : isError.ErrorCode == '400'
                        ? <BadRequest />
                        : null;
        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage
            ? <OccurenceEventList
                current_user={current_user}
                data_list={data.items}
            />
            : null;

        return <>
            {!errorMessage
                ? spinner || content
                : errorMessage
            }
        </>
    }
}

const mapStateToProps = (state) => {
    return {
        occurenceEvents: state.occurenceEvents,
        current_user: state.user
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        getOccurenceEvents: () => dispatch(getOccurenceEvents()),
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(OccurenceEventListWrapper);
