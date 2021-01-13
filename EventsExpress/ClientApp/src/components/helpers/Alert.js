import React from "react";
import clsx from "clsx";
import CheckCircleIcon from "@material-ui/icons/CheckCircle";
import ErrorIcon from "@material-ui/icons/Error";
import InfoIcon from "@material-ui/icons/Info";
import CloseIcon from "@material-ui/icons/Close";
import { amber, green } from "@material-ui/core/colors";
import IconButton from "@material-ui/core/IconButton";
import Snackbar from "@material-ui/core/Snackbar";
import SnackbarContent from "@material-ui/core/SnackbarContent";
import WarningIcon from "@material-ui/icons/Warning";
import { makeStyles } from "@material-ui/core/styles";

const variantIcon = {
  success: CheckCircleIcon,
  warning: WarningIcon,
  error: ErrorIcon,
  info: InfoIcon,
};

const useStyles1 = makeStyles(theme => ({
  success: {
    backgroundColor: green[600],
    zIndex: 100000,
  },
  error: {
    backgroundColor: theme.palette.error.dark,
    zIndex: 100000,
  },
  info: {
    backgroundColor: theme.palette.primary.main,
    zIndex: 100000,
  },
  warning: {
    backgroundColor: amber[700],
    zIndex: 100000,
  },
  icon: {
    fontSize: 20,
    zIndex: 100000,
  },
  iconVariant: {
    opacity: 0.9,
    marginRight: theme.spacing(1),
    zIndex: 100000,
  },
  message: {
    display: 'flex',
    alignItems: 'center',
    zIndex: 100000,
  },
}));

export default function MySnackbar(props) {
  const classes = useStyles1();
  const { onClose, ...other } = props;
  const { message, open, variant, autoHideDuration } = props.alert;
  const Icon = variantIcon[variant];
  let timeToShow;

  if(variant !== 'error' && autoHideDuration)
  {
    timeToShow = 5000;
  }

  return (
    <Snackbar
      anchorOrigin={{
        vertical: "bottom",
        horizontal: "left"
      }}
      open={open}
      autoHideDuration={timeToShow}
      onClose={onClose}
    >
      <SnackbarContent
        className={clsx(classes[variant])}
        aria-describedby="client-snackbar"
        message={
          <span id="client-snackbar" className={classes.message}>
            {Icon &&
              <Icon className={clsx(classes.icon, classes.iconVariant)} />
            }
            {message}
          </span>
        }
        action={[
          <IconButton key="close" aria-label="close" color="inherit" onClick={onClose}>
            <CloseIcon className={classes.icon} />
          </IconButton>,
        ]}
        {...other}
      />
    </Snackbar>
  )
}
