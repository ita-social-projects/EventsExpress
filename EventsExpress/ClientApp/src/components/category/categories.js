import React, { Component } from 'react';
import CategoryAddWrapper from '../../containers/categories/category-add';
import CategoryListWrapper from '../../containers/categories/category-list';


export default class Categories extends Component{
    
    render() {
     
        return <div>
                <table className="table w-75">
                    <tbody>
                        <CategoryAddWrapper 
                            item={{name: "", id: "00000000-0000-0000-0000-000000000000"}} 
                        />
                        <CategoryListWrapper /> 
                    </tbody>
                </table> 
            </div>
    }
}