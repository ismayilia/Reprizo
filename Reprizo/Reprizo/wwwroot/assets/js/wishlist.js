$(function () {

    $(document).on("click", ".wishlist-add", function () {


        let id = $(this).parent().parent().attr("data-id");;
        let count = $(".wishlist-count").text();
        $.ajax({
            url: `/shop/addwishlist?id=${id}`,
            type: "Post",
            success: function (res) {

                $(".wishlist-count").text(res);

            }
        })

    })


    $(document).on("click", ".cart-add-basket", function () {


        let id = $(this).parent().attr("data-id");;
        let count = $(".basket-count").text();
        $.ajax({
            url: `shop/addbasket?id=${id}`,
            type: "Post",
            success: function (res) {

                count++;
                $(".basket-count").text(count);

            }
        })

    })

    

    $(document).on("click", ".delete-wishlist-item", function (e) {
        
        
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `wishlist/delete?id=${id}`,
            type: "Post",
            success: function (res) {


                $(".wishlist-count").text(res.count);
                $(e.target).closest("tr").remove();

                if (res.count === 0) {
                    $(".empty").removeClass("d-none");
                    $(".wishlist-table").addClass("d-none");
                }


            }
        })

    })
})