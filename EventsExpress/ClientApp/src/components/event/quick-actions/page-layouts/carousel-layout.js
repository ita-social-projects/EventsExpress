import ViewCarouselIcon from '@material-ui/icons/ViewCarousel';
import { QuickActionButton } from '../quick-action-button';
import React, { useContext } from 'react';
import { SetEventsLayout } from '../../../../actions/events-layout';
import { connect } from 'react-redux';
import { useSessionItem } from '../quick-actions-hooks';
import { PAGE_SIZE } from '../../../../constants/constants';
import { RefreshEventsContext } from '../quick-actions';

const CarouselLayout = (props) => {
    const pageSizeItem = useSessionItem(PAGE_SIZE);
    const refreshEvents = useContext(RefreshEventsContext);

    const setCarouselLayout = () => {
        pageSizeItem.value = 4;
        props.setLayout('carousel');
        refreshEvents();
    };

    return (
        <QuickActionButton
            active={props.currentLayout === 'carousel'}
            title="Carousel layout"
            icon={<ViewCarouselIcon />}
            onClick={setCarouselLayout}
        />
    );
};

const mapStateToProps = (state) => ({
    currentLayout: state.events.layout
});

const mapDispatchToProps = (dispatch) => ({
    setLayout: (data) => dispatch(SetEventsLayout(data))
});

export default connect(mapStateToProps, mapDispatchToProps)(CarouselLayout);
