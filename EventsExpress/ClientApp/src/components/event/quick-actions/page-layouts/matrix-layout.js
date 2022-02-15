import ViewModuleIcon from '@material-ui/icons/ViewModule';
import { QuickActionButton } from '../quick-action-button';
import React from 'react';
import { SetEventsLayout } from '../../../../actions/events-layout';
import { connect } from 'react-redux';

const MatrixLayout = (props) => {
    const setMatrixLayout = () => props.setLayout('matrix');

    return (
        <QuickActionButton
            title="Matrix layout"
            icon={<ViewModuleIcon />}
            onClick={setMatrixLayout}
        />
    );
};

const mapDispatchToProps = (dispatch) => ({
    setLayout: (data) => dispatch(SetEventsLayout(data))
});

export default connect(null, mapDispatchToProps)(MatrixLayout);
