import ViewModuleIcon from '@material-ui/icons/ViewModule';
import { QuickActionButton } from '../quick-action-button';
import React, { useContext } from 'react';
import { SetEventsLayout } from '../../../../actions/events-layout';
import { connect } from 'react-redux';
import { useSessionItem } from '../quick-actions-hooks';
import { PAGE_SIZE } from '../../../../constants/constants';
import { RefreshEventsContext } from '../quick-actions';

const MatrixLayout = (props) => {
    const pageSizeItem = useSessionItem(PAGE_SIZE);
    const refreshEvents = useContext(RefreshEventsContext);
    const setMatrixLayout = () => {
        pageSizeItem.reset();
        props.setLayout('matrix');
        refreshEvents();
    };

    return (
        <QuickActionButton
            active={props.currentLayout === 'matrix'}
            title="Matrix layout"
            icon={<ViewModuleIcon />}
            onClick={setMatrixLayout}
        />
    );
};

const mapStateToProps = (state) => ({
    currentLayout: state.events.layout
});

const mapDispatchToProps = (dispatch) => ({
    setLayout: (data) => dispatch(SetEventsLayout(data))
});

export default connect(mapStateToProps, mapDispatchToProps)(MatrixLayout);
