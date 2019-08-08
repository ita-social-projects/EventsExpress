import Categories  from '../../components/category/categories';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

const mapStateToProps = (state) => ({
    categories: state.categories
});

const mapDispatchToProps = (dispatch) => { return bindActionCreators(() => { }, dispatch); };

export default connect(mapStateToProps, mapDispatchToProps)(Categories);