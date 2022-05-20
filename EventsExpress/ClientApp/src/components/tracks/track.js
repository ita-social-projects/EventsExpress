import React, { Component } from 'react';
import TrackList from './track-list';
import TracksFilter from './tracks-filter';
import SpinnerWrapper from '../../containers/spinner';
import getAllTracks, { getEntityNames } from '../../actions/tracks/track-list-action';
import { getFormValues, reset } from 'redux-form';
import { connect } from 'react-redux';

class Tracks extends Component {

    componentDidMount = () => {
        this.props.getAllTracks({
            page: 1,
            firstName: "",
            changesType: [],
            dateFrom: null,
            dateTo: null,
            entityName: []
        });
        this.props.getEntityNames();
    }

    handleSubmit = async (filters) => {
        let currentFilters = filters || {};
        await this.handleFunc(currentFilters, 1);
    }

    handlePageChange = async (page) => {
        let currentFilters = this.props.form_values || {};
        await this.handleFunc(currentFilters, page);
    }

    onReset = async () => {
        this.props.reset_filters();
        await this.handleFunc({}, 1);
    }

    handleFunc = async (data, pages) => {
        const { entityNames = [], changesType, dateFrom, dateTo } = data;
        await this.props.getAllTracks({
            entityName: !!entityNames ? entityNames.map(x => x.entityName) : null,
            changesType: changesType,
            dateFrom: dateFrom,
            dateTo: dateTo,
            page: pages
        })
    }

    render() {
        const { data, entityNames } = this.props.tracks;
        return <SpinnerWrapper showContent={data != undefined}>
            <table className="table w-100 m-auto">
                <tbody>
                    <div className="d-flex">
                        {data?.items &&
                            <div className="w-75">
                                <TrackList
                                    data_list={data}
                                    handlePageChange={this.handlePageChange}
                                />
                            </div>
                        }
                        <div className="w-25">
                            <TracksFilter
                                entityNames={entityNames}
                                onSubmit={this.handleSubmit}
                                onReset={this.onReset}
                                form_values={this.props.form_values}
                            />
                        </div>
                    </div>
                </tbody>
            </table>

        </SpinnerWrapper>
    }
}

const mapStateToProps = (state) => ({
    tracks: state.tracks,
    form_values: getFormValues('tracks-filter-form')(state)
});

const mapDispatchToProps = (dispatch) => {
    return {
        getAllTracks: (filter) => dispatch(getAllTracks(filter)),
        getEntityNames: () => dispatch(getEntityNames()),
        reset_filters: () => dispatch(reset('tracks-filter-form')),
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(Tracks);
