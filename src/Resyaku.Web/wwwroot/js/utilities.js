export function toPascalCaseObject(obj) {
    return Object.entries(obj).reduce((acc, [key, value]) => {
        const pascalCaseKey = key.charAt(0).toUpperCase() + key.slice(1);
        acc[pascalCaseKey] = value;
        return acc;
    }, {});
}

export function isNumeric(value) {
    return typeof value === 'number' ||
        (typeof value === 'string' && value.trim() !== '' && !isNaN(value));
}