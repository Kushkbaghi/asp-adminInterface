"use strict";
const url = window.location.pathname;                       //set pathnamn
var pro = document.querySelector('#pro');                 // homepage lemenet

const navLinks = document.querySelectorAll('ul a').       // set nav element as an array
    forEach(link => {
        if (url == "/") {
            pro.classList.add('active');
        } else {
            if (link.href.includes(url)) {
                link.classList.add('active');
            }
        }
    }
    )