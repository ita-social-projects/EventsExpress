
import { validateEventFormPart1 } from '../helpers/helpers'


export default (async function submit(values, dispatch, props) {
    props.add_event({ ...validateEventFormPart1(props.form_values), user_id: props.user_id, id: props.event.id });
});
