'use strict'
const themeKey = 'theme-stylesheet';
const urlBase = 'https://cdn.jsdelivr.net/npm/water.css@2/out/';
const stylesheet = document.getElementById(themeKey);
const root = document.querySelector(':root').style;

const getTheme = () => window.localStorage.getItem(themeKey);

const setTheme = (theme) => window.localStorage.setItem(themeKey, theme);

const updateTheme = () => {
    const theme = getTheme();
    stylesheet.href = `${urlBase}${theme}.min.css`;

    if (theme === 'light') {
        root.setProperty('--blue', '#1976d2');
        root.setProperty('--blue-hover', '#0d47a1');
        root.setProperty('--red', '#d32f2f');
        root.setProperty('--red-hover', '#b71c1c');
        root.setProperty('--amber', '#ffa000');
        root.setProperty('--amber-hover', '#ff6f00');
    }
    else {
        root.setProperty('--blue', '#004ba0');
        root.setProperty('--blue-hover', '#0069c0');
        root.setProperty('--red', '#9a0007');
        root.setProperty('--red-hover', '#ba000d');
        root.setProperty('--amber', '#c67100');
        root.setProperty('--amber-hover', '#c79100');
    }
};

const toggleTheme = () => {
    const newTheme = getTheme() === 'light' ? 'dark' : 'light';
    setTheme(newTheme);
    updateTheme();
    return newTheme;
};

if (!getTheme()) {
    const isLight = window.matchMedia('(prefers-color-scheme: light)').matches;
    setTheme(isLight ? 'light' : 'dark');
}

window.themeKey = themeKey;
window.getTheme = getTheme;
window.setTheme = setTheme;
window.updateTheme = updateTheme;
window.toggleTheme = toggleTheme;

updateTheme();