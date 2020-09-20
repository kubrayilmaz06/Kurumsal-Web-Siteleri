$(document).ready(function () {
    $("#owl-carousel-service-7").owlCarousel({
        margin:0,
        items: 1,
        loop: true,
        center: false,
        mouseDrag: true,
        nav: true,
        dots: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 1
            },
            720: {
                items: 2
            },
            960: {
                items: 2
            },
            1140: {
                items: 3
            }
        },
        navText: ["<i class=\"fa fa-angle-left\" aria-hidden=\"true\"></i>", "<i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i>"]
    });
});