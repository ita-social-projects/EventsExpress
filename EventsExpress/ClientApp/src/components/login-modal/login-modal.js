import React from "react";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from "@material-ui/core/Button";
import Paper from "@material-ui/core/Paper";
import { makeStyles } from "@material-ui/core/styles";
import Tabs from "@material-ui/core/Tabs";
import Tab from "@material-ui/core/Tab";
import PersonPinIcon from "@material-ui/icons/PersonPin";
import LockOpen from "@material-ui/icons/LockOpen";
import Typography from "@material-ui/core/Typography";
import LoginWrapper from "../../containers/login";
import RegisterWrapper from "../../containers/register";
import { connect } from "react-redux";
import { toggleLoginModalState } from "../../actions/login-modal";
import RecoverPasswordModal from "../recoverPassword/recover-password-modal";

function TabContainer(props) {
  return (
    <Typography component="div" style={{ padding: 8 * 3 }}>
      {props.children}
    </Typography>
  );
}

const useStyles = makeStyles({
  root: {
    flexGrow: 1,
    maxWidth: 500,
  },
});

function LoginModal(props) {
  const classes = useStyles();
  const [value, setValue] = React.useState(0);

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };
  const { onClose } = props;
  const open = props.status.isOpen;
  return (
    <div className="d-inline-block">
      <Dialog open={open} onClose={onClose}>
      <DialogTitle >{"Please Log in to the system or create an account"}</DialogTitle>
        <Paper square className={classes.root}>
          <Tabs
            value={value}
            onChange={handleChange}
            variant="fullWidth"
            indicatorColor="primary"
            textColor="primary"
          >
            <Tab icon={<LockOpen />} label="Login" />
            <Tab icon={<PersonPinIcon />} label="Register" />
          </Tabs>

          {value === 0 && (
            <TabContainer>
              <LoginWrapper />
            </TabContainer>
          )}
          {value === 1 && (
            <TabContainer>
              <RegisterWrapper handleClose={onClose} />
            </TabContainer>
          )}

          <div className="text-center">
            <RecoverPasswordModal />
          </div>
          <Button fullWidth onClick={onClose} color="primary">
            Cancel
          </Button>
        </Paper>
      </Dialog>
    </div>
  );
}

const mapStateToProps = (state) => ({
  status: state.modal,
});

const mapDispatchToProps = (dispatch) => ({
  onClose: () => dispatch(toggleLoginModalState(false)),
});

export default connect(mapStateToProps, mapDispatchToProps)(LoginModal);
