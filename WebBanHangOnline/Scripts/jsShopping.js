$(document).ready(function () {
    ShowCount(); // Hiển thị số lượng sản phẩm trong giỏ hàng

    // Thêm sản phẩm vào giỏ hàng
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }

        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    $('#co-cart').attr('data-notify', rs.Count); // Cập nhật số lượng giỏ hàng
                    swal("Thêm vào giỏ hàng thành công!", "success"); // Thông báo thành công
                    LoadCart(); // Cập nhật lại giỏ hàng
                }
            }
        });
    });

    // Cập nhật số lượng sản phẩm trong giỏ hàng
    /*$('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity); // Gửi yêu cầu cập nhật số lượng sản phẩm
    });*/

    // Xóa hết sản phẩm trong giỏ hàng
    /*$('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng?');
        if (conf == true) {
            DeleteAll(); // Gửi yêu cầu xóa tất cả sản phẩm
        }
    });*/

    // Xóa một sản phẩm khỏi giỏ hàng
    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?');
        if (conf == true) {
            $.ajax({
                url: '/shoppingcart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.Count); // Cập nhật số lượng sản phẩm giỏ hàng
                        $('#trow_' + id).remove(); // Xóa dòng sản phẩm khỏi giỏ hàng
                        LoadCart(); // Cập nhật lại giỏ hàng
                        $('#co-cart').attr('data-notify', rs.Count);
                    }
                }
            });
        }
    });

    // Cập nhật số lượng khi nhấn nút tăng
    $('body').on('click', '.btn-increase', function () {
        var id = $(this).data("id");
        var quantityInput = $('#Quantity_' + id);
        var quantity = parseInt(quantityInput.val());
        quantity += 1;
        quantityInput.val(quantity);
        Update(id, quantity); // Gửi yêu cầu cập nhật số lượng sau khi tăng
    });

    // Cập nhật số lượng khi nhấn nút giảm
    $('body').on('click', '.btn-decrease', function () {
        var id = $(this).data("id");
        var quantityInput = $('#Quantity_' + id);
        var quantity = parseInt(quantityInput.val());
        if (quantity > 1) {
            quantity -= 1;
        }
        quantityInput.val(quantity);
        Update(id, quantity); // Gửi yêu cầu cập nhật số lượng sau khi giảm
    });
});

// Hàm hiển thị số lượng sản phẩm trong giỏ hàng
function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#co-cart').attr('data-notify', rs.Count); // Cập nhật số lượng giỏ hàng
        }
    });
}

// Hàm xóa tất cả sản phẩm trong giỏ hàng
/*function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                LoadCart(); // Cập nhật lại giỏ hàng
            }
        }
    });
}*/

// Hàm cập nhật số lượng sản phẩm trong giỏ hàng
function Update(id, quantity) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart(); // Cập nhật lại giỏ hàng
            }
        }
    });
}

// Hàm tải lại giỏ hàng (partial view)
function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs); // Cập nhật lại giao diện giỏ hàng
        }
    });
}
// Hàm tải lại item thanh toán
function LoadCheckOut() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_ThanhToan',
        type: 'GET',
        success: function (rs) {
            $('#load_data_checkout').html(rs); // Cập nhật lại giao diện giỏ hàng
        }
    });
}
