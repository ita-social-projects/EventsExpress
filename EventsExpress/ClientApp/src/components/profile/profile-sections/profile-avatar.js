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
    top: "75%",
    transform: "translateX(-50%)",
    zIndex:100
  

},
avatar:{
  position:'relative',
  backgroundClip: "content-box",
    '&:hover':{
       cursor: "pointer",
       opacity : 0.5

    },
   '&:hover $deleteIcon':{
     display: 'block',
     backgroundColor: "white",
     borderRadius : "50%"
     
   }
},


});

export default function ProfileAvatar(props) {
  const classes = useStyles();
  return (
    <div className={classes.avatar}>
        <div className= {classes.deleteIcon} >
          
            <IconButton aria-label="delete"  >
              <DeleteIcon fontSize="large" />
           </IconButton>
        </div>
        <CustomAvatar height= "300px" width=  "300px"  name={props.name} userId={props.userId}/>
    </div>
)
}