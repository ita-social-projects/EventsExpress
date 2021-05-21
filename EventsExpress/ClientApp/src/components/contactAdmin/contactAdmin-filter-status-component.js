import React, { Component } from 'react';

function ContactAdminStatusReadingString(option) {
    switch (option) {
        case 0:
            return " Open"
        case 1:
            return " InProgress"
        case 2:
            return " Resolve"
        default:
            return " Default status"
    }
}

class ContactAdminFilterStatus extends Component {
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
                            onChange={(contactAdmin) => {
                                const newValue = [...input.value];
                                if (contactAdmin.target.checked) {
                                    newValue.push(option);
                                } else {
                                    newValue.splice(newValue.indexOf(option), 1);
                                }
                                return input.onChange(newValue);
                            }} />
                        {ContactAdminStatusReadingString(option)}
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
export default ContactAdminFilterStatus;

