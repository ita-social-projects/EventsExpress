import React, { Component } from "react";
import { reduxForm } from "redux-form";
import Button from "@material-ui/core/Button";



class categoryItem extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { click } = this.props;
        const { id, name } = this.props.item;
        return (
            <div className="ItemCategory">
                <h4>#{name}</h4>
                <div>
                    <Button className="btn" type="submit" onClick={click} value="deleteCat" color="primary">
                        Delete
                    </Button>
                </div>
            </div>
        );
    }
}

categoryItem = reduxForm({
    // a unique name for the form
    form: "deleteCat-form"
})(categoryItem);

export default categoryItem;
