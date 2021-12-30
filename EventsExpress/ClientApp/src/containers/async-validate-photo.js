import PhotoService from '../services/PhotoService';

const photoService = new PhotoService();

const asyncValidatePhoto = async (values) => {
    if (values.photo === undefined) {
        throw { photo: 'Please, upload the photo.' };
    }

    const response = await photoService.setEventTempPhoto(values.id, values.photo.file);
    if (!response.ok) {
        const { errors } = await response.json();
        throw { photo: errors['Photo'], _error: errors._error };
    }
};

export default asyncValidatePhoto;
