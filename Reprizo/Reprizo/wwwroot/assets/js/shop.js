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

    // FIlter

    (function () {

        var parent = document.querySelector(".range-slider");
        if (!parent) return;

        var
            rangeS = parent.querySelectorAll("input[type=range]"),
            numberS = parent.querySelectorAll("input[type=number]");

        rangeS.forEach(function (el) {
            el.oninput = function () {
                var slide1 = parseFloat(rangeS[0].value),
                    slide2 = parseFloat(rangeS[1].value);

                if (slide1 > slide2) {
                    [slide1, slide2] = [slide2, slide1];

                }

                numberS[0].value = slide1;
                numberS[1].value = slide2;
            }
        });

        numberS.forEach(function (el) {
            el.oninput = function () {
                var number1 = parseFloat(numberS[0].value),
                    number2 = parseFloat(numberS[1].value);

                if (number1 > number2) {
                    var tmp = number1;
                    numberS[0].value = number2;
                    numberS[1].value = tmp;
                }

                rangeS[0].value = number1;
                rangeS[1].value = number2;

            }
        });

    })();

    //Sort
    // $(document).on("click", ".item0", function () {
    //   let usdText = $(".item0").eq(0).text();
    //   $(".button-dropdown").eq(0).text(usdText)
    // })

    // $(document).on("click", ".item1", function () {
    //   let usdText = $(".item1").eq(0).text();
    //   $(".button-dropdown").eq(0).text(usdText)
    // })

    // $(document).on("click", ".item2", function () {
    //   let usdText = $(".item2").eq(0).text();
    //   $(".button-dropdown").eq(0).text(usdText)
    // })

    // $(document).on("click", ".item3", function () {
    //   let usdText = $(".item3").eq(0).text();
    //   $(".button-dropdown").eq(0).text(usdText)
    // })

    // $(document).on("click", ".item4", function () {
    //   let usdText = $(".item4").eq(0).text();
    //   $(".button-dropdown").eq(0).text(usdText)
    // })



    // Serarch

    $(document).on("submit", ".hm-searchbox", function (e) {
        e.preventDefault();
        let value = $(".input-search").val();
        let url = `/Shop/Search?searchText=${value}`;
        window.location.assign(url);

    })

    // Sort

    $(document).on("change", "#sort", function (e) {
        e.preventDefault();

        //// Remove the 'selected' attribute from the currently selected option
        //$(this).find("option:selected").removeAttr("selected");

        //// Set the 'selected' attribute for the newly selected option
        //$("#sort").find(":selected").attr("selected", "selected");

        let sortValue = $(this).val();

        let url = `/Shop/Sort?sortValue=${sortValue}`;

        window.location.assign(url);

  


        //$(this).val() = sortValue;
        //$('option:selected', this).text() = sortText;

    });



    // Filter

    $(document).on("click", "#filterForm", function (e) {
        e.preventDefault();
        let value1 = $(".min-price").val();
        let value2 = $(".max-price").val();
        let data = { value1: value1, value2: value2 }
        let parent = $(".product-list");

        $.ajax({
            url: "/Shop/Filter",
            type: "Get",
            data: data,
            success: function (res) {
                $(parent).html(res);

            }
        })

        let count = $(".product-count .count");
        $.ajax({
            url: "/Shop/FilterCount",
            type: "Get",
            data: data,
            success: function (res) {
                count.text(res);
            }
        })
    })

    














})


