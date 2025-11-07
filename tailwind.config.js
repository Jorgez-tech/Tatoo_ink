/** @type {import('tailwindcss').Config} */
export default {
    darkMode: ["class"],
    content: [
        "./index.html",
        "./src/**/*.{js,ts,jsx,tsx}",
    ],
    theme: {
        extend: {
            borderRadius: {
                lg: 'var(--radius)',
                md: 'calc(var(--radius) - 2px)',
                sm: 'calc(var(--radius) - 4px)'
            },
            colors: {
                border: "var(--color-border)",
                background: "var(--color-background)",
                foreground: "var(--color-foreground)",
                ring: "var(--color-ring)",
                input: "var(--color-input)",
            },
        },
    },
    plugins: [],
}
