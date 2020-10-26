var uri = '/MP/api/v1/movie/ScenarioAdd';
var Service = (function (m) {
    m.ScenarioFile = {
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

        m.Scenario = {
            Add: function (obj, cb) {

                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(obj),
                    dataType: "json",
                    contentType: "application/json",
                    url: '/MP/api/v1/movie/ScenarioAdd'
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
                    url: '/MP/api/v1/movie/DeleteScenario'
                }).success(function (data) {
                    cb(data);
                });
            },
            Update: function (obj, cb) {
                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(obj),
                    dataType: "json",
                    contentType: "application/json",
                    url: '/MP/api/v1/movie/UptadeScenario'
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
                    url: '/MP/api/v1/movie/GetScenario'
                }).success(function (data) {
                    cb(data)
                });



            },
        }
    return m;
}(Service || {}))



//var obj = {
//    ScenarioID:1,
//    ScenarioName:"Temur", 
//    Scenarioİmage:"jpg"
//};

//var deleteObj = {
//    FilterCol: "ScenarioID",
//    FilterVal: ""
//};
//var sendObj = {
//    FilterCol: "ScenarioID",
//    FilterVal: ""
//    //ScenarioName: "",
//    //Scenarioİmage: ""

//}

$(document).ready(function () {
    $("#AddScenarioSave").click(function () {
        var scenarioname = $("#ScenarioName").val();
        var scenarioimage = $("#Scenarioİmage").val();

        var obj = {
            ScenarioID: "1",
            ScenarioName: scenarioname,
            Scenarioİmage: scenarioimage

        };

        Service.Scenario.Add(obj, function (data) {
            var al = $('<div class="alert alert-success">' + scenarioname + ' Başarıyla eklendi..</div>');

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
    $("#ScenarioUptade").click(function () {
        var scenarioname = $("#ScenarioName").val();
        var scenarioimage = $("#Scenarioİmage").val();


        var sendObj = {
            FilterCol: "ScenarioID",
            FilterVal: GetID,
            ScenarioName: scenarioname,
            Scenarioİmage: scenarioimage
        };
        Service.Scenario.Update(sendObj, function (data) {
            var alert = $('<div class="alert alert-success">' + scenarioname + ' Başarıyla Güncellendi..</div>');

            if (data.Type == 0) {
                $(alert).html("Kayıt güncelleme esnasında hata oluştu");
                $(alert).removeClass("alert-success");
                $(alert).addClass("alert-danger");
            }

            $(".infoArea").html("").append(alert);


        });
    });
    $("#ScenarioDelete").click(function () {
        var deleteObj = {
            FilterCol: "ScenarioID",
            FilterVal: GetID
        };

        Service.Scenario.Remove(deleteObj, function (data) {
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

//Service.Scenario.Add(obj, function (data) {
//    $(".infoArea").html("").append(alert);
//});
//Service.Scenario.Remove(deleteObj, function (data) {
//    $(".infoArea").html("").append(alert);
//})
//Service.Scenario.Update(sendObj, function (data) {
//    $(".infoArea").html("").append(alert);
//})
//Service.Scenario.Get(obj, function (data) {
//    $(".infoArea").html("").append(alert);
//})