import PhotoService from '../services/PhotoService';

const photoService = new PhotoService();

const asyncValidate = async (values) => {

    var err;

    if (values.photo !== null)
    {
        let response = await photoService.setEventTempPhoto(values.id, values.photo.file);

        if (!response.ok) {
            err = await response.json();
            err = err.errors[`Photo`];
            throw { photo: err[0] };
        }
    }
}
export default asyncValidate;