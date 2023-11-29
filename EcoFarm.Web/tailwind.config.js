import { nextui } from "@nextui-org/react"
import { daisyui } from "@daisyui/react"

//const {nextui} = require("@nextui-org/react");


/** @type {import('tailwindcss').Config} */
export default {
    content: [
        "./index.html",
        "./src/**/*.{js,ts,jsx,tsx}",
        "./node_modules/@nextui-org/theme/dist/**/*.{js,ts,jsx,tsx}",
        //'node_modules/flowbite-react/lib/esm/**/*.js'
    ],
    theme: {
        extend: {
            // colors: {
            //     'red': '#ff7777',
            // }
        },
    },
    plugins: [
        
        //require('flowbite/plugin'),
    ]
    // darkMode: 'class',
    // plugins: [
    //     require(nextui()),
    // ],
    // daisyui: {
    //     theme: ["light"],
    //     //prefix: "du"
    // }
    //darkMode: 'class',
    //plugins: [nextui()],
}

function getDaisyUI() {
    return "daisyui";
}