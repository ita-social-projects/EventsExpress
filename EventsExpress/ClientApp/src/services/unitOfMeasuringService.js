import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class UnitOfMeasuringService {
    getUnitsOfMeasuring = async () => {
        return await baseService.getResourceNew('unitofmeasuring/all');
    }

    setUnitOfMeasuringDelete = async (data) => {

        const res = await baseService.setResource(`unitOfMeasuring/delete/${data}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUnitOfMeasuring = async (data) => {
        const res = await baseService.setResource('unitOfMeasuring/create', {
            unitName: data.unitName,
            shortName: data.shortName
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    editUnitOfMeasuring = async (data) => {
        const res = await baseService.setResource('unitOfMeasuring/edit', {
            id: data.id,
            unitName: data.unitName,
            shortName: data.shortName
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

}
