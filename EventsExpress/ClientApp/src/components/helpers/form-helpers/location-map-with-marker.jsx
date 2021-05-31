import React from 'react';
import { Field } from 'redux-form';
import { Marker, Popup } from 'react-leaflet';
import { LocationMap } from '.';

export default function LocationMapWithMarker(props) {
    let initialPos = { lat: 50.4547, lng: 30.5238 };
    if (props.input.value.latitude != undefined) {
        initialPos = { lat: props.input.value.latitude, lng: props.input.value.longitude };
    }
    else {
        props.input.value.latitude = initialPos.lat;
        props.input.value.longitude = initialPos.lng;
    }
    const [location, setLocation] = React.useState(initialPos);

    function handleChange(latlng) {
        setLocation(latlng);
        props.input.onChange({ ...props.input.value, latitude: latlng.lat, longitude: latlng.lng });
    }

    function updateMarker(e) {
        handleChange(e.target.getLatLng());
    }

    return (
        <Field
            name='location'
            location = {location}
            onUpdate = {handleChange}
            component={LocationMap}
        >
            <Marker position={location}
                draggable={true}
                onDragend={updateMarker}>
                <Popup position={location}>
                    <pre>
                        {JSON.stringify(location, null, 2)}
                    </pre>
                </Popup>
            </Marker>
        </Field>
    );
}