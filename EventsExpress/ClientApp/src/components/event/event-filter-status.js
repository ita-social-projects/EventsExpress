import React, { Component } from 'react';

function eventStatusHistoryReadingString(option) {
    switch (option) {
        case 0:
            return " Active"
        case 1:
            return " Blocked"
        case 2:
            return " Canceled"
        default:
            return " Default status"
    }
}

function trackChangesTypeReadingString(option) {
    switch (option) {
        case 0:
            return " Undefined"
        case 1:
            return " Modified"
        case 2:
            return " Created"
        case 3:
            return " Deleted"
        default:
            return " Default status"
    }
}

class EventFilterStatus extends Component {
    checkboxGroup() {
        let { options, input } = this.props;
        if (input.value == "")
            input.value = options

        return options.map((option, index) => {
            return (
                <div className="checkbox" key={index}>
                    <label>
                        <input type="checkbox"
                            name={`${input.name}[${index}]`}
                            value={option}
                            checked={input.value.find(x => x === option) !== undefined}
                            onChange={(event) => {
                                const newValue = [...input.value];
                                if (event.target.checked) {
                                    newValue.push(option);
                                } else {
                                    newValue.splice(newValue.indexOf(option), 1);
                                }
                                return input.onChange(newValue);
                            }} />
                        {options.length >= 4 
                            ? trackChangesTypeReadingString(option) 
                            : eventStatusHistoryReadingString(option) 
                        }
                    </label>
                </div>)
        });
    }
    render() {
        return (
            <div>
                {this.checkboxGroup()}
            </div>
        )
    }
}
export default EventFilterStatus;

