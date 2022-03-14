import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { reduxForm } from 'redux-form';
import get_category_groups from '../../../../actions/categoryGroup/category-group-list-action';
import get_categories from '../../../../actions/category/category-list-action';
import TileGroup from '../../../../containers/TileGroup';
import StepperNavigation from '../../StepperNavigation';

const ChooseActivities = ({
    categories,
    categoryGroups,
    getCategories,
    getCategoryGroups,
    handleSubmit,
}) => {
    useEffect(() => {
        getCategoryGroups();
        getCategories();
    }, []);

    const mapToCategories = () => {
        return categoryGroups.map(el => ({
            group: el,
            categories: categories.filter(c => c.categoryGroup.id === el.id),
        }));
    };

    return (
        <form onSubmit={handleSubmit}>
            <h1 className="mb-3">
                Step 3: Choose your favorite activities
            </h1>
            <h1 className="mb-3">
                What are your reasons for joining EventsExpress?
            </h1>
            <TileGroup data={mapToCategories()} />
            <div className="stepper-submit">
                <StepperNavigation />
            </div>
        </form>
    );
};

const mapStateToProps = state => ({
    categoryGroups: state.categoryGroups.data,
    categories: state.categories.data,
});

const mapDispatchToProps = dispatch => ({
    getCategoryGroups: () => dispatch(get_category_groups()),
    getCategories: () => dispatch(get_categories()),
});

export default connect(mapStateToProps, mapDispatchToProps)(
    reduxForm({
        form: 'registrationForm',
        destroyOnUnmount: false,
        forceUnregisterOnUnmount: true,
    })(ChooseActivities)
);
