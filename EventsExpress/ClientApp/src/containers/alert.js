import React from 'react';
import { connect } from "react-redux";
import MySnackbar from '../components/helpers/Alert';
import { setAlertOpen } from '../actions/alert-action'

class AlertContainer extends React.Component {
  render() {
    return (
      <MySnackbar
        open={this.props.open}
        onClose={this.props.close}
        alert={this.props.alert} />
    );
  }
}

const mapStateToProps = state => {
  return {
    alert: state.alert
  }
};

const mapDispatchToProps = dispatch => {
  return {
    close: () => dispatch(setAlertOpen(false)),
    open: () => dispatch(setAlertOpen(true))
  }
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(AlertContainer)
