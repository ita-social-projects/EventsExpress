import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class InventoryService {

    getInventoriesByEventId = async (eventId) => {
        const res = await baseService.getResource(`inventory/GetInventar?eventId=${eventId}`);
        return res;
    }

    setItem = async (item, eventId) => {
        const value = {
            id: item.id,
            itemName: item.itemName,
            needQuantity: Number(item.needQuantity),
            unitOfMeasuring: {id: item.unitOfMeasuring}
        }
        const res = await baseService.setResource(`inventory/EditInventar/?eventId=${eventId}`, value);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setItemToInventory = async (item, eventId) => {
        const value = {
            itemName: item.itemName,
            needQuantity: Number(item.needQuantity),
            unitOfMeasuring: {id: item.unitOfMeasuring}
        }
        const res = await baseService.setResource(`inventory/AddInventar/?eventId=${eventId}`, value);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setItemDelete = async (itemId, eventId) => {
        const res = await baseService.setResource(`inventory/DeleteInventar/?itemId=${itemId}&eventId=${eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getUnitsOfMeasuring = async () => {
        return await baseService.getResource('unitofmeasuring/getall');
    }
}