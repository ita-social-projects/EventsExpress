import { reduxForm } from 'redux-form';
import { EventForm } from '../components/event/EventForm';
import { bindActionCreators } from 'redux';
 
const mapStateToProps = (state) => ({
    ...state
});

const mapDispatchToProps = (dispatch) => { return bindActionCreators(() => {}, dispatch); };

export default connect(mapStateToProps, mapDispatchToProps)(EventForm);