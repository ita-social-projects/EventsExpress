import { FilterExpansionPanel } from '../../expansion-panel/filter-expansion-panel';
import { CategoryGroupPanel } from './category-group-panel';
import React, { useEffect } from 'react';
import { useCategoryFilterStyles } from './category-filter-styles';
import { Accordion } from '@material-ui/core';
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
            <CategoryGroupPanel title={name}>
                <div className={classes.wrapper}>
                    <Field
                        name="categories"
                        component={MultiCheckbox}
                        //type="select-multiple"
                        options={optionsList}
                    />
                </div>
            </CategoryGroupPanel>
        </div>
    );
};