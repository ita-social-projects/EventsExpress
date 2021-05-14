import React, { Component } from 'react';

class MultiCheckbox extends Component {
    checkboxGroup() {
        let { options, input } = this.props;
        if (input.value === "")
            input.value = [];

        return options.map((option, index) => {
            return (
                <div className="checkbox" key={index}>
                    <label>
                        <input type="checkbox"
                            name={`${input.name}[${index}]`}
                            value={option.value}
                            checked={input.value.find(x => x === option.value) !== undefined}
                            onChange={(event) => {
                                const newValue = [...input.value];
                                if (event.target.checked) {
                                    newValue.push(option.value);
                                } else {
                                    newValue.splice(newValue.indexOf(option.value), 1);
                                }
                                return input.onChange(newValue);
                            }} />
                        {option.text}
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
export default MultiCheckbox;

