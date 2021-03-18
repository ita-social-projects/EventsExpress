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

class EventFilterStatus extends Component {
    checkboxGroup() {
        let { options, input } = this.props;
        input.value = input.value.StatusHistory || input.value;        
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
                        {eventStatusHistoryReadingString(option)}
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

