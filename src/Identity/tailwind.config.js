const colors = require('tailwindcss/colors')

module.exports = {
    purge: {
        content: [
            './**/*.html',
            '.**/*.cshtml'
        ],
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            colors: {
                primary: colors.blue,
            }
        },
    },
    variants: {
        extend: {
            backgroundColor: ['active'],
            borderColor: ['active'],
        },
    },
    plugins: [],
}