import type {Config} from 'jest';

const config: Config = {
    verbose: true,
    transform: {
        '^.+\\.ts?$': ['ts-jest', {
            tsconfig: "tsconfig.spec.json",
            diagnostics: true
        }],
    },
    testEnvironment: 'node',
    testRegex: '/tests/.*\\.(test|spec)?\\.(ts|tsx)$',
    moduleFileExtensions: ['ts', 'tsx', 'js', 'jsx', 'json', 'node'],
    moduleNameMapper: {
        '^@/(.*)$': '<rootDir>/src/$1',
    },
};

export default config;