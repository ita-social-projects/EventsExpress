import React, { Component } from 'react';
import { createBrowserHistory } from 'history';
import SimpleModalWithDetails from '../helpers/simple-modal-with-details';

const history = createBrowserHistory({ forceRefresh: true });

export default class ContactAdminDetails extends Component {

    handleClose = () => {
        history.push(`/contactAdmin/issues?page=1`);
    }

    render() {
        const { items } = this.props;

        return (
            <div>
                <div>
                    <h1 className="text-center my-5">{'Issue description'}</h1>
                    {items.description}
                    <div className="text-center">
                        <div className="btn-group mt-5">
                            <button
                                type="button"
                                className="btn btn-secondary btn-lg mr-5"
                                onClick={this.handleClose}>
                                Close
                                </button>
                            {<SimpleModalWithDetails
                                button={<button className="btn btn-primary btn-lg mr-5">Mark as resolved</button>}
                                submitCallback={this.props.onResolve}
                                data="Enter resolution details"
                            />}
                            {<SimpleModalWithDetails
                                button={<button className="btn btn-primary btn-lg mr-5">Move in progress</button>}
                                submitCallback={this.props.onInProgress}
                                data="Enter resolution details"
                            />}
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}
