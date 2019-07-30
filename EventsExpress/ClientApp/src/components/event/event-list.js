import React, {Component} from 'react';
import Event from './event-item';

export default class EventList extends Component{
  
  renderItems = (arr) => {
   return arr.map((item) => {

      return (
          <Event key={item.id} item={item} />
      );
    });
  }
  render()
  {
      const { data_list } = this.props;

      const items = this.renderItems(data_list);

      return <>
              {items}
      </>
  }
}