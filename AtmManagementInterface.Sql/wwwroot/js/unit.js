var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
function unitFormatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white" class="edit btn btn-sm  btn-info"  title="Edit Unit">'
        + '<i class="fas fa-edit"></i>' +
        '<a style="color:white"  title="Remove unit" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}

window.unitEvents = {
    'click .edit': function (e, value, row, index) {
        if (row.state = true) {

            var data = JSON.stringify(row);
            $('#Id').val(row.id);
            $('#UnitCode').val(row.unitCode);
            $('#UnitName').val(row.unitName);           
            $('#Remark').val(row.remark);          
            $('#AddNewUnit').modal('show');
            $('#btnUnitUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: url_path + '/RemoveUnit',
            type: 'POST',
            data: { ID: row.id },
            success: function (data) {
                swal({
                    title: "Are you sure?",
                    text: "You are about to delete this record!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#ff9800",
                    confirmButtonText: "Yes, proceed",
                    cancelButtonText: "No, cancel!",
                    showLoaderOnConfirm: true,
                    preConfirm: function () {
                        return new Promise(function (resolve) {
                            setTimeout(function () {
                                resolve();
                            }, 4000);
                        });
                    }
                }).then(function (isConfirm) {
                    if (isConfirm) {



                        swal("Deleted succesfully");
                        //alert('Deleted succesfully');
                        $('#unitTable').
                            bootstrapTable(
                            'refresh', { url: url_path + '/listunit' });

                        //return false;
                    }
                    else {
                        swal("Unit", "You cancelled delete unit.", "error");
                    }
                    $('#unitTable').
                        bootstrapTable(
                            'refresh', { url: url_path + '/listunit' });
                });
                return

            },

            error: function (e) {
                //alert("An exception occured!");
                swal("An exception occured!");
            }
        });
    }
};

$(document).ready(function ($) {

    $('#btnUnitUpdate').on('click', function () {
        updateUnit();
    });

});

function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#UnitCode').val('');
    $('#UnitName').val('');
 
    $('#Remark').val('');

}

function updateUnit() {
    debugger
   $("input[type=submit]").attr("disabled", "disabled");
    $('#frmunit').validate({
        //messages: {
        //    UnitCode: { required: "UnitCode is required" },
        //    UnitName: { required: "Unit Name is required" },    

        //},
        errorPlacement: function (error, element) {
            $.notify({
                icon: "now-ui-icons travel_info",
                message: error.text(),
            }, {
                    type: 'danger',
                    placement: {
                        from: 'top',
                        align: 'right'
                    }
                });
        },
        submitHandler: function (form) {
            swal({
                title: "Are you sure?",
                text: "Unit will be Updated!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#ff9800",
                confirmButtonText: "Yes, continue",
                cancelButtonText: "No, stop!",
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve();
                        }, 4000);
                    });
                }
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $("#btnUnitUpdate").attr("disabled", "disabled");
                    debugger
                    var json_data = {};
                    json_data.Id = $('#Id').val();
                    json_data.UnitCode = $('#UnitCode').val();
                    json_data.UnitName = $('#UnitName').val();
                    json_data.Remark = $('#Remark').val();     

                    $.ajax({
                        url: url_path + '/UpdateUnit',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",                       
                        success: function (result) {                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update  Unit', text: 'Departmental Unit updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewUnit').modal('hide');
                                $('#unitTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path + '/listunit' });

                                $("#btnUnitUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update  Unit', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnUnitUpdated").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update  Unit', text: 'Update  Unit encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnUnitUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update  Unit', 'You cancelled  Unit update.', 'error');
            $("#btnUnitUpdate").removeAttr("disabled");
        });

}

$(document).ready(function ($) {

    $('#btnUnit').on('click', function () {
        addUnit();
    });

});
function addUnit() {
    $('#btnUnitUpdate').hide();
    $("input[type=submit]").attr("disabled", "disabled");

    $('#frmunit').validate({

        messages: {
            UnitCode: { required: "UnitCode is required" },
            UnitName: { required: "Unit Name is required" },    
         

        },
        errorPlacement: function (error, element) {
            $.notify({
                icon: "now-ui-icons travel_info",
                message: error.text(),
            }, {
                    type: 'danger',
                    placement: {
                        from: 'top',
                        align: 'right'
                    }
                });
        },
        submitHandler: function (form) {
            swal({
                title: "Are you sure?",
                text: "Unit will be saved!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#ff9800",
                confirmButtonText: "Yes, continue",
                cancelButtonText: "No, stop!",
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve();
                        }, 4000);
                    });
                }
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $("#btnUnit").attr("disabled", "disabled");
                    debugger
                    var json_data = {};
                    json_data.Id = $('#Id').val();
                    json_data.UnitCode = $('#UnitCode').val();
                    json_data.UnitName = $('#UnitName').val();
                    json_data.Remark = $('#Remark').val();   


                    $.ajax({
                        url: url_path + '/AddUnit',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Create  Unit', text: ' Unit updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewUnit').modal('hide');
                                $('#unitTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path + '/listunit' });

                                $("#btnUnit").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Create  Unit', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnUnit").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Create  Unit', text: 'Create  unit encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnUnit").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Create  Unit', 'You cancelled  unit creation.', 'error');
            $("#btnUnit").removeAttr("disabled");
        });

}

$('#unitTable').on('expand-row.bs.table', function (e, index, row, $detail) {
    $detail.html('Loading request...');

    var htmlData = '';
    var header = '<div>';
    var footer = '</div>';
    htmlData = htmlData + header;

    debugger

    var html =
        '<h8>' +
         '<p style="text-align:left">' +
        
        ' <strong>Unit Name: </strong> &nbsp' + row.unitName + '' + '<p>' +
        ' <strong>Unit Code: </strong> &nbsp' + row.unitCode + '' + '<p>' +
        ' <strong>Remark: </strong> &nbsp' + row.remark + '</div>';
    htmlData = htmlData + html;
    htmlData = htmlData + footer;
    $detail.html(htmlData);
});