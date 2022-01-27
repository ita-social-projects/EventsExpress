import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import React from 'react';
import { useCategoryFilterStyles } from './category-filter-styles';
import MultiCheckbox from '../../../../helpers/form-helpers/MultiCheckbox';
import { Field } from 'redux-form';


export const CategoryGroup = ({name, categories}) => {
    const classes = useCategoryFilterStyles();
    let optionsList = [];
    if(categories){
        categories.forEach((e, ind) => {
            optionsList.push({id: ind, value: e.id, text: e.name})
        })
    }

    return (
        <div className={classes.wrapper}>
            <FilterExpansionPanel 
                title={name}
                clearButton={false}
            >
                <div className={classes.wrapper}>
                    <Field
                        name="categories"
                        component={MultiCheckbox}
                        options={optionsList}
                    />
                </div>
            </FilterExpansionPanel>
        </div>
    );
};