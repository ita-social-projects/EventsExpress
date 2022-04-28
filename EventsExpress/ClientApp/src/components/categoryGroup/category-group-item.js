import React, {Component} from "react";
import IconButton from "@material-ui/core/IconButton";
import {Link} from "react-router-dom";

export default class CategoryGroupItem extends Component {
    render() {
        const {item} = this.props;

        return (
            <>
                <td>
                    <i className="fas fa-hashtag mr-1"/>
                    {item.title}
                </td>
                <td className="align-middle align-items-stretch">
                    <Link to={"/admin/categoriesGroup/" + item.id}>
                        <IconButton className="text-info" size="small">
                            <i className="fas fa-edit"/>
                        </IconButton>
                    </Link>
                </td>
            </>
        );
    }
}
