import React from 'react'
import { Field, reduxForm } from 'redux-form'
import Multiselect from 'react-widgets/lib/Multiselect'
import Button from "@material-ui/core/Button";
import { emphasize, makeStyles, useTheme } from '@material-ui/core/styles';
const suggestions = [
  'Summer' ,
 'Mount' ,
  'Party' ,
 'Gaming' ,
 'Golf' 
];


const renderMultiselect = ({ input, data, valueField, textField }) =>
    <Multiselect {...input}
        onBlur={() => input.onBlur()}
        value={input.value || []} // requires value to be an array
        data={data}
        valueField={valueField}
        textField={textField}
    />
const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        height: 250,
    },
    input: {
        display: 'flex',
        padding: 0,
        height: 'auto',
    },
    valueContainer: {
        display: 'flex',
        flexWrap: 'wrap',
        flex: 1,
        alignItems: 'center',
        overflow: 'hidden',
    },
    chip: {
        margin: theme.spacing(0.5, 0.25),
    },
    chipFocused: {
        backgroundColor: emphasize(
            theme.palette.type === 'light' ? theme.palette.grey[300] : theme.palette.grey[700],
            0.08,
        ),
    },
    noOptionsMessage: {
        padding: theme.spacing(1, 2),
    },
    singleValue: {
        fontSize: 16,
    },
    placeholder: {
        position: 'absolute',
        left: 2,
        bottom: 6,
        fontSize: 16,
    },
    paper: {
        position: 'absolute',
        zIndex: 1,
        marginTop: theme.spacing(1),
        left: 0,
        right: 0,
    },
    divider: {
        height: theme.spacing(2),
    },
}));

function SelectCategories(props) {
    const { handleSubmit, submitting } = props
    const classes = useStyles();
    const theme = useTheme();
    const selectStyles = {
        input: base => ({
            ...base,
            color: theme.palette.text.primary,
            '& input': {
                font: 'inherit',
            },
        }),
    };
    return ( 
        <div className={classes.root}>
          
        <form onSubmit={handleSubmit}>
                <Field
                    classes={classes}
                    styles={selectStyles}
                    inputId="react-select-single"
                            name="Categories"
                            component={renderMultiselect}
                    data={suggestions}
                    TextFieldProps={{
                        label: 'Country',
                        InputLabelProps: {
                            htmlFor: 'react-select-single',
                            shrink: true,
                        },
                        placeholder: 'Search a country (start with a)',
                    }}
                    />
                    <div>
                        <Button type="submit" color="primary" disabled={ submitting} >
                            Save
                        </Button >
            </div>
            </form>
        </div>
  )
} 
export default reduxForm({
    form: 'SelectCategories',  // a unique identifier for this form
})(SelectCategories)