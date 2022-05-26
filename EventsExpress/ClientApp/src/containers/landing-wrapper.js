import React, { Component } from "react";
import { connect } from "react-redux";
import { get_upcoming_events } from "../actions/event/event-list-action";
import add_event from "../actions/event/event-add-action";
import { toggleLoginModalState } from "../actions/login-modal";
import Landing from "../components/landing";

class LandingWrapper extends Component {
  render() {
    const { user, events, add_event, openLoginModal, get_upcoming_events } =
      this.props;
    return (
      <Landing
        user={user}
        events={events}
        onCreateEvent={add_event}
        onOpenLoginModal={openLoginModal}
        getUpcomingEvents={get_upcoming_events}
      />
    );
  }
}

const mapStateToProps = (state) => ({
  user: state.user,
  hub: state.hubConnections.chatHub,
  events: state.events,
});

const mapDispatchToProps = (dispatch) => {
  return {
    add_event: () => dispatch(add_event()),
    openLoginModal: () => dispatch(toggleLoginModalState(true)),
    get_upcoming_events: () => dispatch(get_upcoming_events()),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(LandingWrapper);
