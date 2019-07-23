import React from "react";
import CategoryItem from "../components/category/category-item";
import CategoryEdit from "../components/category/category-edit";
import { connect } from "react-redux";
import deleteCat from "../actions/delete-category";
import save from "../actions/add-category";
import Fab from '@material-ui/core/Fab';
import Icon from '@material-ui/core/Icon';
import '../components/category/Category.css';




class categoryItemWrapper extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            status: "info"
        };

    }
    submit = () => {
        let value = this.props.item.id;
        console.log('delete: ');
        console.log(value);
        this.props.deleteCat({ id: value });
    };

    edit = () => {
        this.setState({
            status: "edited"
        });
    };

    save = values => {
        console.log(values);
        this.props.save({ ...values, Id: this.props.item.id });
    };

    cancel = () => {
        this.setState({
            status: "info"
        });
    };

    render() {
        if (this.state.status == "info") {
            return (
                <div className="ItemCategory">
                    <CategoryItem item={this.props.item}/>
                    <div>
                        <div className="icon">
                        <Fab 
                            size="small"
                            color="primary"
                            className="mr-2"
                            onClick={this.edit}
                            aria-label="Edit">
                            <i className="fa fa-edit"></i>
                            </Fab>
                        <Fab
                            size="small"
                            color="primary"
                            onClick={this.submit}
                            aria-label="Edit">
                                <i className="fa fa-trash"></i>
                            </Fab>
                        </div>
                    </div>
                </div>
            )
        }
        return(
            <div className="ItemCategory">
                <CategoryEdit item={this.props.item} onSubmit={this.save}/>
                <div>
                    <Fab className="icon"
                        size="small"
                        color="primary"
                        onClick={this.cancel}
                        aria-label="Edit">
                        <i className="fa fa-times"></i>
                    </Fab>
                    <Fab size="small"
                        color="primary"
                        onClick={this.submit}
                        aria-label="Edit">
                        <i className="fa fa-trash"></i>
                    </Fab>
                </div>
            </div>
        )
    };
}
const mapStateToProps = state => ({ save: state.save });

const mapDispatchToProps = dispatch => {
    return {
        deleteCat: (id) => dispatch(deleteCat(id)),
        save: (data) => dispatch(save(data))
    };
};
export default connect(
    mapStateToProps,
    mapDispatchToProps
)(categoryItemWrapper);