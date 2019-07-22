import { reduxForm } from 'redux-form';
import { CategoryForm } from '../components/category/category-form';
import { bindActionCreators } from 'redux';

const mapStateToProps = (state) => ({
    ...state
});

const mapDispatchToProps = (dispatch) => { return bindActionCreators(() => { }, dispatch); };

export default connect(mapStateToProps, mapDispatchToProps)(CategoryForm);