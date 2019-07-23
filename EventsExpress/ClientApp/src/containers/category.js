
import { Category } from '../components/category/category';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

const mapStateToProps = (state) => ({
    categories: state.categories
});

const mapDispatchToProps = (dispatch) => { return bindActionCreators(() => { }, dispatch); };

export default connect(mapStateToProps, mapDispatchToProps)(Category);