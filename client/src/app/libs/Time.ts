/**
 * Parses time to guarantee is using 00 or 01 instead of 0 and 1 respectively
 * @param m
 */
export const parseTime = (m: number): { hours: number, minutes: number } => {
    const hours = Math.floor(m / 60);
    const minutes = m % 60;

    return {hours, minutes};
}

/**
 * Validates if given date is valid date
 * @param d
 */
export const isValidFutureDate = (d: string) => {
    const parsedDate = new Date(d);
    
    return (
        !isNaN(parsedDate.getTime()) &&
        d.trim() !== '' &&
        parsedDate > new Date()
    )
}