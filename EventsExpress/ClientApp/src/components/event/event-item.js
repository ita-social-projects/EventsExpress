import React, {Component} from 'react';
import Moment from 'react-moment';
import 'moment-timezone';

export default class Event extends Component{

    renderCategories = (arr) =>{
        return arr.map((x) => (<span key={x.id}>#{x.name}</span>)
        );
    }

    render(){
        const { title, dateFrom, comment_count, description, photoUrl, categories } = this.props.item;
        const { city, country } = this.props.item.location;
        console.log('Event ', categories);

        return(
                <div className="blog-entry d-md-flex fadeInUp">
                    <a href="/#" className="img img-2" style={{backgroundImage: "url('" + photoUrl + "')"}}></a>
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
                            <p className="mb-4">{description}</p>
                            <p>{this.renderCategories(categories)}</p>
                            <p><a href="#" className="btn-custom">Read More <span className="ion-ios-arrow-forward"></span></a></p>
                    </div>
                </div>
        );
    }
}