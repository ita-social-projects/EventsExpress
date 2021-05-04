import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class UnitOfMeasuringService {
    getUnitsOfMeasuring = async () => baseService.getResource('unitofmeasuring/all');

    setUnitOfMeasuringDelete = data => baseService.setResource(`unitOfMeasuring/delete/${data}`);

    setUnitOfMeasuring = data => baseService.setResource('unitOfMeasuring/create', {
        unitName: data.unitName,
        shortName: data.shortName
    });

    editUnitOfMeasuring = data => baseService.setResource('unitOfMeasuring/edit', {
        id: data.id,
        unitName: data.unitName,
        shortName: data.shortName
    });
}

