// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#responsive1,#responsive2,#responsive3').lightSlider({
        item: 4.5,
        loop: true,
        slideMove: 2,
        easing: 'cubic-bezier(0.25, 0, 0.25, 1)',
        speed: 600,
        slideMargin: 10,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    item: 3.8,
                    slideMove: 1,
                    slideMargin: 10
                }
            },
            {
                breakpoint: 992,
                settings: {
                    item: 4.3,
                    slideMove: 1,
                    slideMargin: 10
                }
            },
            {
                breakpoint: 768,
                settings: {
                    item: 3.1,
                    slideMove: 1,
                    slideMargin: 10
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
    $('#responsivepopular1,#responsivepopular2,#responsivepopular3').lightSlider({
        item: 2.2,
        loop: true,
        slideMove: 1,
        slideMargin: 10,
        easing: 'cubic-bezier(0.25, 0, 0.25, 1)',
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    item: 1.8,
                    slideMove: 1,
                    slideMargin: 10,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    item: 3,
                    slideMove: 1,
                    slideMargin: 0,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    item: 3,
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