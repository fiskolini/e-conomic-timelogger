import {isValidFutureDate, parseTime} from "@/app/libs/Time";

describe('testing \'parseTime\' function', () => {
    test.each([
        [0, {hours: 0, minutes: 0}],
        [60, {hours: 1, minutes: 0}],
        [91, {hours: 1, minutes: 31}],
        [200, {hours: 3, minutes: 20}],
    ])('parseTime(%i) should return %p', (input, expected) => {
        expect(parseTime(input)).toEqual(expected);
    });
});

describe('testing \'isValidDate\' function', () => {
    test.each([
        ['foo', false],
        ['2014-55-26', false],
        ['2023/02/01', false],
        ['2024-01-01', true],
    ])('isValidDate(%s) should return %p', (input, expected) => {
        expect(isValidFutureDate(input)).toEqual(expected);
    });
});