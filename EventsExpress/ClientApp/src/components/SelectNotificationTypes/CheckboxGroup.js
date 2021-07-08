import React from 'react';
class CheckboxGroup extends React.Component {
    checkboxGroup() {
        let { options, input } = this.props;
        input.value = input.value.notificationTypes || input.value;
        return (
            
            options.map((option, index) => {
                return (
                    <div className="checkbox" key={option.id}>
                        <label>
                            <input type="checkbox" style={{marginRight: "8px"}}
                                name={`${input.name}[${index}]`}
                                value={option||[]}
                                checked={input.value.find(x => x.id === option.id) !== undefined}
                                onChange={(event) => {                                    
                                    const newValue = [...input.value];
                                    if (event.target.checked) {
                                        newValue.push(option);
                                    } else {
                                        newValue.splice(newValue.findIndex(op => op.id === option.id), 1);
                                    }
                                    return input.onChange(newValue);
                                }}
                                
                            />
                            {option.name}
                        </label>
                    </div>
                )
            }))
    }

    render() {
        return (
            <div>
                {this.checkboxGroup()}
            </div>
        )
    }
}


export default CheckboxGroup;