import { useEffect, useState } from 'react';
import { UserService } from '../../../../../services';
import { stringify } from 'query-string';

export const useOrganizerAutocomplete = (input, options) => {
    const [selectedOrganizers, setSelectedOrganizers] = useState([]);

    const updateOrganizers = (event, values) => {
        const last = values[values.length - 1].id;
        setSelectedOrganizers(
            selectedOrganizers.concat(
                options.find(organizer => organizer.id === last)
            )
        );

        input.onChange(
            input.value.concat(last)
        );
    };

    const deleteOrganizer = organizerToDelete => {
        return () => {
            setSelectedOrganizers(
                selectedOrganizers.filter(organizer => organizer.id !== organizerToDelete.id)
            );

            input.onChange(
                input.value.filter(id => id !== organizerToDelete.id)
            );
        };
    };

    const fetchOrganizers = async () => {
        const userService = new UserService();
        const response = await userService.getUsersShortInformation(
            `?${stringify({ ids: input.value }, { arrayFormat: 'index' })}`
        );
        if (!response.ok) {
            return;
        }

        const organizers = await response.json();
        setSelectedOrganizers(organizers);
    };

    useEffect(() => {
        if (input.value.length !== 0) {
            fetchOrganizers();
        }
    }, []);

    useEffect(() => {
        const inputErased = input.value.length === 0;
        if (inputErased) {
            setSelectedOrganizers([]);
        }
    }, [input.value]);

    return { selectedOrganizers, updateOrganizers, deleteOrganizer };
};
