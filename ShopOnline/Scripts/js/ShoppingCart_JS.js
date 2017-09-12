$(function () {
    $(".delete-item-cart").off('click').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            data: { idsach: $(this).data('id') },
            url: "/ShoppingCart/Delete",
            dataType: "json",
            type: "POST",
            success: function (res) {
                if (res.status == true) {
                    window.location.href = '/ShoppingCart/IndexShoppingCart';
                }
            }

        });

    });

    $("#deleteall").off('click').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            url: "/ShoppingCart/DeleteAll",
            dataType: "json",
            type: "POST",
            
            success: function (res) {
                if (res.status == true) {
                    window.location.href = '/ShoppingCart/IndexShoppingCart';
                }
            }

        });

    });


    $("#updateitem").off('click').on('click', function (e) {
        e.preventDefault();
        var sachlist = $('.soluongsach');//lấy ra các sách có class = soluongsach(nó sẽ là 1 danh sách)
        var cardlist = [];

        $.each(sachlist, function (i, item) {
            cardlist.push({
                Soluong: $(item).val(),
                Sachs: {
                    MaSach: $(item).data('id')
                }
            });//cartlist thêm vào 1 đối tượng có thuộc tính soluong và sách
        });

        $.ajax({
            url: "/ShoppingCart/Update",
            data: { model: JSON.stringify(cardlist) },//dùng stringify để phân tích đối tượng json thành chuỗi gửi về controller
            dataType: "json",
            type: "POST",
            success: function (res) {
                if (res.status == true) {
                    window.location.href = '/ShoppingCart/IndexShoppingCart';
                }
            }

        });

    });
});