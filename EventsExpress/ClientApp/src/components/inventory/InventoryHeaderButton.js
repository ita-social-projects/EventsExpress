import React, { Component } from 'react';

class InventoryHeaderButton extends Component {
    render() {
        const { isOpen, handleOnClickCaret } = this.props;
        return (
            <div className="d-flex justify-content-start align-items-center">
            <h4>List of inventories</h4>
            {isOpen
                ? <button type="button" title="Caret" className="btn clear-backgroud d-flex justify-content-start align-items-center" onClick={handleOnClickCaret}>
                    <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                        <path fillRule="evenodd" d="M3.204 5L8 10.481 12.796 5H3.204zm-.753.659l4.796 5.48a1 1 0 0 0 1.506 0l4.796-5.48c.566-.647.106-1.659-.753-1.659H3.204a1 1 0 0 0-.753 1.659z"/>
                    </svg>
                </button>
                :  <button type="button" title="Caret" className="btn clear-backgroud d-flex justify-content-start align-items-center" onClick={handleOnClickCaret}>
                    <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-caret-down-fill" fill="currentColor" xmlns="http://www.w3.org/2000/svg">                            
                        <path d="M7.247 11.14L2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z"/>
                    </svg>
                </button>
            }
            </div>
        );
    }
}

export default InventoryHeaderButton;