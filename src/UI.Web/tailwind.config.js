const colors = require('tailwindcss/colors')

module.exports = {
    purge: {
        mode: 'all',
        preserveHtmlElements: false,
        content: [
            './**/*.html',
            '.**/*.razor'
        ],
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            colors: {
                primary: colors.blue,
                irrigation: {
                    DEFAULT: colors.lightBlue[400],
                    hover: colors.lightBlue[300],
                    active: colors.lightBlue[500],
                },
                measurement: {
                    DEFAULT: colors.emerald[400],
                    hover: colors.emerald[300],
                    active: colors.emerald[500],
                }
            }
        },
    },
    variants: {
        extend: {
            backgroundColor: ['active'],
        },
    },
    plugins: [],
}