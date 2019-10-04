// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#responsive1,#responsive2,#responsive3').lightSlider({
        item: 6,
        loop: true,
        slideMove: 2,
        slideMargin: 11,
        easing: 'cubic-bezier(0.25, 0, 0.25, 1)',
        speed: 600,
        responsive: [
            {
                breakpoint: 2262,
                settings: {
                    item: 6,
                    slideMove: 1,
                    slideMargin: 11
                }
            },
            {
                breakpoint: 1921,
                settings: {
                    item: 5,
                    slideMove: 1,
                    slideMargin: 10
                }
            },
            {
                breakpoint: 1601,
                settings: {
                    item: 3,
                    slideMove: 2,
                    slideMargin: 10
                }
            },
            {
                breakpoint: 1201,
                settings: {
                    item: 3,
                    slideMove: 2,
                    slideMargin: 2
                }
            },
            {
                breakpoint: 992,
                settings: {
                    item: 5,
                    slideMove: 1,
                    slideMargin: 10
                }
            },
            {
                breakpoint: 769,
                settings: {
                    item: 5,
                    slideMargin: 100,
                    slideMove: 1
                }
            },
            {
                breakpoint: 577,
                settings: {
                    item: 3,
                    slideMargin: -30,
                    slideMove: 1
                }
            }
        ]
    });
    $('#responsivepopular1,#responsivepopular2,#responsivepopular3').lightSlider({
        item: 3,
        loop: true,
        slideMove: 1,
        slideMargin: 10,
        easing: 'cubic-bezier(0.25, 0, 0.25, 1)',
        responsive: [
            {
                breakpoint: 1920,
                settings: {
                    item: 3,
                    slideMove: 1,
                    slideMargin: 10,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    item: 1,
                    slideMove: 1,
                    slideMargin: 0,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    item: 1,
                    slideMove: 1
                }
            },
            {
                breakpoint: 576,
                settings: {
                    item: 3,
                    slideMove: 1
                }
            }
        ]
    });
});