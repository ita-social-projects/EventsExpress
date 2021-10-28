
export const normalizeEventForm = values => {

    if (!values)
        return values;

    if (!values.isPublic) {
        values.isPublic = false;
    }

    if (values.isReccurent) {
        if (!values.frequency) {
            values.frequency = 0;
        }
    }

    if (!values.maxParticipants) {
        values.maxParticipants = 2147483647;
    }

    if (!values.dateFrom) {
        values.dateFrom = new Date(Date.now());
    }

    if (!values.dateTo) {
        values.dateTo = new Date(values.dateFrom);
    }

    return values;
}