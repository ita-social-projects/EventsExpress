import { useEffect, useState } from 'react';

export const useDelay = (delayedAction, initialValue, timeout = 1000) => {
    const [value, setValue] = useState(initialValue);

    useEffect(() => {
        const timeoutId = setTimeout(() => {
            delayedAction(value);
        }, timeout);
        return () => clearTimeout(timeoutId);
    }, [value]);

    return [value, setValue];
};
