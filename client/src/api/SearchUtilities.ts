export enum StringOperations {
    contains,
    beginsWith,
    is
}

export const StringOperationValues = [
    { name: 'Is', value: 0 },
    { name: 'Begins With', value: 1 },
    { name: 'Contains', value: 2 },
]


export enum LogStatus {
    active,
    closed
}