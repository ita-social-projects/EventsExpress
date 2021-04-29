import './attitude.css';

export const getAttitudeClassName = (attitude) => {
    switch (attitude) {
        case 0:
            return "attitude-like";
        case 1:
            return "attitude-dislike";
        default:
            return '';
    }
}



