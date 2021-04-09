import React, {Component} from 'react';
import TrackList from './track-list';
import Spinner from '../spinner';
import getAllTracks, {getEntityNames, setFilterEntities} from '../../actions/tracks/track-list-action';
import {connect} from 'react-redux';

class Tracks extends Component {

    componentDidMount = () => {
        this.props.getAllTracks(this.props.tracks.filter);
        this.props.getEntityNames();
    }

    handleSubmit = (values) => {
        this.props.getAllTracks(values)
    }

    render() {
        const that = this;
        const {isPending, data, entityNames} = this.props.tracks;
        return <div>
            <table className="table w-100 m-auto">
                <tbody>
                {!isPending &&
                data?.items ?
                    <TrackList
                        onEntitiesSelected={(names) => {
                            that.props.setFilterEntities(names);
                        }}
                        data_list={data}
                        onSearch={
                            (page) => {
                                that.props.getAllTracks({
                                    ...that.props.filter,
                                    page: page
                                })
                            }
                        }
                        entityNames={entityNames}
                    />
                    : null
                }
                </tbody>
            </table>
            {isPending ? <Spinner/> : null}
        </div>
    }
}

const mapStateToProps = (state) => ({
    tracks: state.tracks,
    filter: state.tracks.filter
});


const mapDispatchToProps = (dispatch) => {
    return {
        getAllTracks: (filter) => dispatch(getAllTracks(filter)),
        getEntityNames: () => dispatch(getEntityNames()),
        setFilterEntities: (names) => dispatch(setFilterEntities(names)),
    }
};


export default connect(mapStateToProps, mapDispatchToProps)(Tracks);