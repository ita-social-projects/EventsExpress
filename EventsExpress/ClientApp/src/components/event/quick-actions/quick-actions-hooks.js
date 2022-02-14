export const useSessionItem = itemName => {
    return new class {
        get value() {
            return sessionStorage.getItem(itemName);
        }
    
        set value(val) {
            sessionStorage.setItem(itemName, val);
        }

        reset() {
            sessionStorage.removeItem(itemName);
        }
    };
};
