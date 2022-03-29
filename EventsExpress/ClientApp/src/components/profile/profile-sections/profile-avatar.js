import React from 'react'
import CustomAvatar from '../../avatar/custom-avatar'
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';
import { makeStyles } from '@material-ui/core/styles';


const useStyles = makeStyles({
  deleteIcon: {
    position:'absolute',
    display:'none',
    left:"50%" ,
    top: "80%",
    transform: "translateX(-50%)",
    zIndex:10,
    textAlign:"center",
    '&:active':{
      color :"red"
    }

},
avatar:{
  position:'relative',
  backgroundClip: "content-box",
    '&:hover':{
       cursor: "pointer",
    },
   '&:hover $deleteIcon':{
     display: 'block',
   },
   '&:hover img':{
     opacity: 0.3
   }

},


});

export default function ProfileAvatar(props) {
  const classes = useStyles();
  return (
    <div className={classes.avatar}>
            <IconButton aria-label="delete" className={classes.deleteIcon} >
              <DeleteIcon fontSize="large" />
           </IconButton>
        <CustomAvatar height= "300px" width=  "300px"  name={props.name} userId={props.userId}/>
    </div>
)
}