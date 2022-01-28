import UserMoreInfoService from "../../services";
import { SubmissionError } from "redux-form";
import { getRequestInc, getRequestDec } from "../request-count-action";
import { buildValidationState } from "../../components/helpers/action-helpers";

const api_serv = new UserMoreInfoService();

export default function (data) {
    return async (dispatch) => {
        dispatch(getRequestInc());
        let response = await api_serv.create(data);
        dispatch(getRequestDec());
        if (!response.ok()) {
            throw new SubmissionError(await buildValidationState(response));
        }
        return Promise.resolve();
    };
}
