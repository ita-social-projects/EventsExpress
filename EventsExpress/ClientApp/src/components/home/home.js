import React, { Component } from 'react';
import './home.css';
import AddEventWrapper from '../../containers/add-event';
import EventListWrapper from '../../containers/event-list';


export default class Home extends Component{
    
    render(){
     
    return(
        <div>
            <AddEventWrapper />
        
            <EventListWrapper /> 
            {/* data_list={[{"isBlocked":false,"title":"asdad","description":"sadad","dateFrom":"2019-07-09T00:00:00","dateTo":"0001-01-01T00:00:00","categories":[],"photo":null,"ownerId":"f0f696aa-3c13-4ffb-773a-08d703976579","owner":null,"cityId":"81996ade-9c72-45c9-e60b-08d703976546","city":null,"eventId":"00000000-0000-0000-0000-000000000000","event":null,"visitors":[]}]} /> */}
            

            {/* <Event 
                title="A Loving Heart is the Truest Wisdom"
                date_from="June 28, 2019"
                comment_count="5"
                description="A small river named Duden flows by their place and supplies it with the necessary regelialia."/>
              <Event 
                title="A Loving Heart is the Truest Wisdom"
                date_from="June 28, 2019"
                comment_count="5"
                description="A small river named Duden flows by their place and supplies it with the necessary regelialia."/>
     */}
        </div>
        
    );
    }
}