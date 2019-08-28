import React, { Component } from 'react';
import Moment from 'react-moment';
import 'moment-timezone';
import '../layout/colorlib.css';
import './event-item-view.css';
import { Link } from 'react-router-dom';
import Avatar from '@material-ui/core/Avatar';
import Comment from '../comment/comment';
import EditEventWrapper from '../../containers/edit-event'; 
export default class EventItemView extends Component {

    state = { edit: false }


    renderCategories = (arr) => {
        return arr.map((x) => (<span key={x.id}>#{x.name}</span>)
        );
    }

    renderUsers = (arr) => {
        return arr.map(
            (x) => (<div className="d-flex align-items-center">
                <Avatar
                    alt="Тут аватар"
                    src={x.photoUrl}

                    className='littleAvatar'
                /><p><Link to={'/user/' + x.id} className="btn-custom"><h4>{x.username} {this.getAge(x.birthday)}</h4></Link></p>
            </div>)
        );
    }

    getAge = (birthday) => {
        let today = new Date();
        let birthDate = new Date(birthday);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }
        if (age >= 100) {
            age = "---";
        }
        return age;
    }

    onEdit = () => {
        this.setState({edit: true});
    }

    render() {
        const { photoUrl, categories, title, dateFrom, dateTo, description, user, visitors } = this.props.data;

        const { current_user } = this.props;

        const { country, city } = this.props.data;

        const categories_list = this.renderCategories(categories);

        let edit = false;
        console.log();
        let i_join = visitors.find(
            x => x.id == current_user.id
        );

        let flag = i_join == null;
        return <>
        <div className="container-fluid mt-1">
            <div className="row">
            <div className="col-9">
                <div className="col-12">
                    <img src={photoUrl} alt="Norway" style={{width: '100%'}} />
                    <div className="text-block"> 
                        <span className="title">{title}</span><br/>
                        <span><Moment format="D MMM YYYY" withTitle>{dateFrom}</Moment> {dateTo != dateFrom && <>- <Moment format="D MMM YYYY" withTitle>{dateTo}</Moment></>}</span><br/>
                        <span>{country} {city}</span><br/>
                        {categories_list}
                    </div>
                    <div className="button-block">
                        
                            {new Date(dateFrom) >= new Date().setHours(0,0,0,0) && current_user.id != user.id && current_user.id != null &&
                                <>
                                    {flag == true &&
                                        <button onClick={this.props.onJoin} className="btn btn-join">Join</button>
                                    }
                                    {!flag &&
                                        <button onClick={this.props.onLeave} className="btn btn-join">Leave</button>
                                    }
                                </>
                            }
                            {new Date(dateFrom) >= new Date().setHours(0,0,0,0) &&current_user.id === user.id && !this.state.edit &&
                                        <button onClick={this.onEdit} className="btn btn-join">Edit</button>
                            }
                        
                    </div>
                </div>
                {this.state.edit &&
                    <div className="row shadow mt-5 p-5 mb-5 bg-white rounded">
                        <EditEventWrapper />
                     </div>
                    }
                    {!this.state.edit && <>
                    <div className="text-box overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">

                        {description}
                    </div>
                    <div className="text-box overflow-auto shadow p-3 mb-5 mt-2 bg-white rounded">
                            <Comment match={this.props.match}/>
                    </div>
                    </>
                    }
            </div>
            <div className="col-3 overflow-auto shadow p-3 mb-5 bg-white rounded">
                {this.renderUsers(visitors)}
            </div>

            </div>
        </div>
        </>
    }
}