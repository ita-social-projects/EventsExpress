import ViewListIcon from '@material-ui/icons/ViewList';
import { QuickActionButton } from '../quick-action-button';
import React from 'react';
import { SetEventsLayout } from '../../../../actions/events-layout';
import { connect } from 'react-redux';


const ListLayout = (props) => {
    const SetListLayout = () => {
        console.log("test1");
        props.setLayout("list")
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
