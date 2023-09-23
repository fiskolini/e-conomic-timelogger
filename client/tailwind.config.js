const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
    content: ['./src/**/*.js', './src/**/*.jsx', './src/**/*.tsx'],
    theme: {
        extend: {
            fontFamily: {
                sans: ['Roboto', ...defaultTheme.fontFamily.sans],
                material: ['Material Icons'],
            },
        },
    },
    variants: {
        extend: {},
    },
    plugins: []
}