import React, { Component } from 'react';

class InventoryHeaderButton extends Component {

    render() {
        const { isOpen, handleOnClickCaret } = this.props;
        return (
            <div className="d-flex justify-content-start align-items-center">
                <h4>List of inventories</h4>
                {isOpen && <button type="button" title="Caret" className="btn clear-backgroud d-flex justify-content-start align-items-center" onClick={handleOnClickCaret}>
                    <i className="fas fa-angle-up" />
                </button>
                }
                {!isOpen && <button type="button" title="Caret" className="btn clear-backgroud d-flex justify-content-start align-items-center" onClick={handleOnClickCaret}>
                    <i className="fas fa-angle-down" />
                </button>
                }
            </div>
        );
    }
}

export default InventoryHeaderButton;
