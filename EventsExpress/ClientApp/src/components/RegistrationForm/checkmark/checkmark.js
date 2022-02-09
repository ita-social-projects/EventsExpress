import React from 'react';
import './CheckMarkAnimation.css';

// TODO: consider rewriting styles to hooks
const CheckMark = () => {
    return (
        <>
            <svg class="checkmark" viewBox="0 0 52 52">
                <circle
                    class="checkmark__circle"
                    cx="26"
                    cy="26"
                    r="25"
                    fill="none"
                />
                <path
                    class="checkmark__check"
                    fill="none"
                    d="M14.1 27.2l7.1 7.2 16.7-16.8"
                />
            </svg>
        </>
    );
};

export default CheckMark;
