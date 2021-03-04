import React, { Component } from 'react';
import TrackList from './track-list';
import Spinner from '../spinner';
import getAllTracks, { setFilterEntities} from '../../actions/tracks/track-list';

import { connect } from 'react-redux';
class Tracks extends Component{
    
    
    componentWillMount = () => this.props.getAllTracks(this.props.tracks.filter);

    handleSubmit = (values) => {
        this.props.getAllTracks(values)
    }

    render() {
        const that = this;
        const { isPending, data } = this.props.tracks;
        return <div>
                <table className="table w-75 m-auto">
                    <tbody>
                        {!isPending &&
                            data?.items ?
                                <TrackList
                                onEntitiesSelected={(names)=>{
                                    that.props.setFilterEntities(names);
                                }}
                                data_list={data}
                                onSearch={
                                    () => {
                                        that.props.getAllTracks(that.props.tracks.filter)
                                    }
                                }
                                entityNames={this.props.tracks.data.items}
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
        setFilterEntities: (names) => dispatch(setFilterEntities(names))
    }
};


export default connect(mapStateToProps, mapDispatchToProps)(Tracks);