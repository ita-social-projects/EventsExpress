import ViewListIcon from '@material-ui/icons/ViewList';
import { QuickActionButton } from '../quick-action-button';
import React, { useContext } from 'react';
import { SetEventsLayout } from '../../../../actions/events-layout';
import { connect } from 'react-redux';
import { useSessionItem } from '../quick-actions-hooks';
import { PAGE_SIZE } from '../../../../constants/constants';
import { RefreshEventsContext } from '../quick-actions';

const ListLayout = (props) => {
    const pageSizeItem = useSessionItem(PAGE_SIZE);
    const refreshEvents = useContext(RefreshEventsContext);
    const SetListLayout = () => {
        pageSizeItem.reset();
        props.setLayout("list")
        refreshEvents();
    }

    return (
        <QuickActionButton
            active={props.currentLayout === 'list'}
            title="List layout"
            icon={<ViewListIcon />}
            onClick={SetListLayout}
        />
    );
}

const mapStateToProps = (state) => ({
    currentLayout: state.events.layout
});

const mapDispatchToProps = (dispatch) => ({
    setLayout: (data) => dispatch(SetEventsLayout(data))
});

export default connect(mapStateToProps, mapDispatchToProps)(ListLayout)
