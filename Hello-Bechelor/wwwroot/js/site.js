
$(document).ready(function () {
    Ladda.bind('.btn', { timeout: 1000 });
    jQueryModalGet = (url, title) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#form-modal .modal-body').html(res.html);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }

    }

    jQueryModalPost = form => {
       
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#viewAll').html(res.html)
                        $('#form-modal').modal('hide');
                        ShowMessage(res.message);
                        location.reload();
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
});

jQueryModalDelete = form => {
    swal.fire({
        title: "Are you sure to delete this?",
        showCancelButton: true,
        confirmButtonText: `Delete`,
        icon: `warning`,
    }).then((willDelete) => {
        if (willDelete.isConfirmed) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        $('#viewAll').html(res.html);
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex.message);
            }
        }
            //prevent default form submit event
            return false;
    });
}

function deleteData(id) {
   
}

    //jQueryModalDelete = form => {
    //    try {
    //        if (confirm('Are you sure to delete this record ?')) {
    //            debugger;
    //            try {


    //                debugger;
    //                $.ajax({
    //                    type: 'POST',
    //                    url: form.action,
    //                    data: new FormData(form),
    //                    contentType: false,
    //                    processData: false,
    //                    success: function (res) {

    //                        $('#viewAll').html(res.html);
    //                    },
    //                    error: function (err) {
    //                        console.log(err)
    //                    }
    //                })
    //            } catch (ex) {
    //                debugger;
    //                console.log(ex)
    //            }
    //        }
    //    } catch (ex) {
    //        console.log(ex)
    //    }

    //    //prevent default form submit event
    //    return false;
    //}

function ShowMessage(msg) {
    toastr.success(msg);
}

function ShowMessageError(msg) {
    toastr.error(msg);
}



function datetimeToDate(datetime) {
    debugger;
    var date = datetime.split(' ')[0];
    return date;
}

function getDbInfo(id) {
    console.log(id);
    $.ajax({
        type: "GET",
        url: '/Expense/Delete' + '?id=' + id,
        success: function (res) {
            $('#viewAll').html(res.html);
        },
        error: function (err) {
            console.log(err)
        }
    });
}

