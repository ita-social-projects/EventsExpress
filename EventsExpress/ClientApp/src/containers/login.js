import { reduxForm } from 'redux-form';
import { Login } from '../components/account/LoginForm'
const mapStateToProps = (state) => ({
    ...state
});

const mapDispatchToProps = (dispatch) => ({
    login: () => dispatch(actionCreators)
});

export const LoginForm = reduxForm({
    form: 'login-form'
})(Login)

export default connect(mapStateToProps, mapDispatchToProps)(LoginForm);