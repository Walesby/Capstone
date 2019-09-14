// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#responsive1,#responsive2,#responsive3').lightSlider({
        item: 5,
        auto: true,
        loop: true,
        slideMove: 2,
        slideMargin: 10,
        easing: 'cubic-bezier(0.25, 0, 0.25, 1)',
        speed: 600,
        responsive: [
            {
                breakpoint: 1500,
                settings: {
                    item: 4,
                    slideMove: 2,
                    slideMargin: 25,
                }
            },
            {
                breakpoint: 1200,
                settings: {
                    item: 3,
                    slideMove: 2,
                    slideMargin: 25,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    item: 3,
                    slideMove: 1,
                    slideMargin: 6,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    item: 2,
                    slideMove: 1
                }
            },
            {
                breakpoint: 576,
                settings: {
                    item: 1,
                    slideMove: 1
                }
            }
        ]
    });
    $('#responsivepopular1,#responsivepopular2,#responsivepopular3').lightSlider({
        item: 3,
        loop: true,
        slideMove: 1,
        slideMargin: 100,
        easing: 'cubic-bezier(0.25, 0, 0.25, 1)',
        speed: 600,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    item: 1,
                    slideMove: 2,
                    slideMargin: 0,
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
                    item: 1,
                    slideMove: 1
                }
            }
        ]
    });
});