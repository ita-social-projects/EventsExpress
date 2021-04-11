import { validateEventFormPart5 } from '../helpers/helpers'


export default (async function submit(values, dispatch, props) {
    console.log("test", props, values);
    props.add_event({ ...validateEventFormPart5(props.form_values), user_id: props.user_id, id: props.event.id });
});
