import React, {Component} from 'react';
import TrackList from './track-list';
import TracksFilter from './tracks-filter';
import Spinner from '../spinner';
import getAllTracks, {getEntityNames} from '../../actions/tracks/track-list-action';
import {getFormValues, reset} from 'redux-form';
import {connect} from 'react-redux';

class Tracks extends Component {

    componentDidMount = () => {
        this.props.getAllTracks(this.props.tracks.filter);
        this.props.getEntityNames();
    }

    handleSubmit = async (filters) => {
        await this.props.getAllTracks({
            entityName: !!filters.entityNames && filters.entityNames.map(x => x.entityName),
            changesType: filters.changesType,
            dateFrom: filters.dateFrom,
            dateTo: filters.dateTo,
            page: 1
        })
    }

    handlePageChange = async (page) => {
        const {entityNames, changesType, dateFrom, dateTo} = this.props.form_values;
        await this.props.getAllTracks({
            entityName: entityNames !== null || true
                ? entityNames.map(x => x.entityName) : null,
            changesType: changesType,
            dateFrom: dateFrom,
            dateTo: dateTo,
            page: page
        })
    }

    render() {
        const {isPending, data, entityNames} = this.props.tracks;
        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                <div className="d-flex">
                    {!isPending &&
                    data?.items &&
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
                            form_values={this.props.form_values}
                        />
                    </div>
                </div>
                </tbody>
            </table>
            {isPending ? <Spinner/> : null}
        </div>
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
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(Tracks);
