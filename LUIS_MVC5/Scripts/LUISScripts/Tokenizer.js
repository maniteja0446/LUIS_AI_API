
$("#ACulture").change(function () {    
    $("#AtokenizerVersion").empty();
    $.ajax({
        type: 'POST',
        url: '/LUISAppMaster/GetTokenNumberBasedonCulture', // we are calling json method
        dataType: 'json',
        data: { "code" : $("#ACulture").val() },
        success: function (mani) {
            for (var i = 0; i < mani.length; i++) {
                
                $("#AtokenizerVersion").append("<option>" + mani[i].Text + "</option>");
            }
            //alert(mani);
        },
        error: function (ex) {
             alert('Failed to retrieve states.' + ex);
            }
        });
        
    });
