/**
 * Parses time to guarantee is using 00 or 01 instead of 0 and 1 respectively
 * @param val
 */
export const parseTime = (val: number | string): string => {
    return ('0' + val).slice(-2);
}