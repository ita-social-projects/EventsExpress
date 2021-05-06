import React, { Component } from 'react';
import ContactUsListWrapper from '../../containers/contactUs/contactUs-list-container';
import Spinner from '../spinner';
import getIssues from '../../actions/contactUs/contact-us-list-action';
import { connect } from 'react-redux';

class ContactUsTable extends Component {
    componentWillMount = () => {
        const { page } = this.props.match.params;
        this.props.getIssues(page);
    }

    render() {
        const { isPending, data } = this.props.contactUs;
        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                    {!isPending
                        ? <ContactUsListWrapper
                            data={data.items}
                            page={data.pageViewModel.pageNumber}
                            totalPages={data.pageViewModel.totalPages}
                            callback={this.props.getIssues}/>
                        : null}
                </tbody>
            </table>
            {isPending ? <Spinner /> : null}
        </div>
    }
}

const mapStateToProps = (state) => ({ contactUs: state.contactUsList });

const mapDispatchToProps = (dispatch) => {
    return {
        getIssues: (page) => dispatch(getIssues(page))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(ContactUsTable)
