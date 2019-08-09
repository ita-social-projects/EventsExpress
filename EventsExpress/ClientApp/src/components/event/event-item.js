import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import Moment from 'react-moment';
import 'moment-timezone';

export default class Event extends Component {

    renderCategories = (arr) => {
        return arr.map((x) => (<span key={x.id}>#{x.name}</span>)
        );
    }

    render() {
        const { id, title, dateFrom, comment_count, description, photoUrl, categories } = this.props.item;
        const { city, country } = this.props.item.location;
        console.log('Event ', categories);

        return (
            <div className="blog-entry d-md-flex fadeInUp">
                <a href="/#" className="img img-2" style={{ backgroundImage: "url('" + photoUrl + "')" }}></a>
                <div className="text text-2 pl-md-4">
                    <h3 className="mb-2"><a href="/#">{title}</a></h3>
                    <div className="meta-wrap">
                        <p className="meta">
                            <span><i className="fa fa-calendar mr-2"></i><Moment format="D MMM YYYY" withTitle>
                                {dateFrom}
                            </Moment></span>
                            <span><a href="/#"><i className="fa fa-map mr-2"></i>{country} {city}</a></span>
                            <span><i className="fa fa-comment mr-2"></i>{comment_count} Comment</span>
                        </p>
                    </div>
                    <div className="h-15 overflow-hidden">
                        <p className="mb-4">{description}</p>
                    </div>
                    <p>{this.renderCategories(categories)}</p>
                    <p><Link to={'/event/' + id+'/'+1} className="btn-custom">Read More</Link></p>
                </div>
            </div>
        );
    }
}