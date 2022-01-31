export const useSessionFilter = filterName => {
    return new class {
        get value() {
            return sessionStorage.getItem(filterName);
        }
    
        set value(val) {
            sessionStorage.setItem(filterName, val);
        }

        reset() {
            sessionStorage.removeItem(filterName);
        }
    };
}
