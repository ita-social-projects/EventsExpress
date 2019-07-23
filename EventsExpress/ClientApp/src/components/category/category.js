import React, { Component } from 'react';
import AddCategory from '../../containers/add-category';
import CategoryListWrapper from '../../containers/category-list';


export default class Category extends Component{
    
    render(){
     
    return(
        <div>
            <AddCategory />
            
            <CategoryListWrapper /> 
          
        </div>
        
    );
    }
}