import ViewCarouselIcon from '@material-ui/icons/ViewCarousel';
import { QuickActionButton } from '../quick-action-button';
import React from 'react';
import { SetEventsLayout } from '../../../../actions/events-layout';
import { connect } from 'react-redux';
import { useFilterActions } from '../../filter/filter-hooks';

const CarouselLayout = (props) => {
    const { appendFilters } = useFilterActions();

    const setCarouselLayout = () => {
        appendFilters({ pageSize: 4 });
        props.setLayout('carousel');
    };

    return (
        <QuickActionButton
            title="Carousel layout"
            icon={<ViewCarouselIcon />}
            onClick={setCarouselLayout}
        />
    );
};

const mapDispatchToProps = (dispatch) => ({
    setLayout: (data) => dispatch(SetEventsLayout(data))
});

export default connect(null, mapDispatchToProps)(CarouselLayout);
