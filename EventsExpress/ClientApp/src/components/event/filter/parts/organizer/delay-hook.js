import { useEffect, useRef, useState } from 'react';

export const useDelay = (delayedAction, initialValue, skipFirstRun = true, timeout = 1000) => {
    const [value, setValue] = useState(initialValue);

    const isFirstRun = useRef(skipFirstRun);
    useEffect(() => {
        if (isFirstRun.current) {
            isFirstRun.current = false;
            return;
        }

        const timeoutId = setTimeout(() => {
            delayedAction(value);
        }, timeout);
        return () => clearTimeout(timeoutId);
    }, [value]);

    return [value, setValue];
};
