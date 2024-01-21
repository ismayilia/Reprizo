$(function () {

    $(document).on("click", ".cart-add", function (e) {

        e.preventDefault();
        console.log(e)
        let id = $(this).parent().parent().attr("data-id");;
        console.log(id)
        let count = $(".basket-count").text();
        console.log(count)
        $.ajax({
            url: `shop/addbasket?id=${id}`,
            type: "Post",
            success: function (res) {

                count++;
                console.log
                $(".basket-count").text(count);

            }
        })

    })

    $(document).on("click", ".basket-table .fa-plus", function (e) {

        let id = parseInt($(this).attr("data-id"))
        let count = $(".basket-count").text();
        $.ajax({

            url: `cart/plusicon?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).prev().text(res.countItem)
                $(".grand-total span .grandtotal").text(res.grandTotal.toFixed(2));
                $(e.target).parent().next().next().children().text(res.productTotalPrice.toFixed(2))
                count++;

                $(".basket-count").text(count);
            }
        })

    })

    $(document).on("click", ".basket-table .fa-minus", function (e) {

        let id = parseInt($(this).attr("data-id"))
        let count = $(".basket-count").text();
        let a = 0;

        $.ajax({

            url: `cart/minusicon?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).next().text(res.countItem)
                $(".grand-total span .grandtotal").text(res.grandTotal.toFixed(2));
                $(e.target).parent().next().next().children().text(res.productTotalPrice.toFixed(2))
                $(".basket-count").text(res.countBasket)

            }
        })

    })



    $(document).on("click", ".delete-basket-item", function (e) {
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `cart/delete?id=${id}`,
            type: "Post",
            success: function (res) {


                $(".basket-count").text(res.count);
                $(e.target).closest("tr").remove();
                $(".grand-total span .grandtotal").text(res.grandTotal);

                if (res.count === 0) {
                    $(".empty").removeClass("d-none");
                    $(".basket-table").addClass("d-none");
                }


            }
        })
    

    })
})