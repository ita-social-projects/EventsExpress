import React, { Component } from 'react';
import { createBrowserHistory } from 'history';
import SimpleModal from './../event/simple-modal';

const history = createBrowserHistory({ forceRefresh: true });

export default class ContactAdminDetails extends Component {

    handleClose = () => {
        history.push(`/contactAdmin/issues`);
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
                            {<SimpleModal
                                button={<button className="btn btn-primary btn-lg mr-5">Mark as resolved</button>}
                                action={this.props.onResolve}
                                data={'Are you sure?'}
                            />}
                            {<SimpleModal
                                button={<button className="btn btn-primary btn-lg mr-5">Move in progress</button>}
                                action={this.props.onInProgress}
                                data={'Are you sure?'}
                            />}
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}
