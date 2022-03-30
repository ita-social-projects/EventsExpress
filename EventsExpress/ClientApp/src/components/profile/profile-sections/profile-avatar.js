import React from 'react'
import CustomAvatar from '../../avatar/custom-avatar'
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';
import { makeStyles } from '@material-ui/core/styles';
import SimpleModalWithDetails from '../../helpers/simple-modal-with-details';

const useStyles = makeStyles({
  deleteIcon: {
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
   '&:hover $deleteIcon':{
     backgroundColor:"white"
     
   },
},


});

export default function ProfileAvatar(props) {
  const classes = useStyles();
  return (
    <div className={classes.avatar}>
      <SimpleModalWithDetails
           data= "Are you sure?"
           button={
            <IconButton aria-label="delete" className={classes.deleteIcon} >
              <i class="fas fa-camera"></i>
           </IconButton>
           }/>
        <CustomAvatar height= "300px" width=  "300px"  name={props.name} userId={props.userId}/>
    </div>
)
}