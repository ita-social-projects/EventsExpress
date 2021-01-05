import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class UnitOfMeasuringService {

    getUnitsOfMeasuring = async () => {        
        return await baseService.getResource('unitofmeasuring/all');
    }
    //setUnitOfMeasuring = async (data) => {
    //    const res = await baseService.setResource('unitOfMeasuring/create', {
    //        name: data.name
    //    });
    //    return !res.ok
    //        ? { error: await res.text() }
    //        : res;
    //}

    //editCategory = async (data) => {
    //    const res = await baseService.setResource('category/edit', {
    //        id: data.id,
    //        name: data.name
    //    });
    //    return !res.ok
    //        ? { error: await res.text() }
    //        : res;
    //}

    setUnitOfMeasuringDelete = async (data) => {
    
       const res = await baseService.setResource(`unitOfMeasuring/delete/${data}`);
       return !res.ok
           ? { error: await res.text() }
           : res;
    }
    setUnitOfMeasuring = async (data) => {
        const res = await baseService.setResource('unitOfMeasuring/create', {
            // id:data.id,
            unitName:data.unitName,
            shortName:data.shortName
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
    editUnitOfMeasuring = async (data) => {
        const res = await baseService.setResource('unitOfMeasuring/edit', {
            id:data.id,
            unitName:data.unitName,
            shortName:data.shortName
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

}