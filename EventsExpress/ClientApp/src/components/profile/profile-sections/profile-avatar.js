import React from 'react'
import CustomAvatar from '../../avatar/custom-avatar'
import IconButton from '@material-ui/core/IconButton';
import { makeStyles } from '@material-ui/core/styles';
import EditAvatarModal from '../editProfile/editAvatarModals/edit-avatar-modal-with-options'


const useStyles = makeStyles({
  editIcon: {
    position:'absolute',
    display:'block',
    left:"50%" ,
    top: "80%",
    transform: "translateX(-50%)",
    zIndex:10,
    textAlign:"center",
    backgroundColor:"white",
    '&:active svg':{
      color :"red"
    }

},
avatar:{
  position:'relative',
  backgroundClip: "content-box",
   '&:hover $editIcon':{
     backgroundColor:"white"
     
   },
},


});

export default function ProfileAvatar(props) {
  const classes = useStyles();
  const [modalShow, setModalShow] = React.useState(false);
  const handleOpen = () => {
    setModalShow(true);
  }
  const handleClose = () =>{
    setModalShow(false);
  }
  return (
    <div className={classes.avatar}>
            <IconButton aria-label="delete" className={classes.editIcon} onClick={() => setModalShow(true)} >
              <i className="fas fa-camera"></i>
           </IconButton>

        <CustomAvatar height= "300px" width=  "300px"  name={props.name} userId={props.userId}/>

        <EditAvatarModal
          show={modalShow}
          onHide={handleClose}
          Open={handleOpen}
          name ={props.name}
          id = {props.userId}
        />
    </div>
)
}