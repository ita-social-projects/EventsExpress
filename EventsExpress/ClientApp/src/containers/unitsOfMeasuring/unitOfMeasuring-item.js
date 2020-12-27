import React, { Component } from "react";
import { connect } from "react-redux";
import IconButton from "@material-ui/core/IconButton";
import UnitOfMeasuringItem  from '../../components/unitOfMeasuring/unitOfMeasuring-item'

class UnitOfMeasuringItemWrapper extends Component {    
    render() {
        const {item} = this.props;
        
        return <tr>               
                     <UnitOfMeasuringItem 
                        item={item}                         
                    />                
                <td className="align-middle align-items-stretch">
                    <div className="d-flex align-items-center justify-content-center">
                        <IconButton className="text-danger" size="small">
                            <i className="fas fa-trash"></i>
                        </IconButton>
                    </div>
                </td>
                
            </tr>
    };
}

const mapStateToProps = state => { 
    return{        
        
    }
    
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        
    };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UnitOfMeasuringItemWrapper);