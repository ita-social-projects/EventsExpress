import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { change, Field, getFormValues } from 'redux-form';
import React, { useEffect, useState, useRef } from 'react';
import { useCategoryFilterStyles } from './category-filter-styles';
import { CategoryGroup } from './category-group';
import { connect } from 'react-redux';
import get_categories, { getCategories } from '../../../../../actions/category/category-list-action';


const HandleCategories = categories => {
    let temp = [];
    categories.forEach(c => {
        if(!temp.find(groupe => groupe.name === c.categoryGroup.title)){
            temp.push({name: c.categoryGroup.title, categories: [{id: c.id, name: c.name}]})
        }
        else{
            temp.find(groupe => groupe.name === c.categoryGroup.title)
            .categories
            .push({id: c.id, name: c.name});
        }
    });
    return temp;
}

const CategoryFilter = ( {dispatch, categoriesList, formValues, ...props} ) => {
    const classes = useCategoryFilterStyles();
    const clear = () => props.change(false);

    let Categories = [];

    const [categoryGroups, setCategoryGroups] = useState([]);
    const isMounted = useRef(false);

    useEffect(() => {
        props.getCategories();
    }, []);

    useEffect(() => {        
        if (isMounted.current) {
            Categories = HandleCategories(categoriesList);
            setCategoryGroups(Categories);
        } 
        else {
            isMounted.current = true;
        }      
    }, [categoriesList]);

    return (
        <FilterExpansionPanel
            title="Category"
            onClearClick={clear}
            clearDisabled={!formValues.categories.length}
        >
            <div className={classes.chips}>
                {categoryGroups.map((group) => (
                    <CategoryGroup key={group.name} name={group.name} categories={group.categories} />
                ))}
            </div>
        </FilterExpansionPanel>
    );
};

const mapStateToProps = state => {
    return {
        categoriesList: state.categories.data,
        formValues: getFormValues('filter-form')(state)
    };
};

const mapDispatchToProps = dispatch => ({
    change: () => dispatch(change('filter-form', 'categories', [])),
    getCategories: () => dispatch(get_categories())
});

export default connect(mapStateToProps, mapDispatchToProps)(CategoryFilter);