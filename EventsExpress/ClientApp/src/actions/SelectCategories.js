export const SELECT_CATEGORIES_SECCESS = "SELECT_CATEGORIES_SECCESS";
export const SELECT_CATEGORIES_ERROR = "SELECT_CATEGORIES_ERROR";
export default function save(categories) {
    return dispatch => {
    callSelectCategoriesApi(categories, error => {
        if (!error) {
            dispatch(selectCategoriesSeccess(true));
        } else {
            dispatch(selectCategoriesError(error));
        }
    });
    };
}

function selectCategoriesSeccess(IsSelectCategoriesSeccess) {
    return {
        type: SELECT_CATEGORIES_SECCESS,
        IsSelectCategoriesSeccess
    };
}
function selectCategoriesError(IsSelectCategoriesError) {
    return {
        type: SELECT_CATEGORIES_ERROR,
        IsSelectCategoriesError
    };
}
function callSelectCategoriesApi(categories, callback) {
    setTimeout(() => {
        if (categories !==0) {
            return callback(null);
        } else {
            return callback(new Error("List of categories is empty"));
        }
    }, 1000);
}