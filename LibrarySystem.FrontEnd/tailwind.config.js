/** @type {import('tailwindcss').Config} */

export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      screens: {
        "max-sm": { max: "639px" },
        "md-v2": "1025px",
        "3xl": "1600px",
      },
    },
  },

  plugins: [require("tailwind-scrollbar")],
};
