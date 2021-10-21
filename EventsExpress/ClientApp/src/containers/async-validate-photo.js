import PhotoService from '../services/PhotoService';

const photoService = new PhotoService();

const asyncValidatePhoto = async (values) => {

    var err;

    if (values.photo !== undefined) {
        let response = await photoService.setEventTempPhoto(values.id, values.photo.file);

        if (!response.ok) {
            err = await response.json();
            err = err.errors[`Photo`];
            throw { photo: err[0] };
        }
    }
    else {
        throw { photo: "Please, upload the photo." }
    }
}
export default asyncValidatePhoto;