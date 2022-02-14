import { useDelay } from './delay-hook';

export const useUsersSearch = fetchUsers => {
    const fetchUsersBySpecifiedUsername = specifiedUsername => {
        fetchUsers(`?KeyWord=${specifiedUsername}`);
    };

    const [username, setUsername] = useDelay(fetchUsersBySpecifiedUsername, '');
    const updateUsername = event => setUsername(event.target.value);
    return [username, updateUsername];
};
