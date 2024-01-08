$(function () {

    // Wishlist icon Start
    let wishWhiteIcons = document.querySelectorAll(".product .product-icons .fa-regular")

    let wishBlackIcons = document.querySelectorAll(".product .product-icons .fa-solid.fa-heart")

    wishWhiteIcons.forEach(wishWhiteIcon => {
        wishWhiteIcon.addEventListener("click", function () {
            this.nextElementSibling.classList.remove("d-none");
            this.classList.add("d-none");

        })
    });

    wishBlackIcons.forEach(wishBlackIcon => {
        wishBlackIcon.addEventListener("click", function () {
            this.previousElementSibling.classList.remove("d-none");
            this.classList.add("d-none");

        })
    });

    // Wishlist icon End
})