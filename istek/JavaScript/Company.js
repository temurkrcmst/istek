var uri = '/MP/api/v1/movie/CompanyAdd';
var Service = (function (m) {
    m.Company = {
        Add: function (obj, cb) {
            $.ajax({
                type: 'POST',
                data: JSON.stringify(obj),
                dataType: "json",
                contentType: "application/json",
                url: uri
            })
        }
    },

        m.Company = {
            Add: function (obj, cb) {

                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(obj),
                    dataType: "json",
                    contentType: "application/json",
                    url: '/MP/api/v1/movie/CompanyAdd'
                }).success(function (data) {
                    cb(data);
                });
            },
            Remove: function (params, cb) {

                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(params),
                    dataType: "json",
                    contentType: "application/json",
                    url: '/MP/api/v1/movie/DeleteCompany'
                }).success(function (data) {
                    cb(data);
                });
            },
            Update: function (params, cb) {
                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(params),
                    dataType: "json",
                    contentType: "application/json",
                    url: '/MP/api/v1/movie/UptadeCompany'
                }).success(function (data) {
                    cb(data);
                });
            },
            Get: function (obj, cb) {

                var GetObj = {
                    Column: "ID",
                    ColumnType: "string",
                    Value: "",
                    Statement: ""
                }

                if (obj != null) {
                    GetObj = obj;
                }

                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(GetObj),
                    dataType: "json",
                    contentType: "application/json",
                    url: '/MP/api/v1/movie/GetCompany'
                }).success(function (data) {
                    cb(data)
                });



            },
        }
    return m;
}(Service || {}))




//var deleteObj = {
//    FilterCol: "CompanyID",
//    FilterVal: ""
//};

//var obj = {
//    CompanyID: "1",
//     CompanyName: "Temur"
//};
$(document).ready(function () {
    $("#AddCompanySave").click(function () {
        var companyname = $("#CompanyName").val();

        var obj = {
            CompanyID: "1",
            CompanyName: companyname

        };

        Service.Company.Add(obj, function (data) {
           var al = $('<div class="alert alert-success">' + companyname + ' Başarıyla eklendi..</div>');

           if (data.Type == 0) {
               $(alertSave).html("Kayıt ekleme esnasında hata oluştu..");
               $(alertSave).removeClass("alert-success");
               $(alertSave).addClass("alert-danger");
           }
           else {
           }
           $(".infoArea").html("").append(al);
        })
        
    });
    $("#CompanyUptade").click(function () {
        var companyname = $("#CompanyName").val();


        var sendObj = {
            FilterCol: "CompanyID",
            FilterVal: GetID,
            CompanyName: companyname
        };
        Service.Company.Update(sendObj, function (data) {
            var alert = $('<div class="alert alert-success">' + companyname + ' Başarıyla Güncellendi..</div>');

            if (data.Type == 0) {
                $(alert).html("Kayıt güncelleme esnasında hata oluştu");
                $(alert).removeClass("alert-success");
                $(alert).addClass("alert-danger");
            }

            $(".infoArea").html("").append(alert);


        });
    });
    $("#CompanyDelete").click(function () {
        var deleteObj = {
            FilterCol: "CompanyID",
            FilterVal: GetID
        };

        Service.Company.Remove(deleteObj, function (data) {
            var alert = $('<div class="alert alert-success"> Başarıyla silindi..</div>');

            if (data.Type == 0) {
                $(alert).html("Kayıt silme esnasında hata oluştu");
                $(alert).removeClass("alert-success");
                $(alert).addClass("alert-danger");
            }
            $(".infoArea").html("").append(alert);
        });

    });
});

//Service.Company.Add(obj, function (data) {
//    $(".infoArea").html("").append(alert);
//  })

//Service.Company.Remove(deleteObj, function (data) {
//    if (data.Type == 0) {
//        $(alert).html("Kayıt silme esnasında hata oluştu");
//        $(alert).removeClass("alert-success");
//        $(alert).addClass("alert-danger");
//    }
//    $(".infoArea").html("").append(alert);
//})
//Service.Company.Update(sendObj, function (data) {
//    $(".infoArea").html("").append(alert);
//})
//Service.Company.Get(obj, function (data) {
//    $(".infoArea").html("").append(alert);
//})