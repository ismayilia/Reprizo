$(function () {

    $(document).on("click", ".detail .cart .add-cart", function (e) {
        let id = $(this).parent().parent().attr("data-id");;
        console.log(id)
        let count = $(".basket-count").text();
        $.ajax({
            url: `/shop/addbasket?id=${id}`,
            type: "Post",
            success: function (res) {

                count++;
                $(".basket-count").text(count);

            }
        })

    })
})