import Tracks  from '../../components/tracks/track';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

const mapStateToProps = (state) => ({
    tracks: state.tracks
});

const mapDispatchToProps = (dispatch) => { return bindActionCreators(() => { }, dispatch); };

export default connect(mapStateToProps, mapDispatchToProps)(Tracks);