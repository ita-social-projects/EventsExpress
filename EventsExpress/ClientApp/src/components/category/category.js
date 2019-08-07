import React, { Component } from 'react';
import AddCategory from '../../containers/add-category';
import CategoryListWrapper from '../../containers/category-list';


export default class Category extends Component{
    
    render() {
     
        return <div>
                <table className="w-75 table">
                    <tbody>
                        <AddCategory item={{name: "", id: "000"}} />
                        <CategoryListWrapper /> 
                    </tbody>
                </table> 
            </div>
    }
}