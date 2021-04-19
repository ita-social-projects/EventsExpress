import React, {Component} from 'react';
import TrackList from './track-list';
import TracksFilter from './tracks-filter';
import Spinner from '../spinner';
import getAllTracks, {getEntityNames} from '../../actions/tracks/track-list-action';
import {connect} from 'react-redux';

class Tracks extends Component {

    componentDidMount = () => {
        this.props.getAllTracks(this.props.tracks.filter);
        this.props.getEntityNames();
    }

    handleSubmit = async (values) => {
        await this.props.getAllTracks({
            entityName: values.entityNames.map(x => x.entityName),
            /*changesType: values.changesType.map(x => x.changesType),*/
            page: 1
        })
    }

    render() {
        const that = this;
        const {isPending, data, entityNames, filter} = this.props.tracks;
        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                <div className="d-flex">
                    {!isPending &&
                    data?.items &&
                        <div className="w-75">
                            <TrackList
                                data_list={data}
                                onPagination={
                                    (page) => {
                                        that.props.getAllTracks({page: page})
                                    }
                                }
                            />
                        </div>
                    }
                    <div className="w-25">
                        <TracksFilter
                            entityNames={entityNames}
                            changesType={filter.changesType}
                            onSubmit={this.handleSubmit}
                        />
                    </div>
                </div>
                </tbody>
            </table>
            {isPending ? <Spinner/> : null}
        </div>
    }
}

const mapStateToProps = (state) => {
    console.log("data", state.tracks.data);
    return {
        tracks: state.tracks,
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        
        getAllTracks: (filter) => dispatch(getAllTracks(filter)),
        getEntityNames: () => dispatch(getEntityNames()),
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(Tracks);
