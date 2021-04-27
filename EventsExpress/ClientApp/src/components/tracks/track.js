import React, {Component} from 'react';
import TrackList from './track-list';
import TracksFilter from './tracks-filter';
import Spinner from '../spinner';
import getAllTracks, {getEntityNames} from '../../actions/tracks/track-list-action';
import { getFormValues, reset } from 'redux-form';
import {connect} from 'react-redux';

class Tracks extends Component {

    componentDidMount = () => {
        this.props.getAllTracks(this.props.tracks.filter);
        this.props.getEntityNames();
    }

    handleSubmit = async (filters) => {
        await this.props.getAllTracks({
            // entityName: filters.entityNames != null ? filters.entityNames.map(x => x.entityName) : null,
            entityName: !!filters.entityNames && filters.entityNames.map(x => x.entityName),
            changesType: filters.changesType,
            dateFrom: filters.dateFrom,
            dateTo: filters.dateTo,
            page: 1
        })
    }
    
    handlePageChange = async (page) => {
        await this.props.getAllTracks({
            entityName: !!this.props.form_values.entityNames && this.props.form_values.entityNames.map(x => x.entityName),
            changesType: this.props.form_values.changesType,
            dateFrom: this.props.form_values.dateFrom,
            dateTo: this.props.form_values.dateTo,
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
