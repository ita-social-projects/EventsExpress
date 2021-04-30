import React, { Component } from 'react';
import ContactUsListWrapper from '../../containers/contactUs/contactUs-list-container';
import ContactUsContainer from '../../containers/contactUs/contactUs-container';
import Spinner from '../spinner';
import getIssues from '../../actions/contactUs/contact-us-list-action';
import { connect } from 'react-redux';

class ContactUsTable extends Component {
    componentWillMount = () => this.props.getIssues();

    render() {
        const { isPending, data } = this.props.contactUs;
        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                    {!isPending ? <ContactUsListWrapper data={data} /> : null}
                </tbody>
            </table>
            {isPending ? <Spinner /> : null}
        </div>
    }
}

const mapStateToProps = (state) => ({ contactUs: state.contactUsList });

const mapDispatchToProps = (dispatch) => {
    return {
        getIssues: () => dispatch(getIssues())
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(ContactUsTable)
