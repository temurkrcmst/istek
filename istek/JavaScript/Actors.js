var uri = '/MP/api/values/ActorsAdd';
var Service = (function (m) {
    m.Actor = {
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

        m.Actor = {
            Add: function (obj, cb) {

                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(obj),
                    dataType: "json",
                    contentType: "application/json",
                    url: '/MP/api/v1/movie/ActorsAdd'
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
                    url: '/MP/api/v1/movie/DeleteActor'
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
                    url: '/MP/api/v1/movie/UptadeActor'
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
                    url: '/MP/api/v1/movie/GetActors'
                }).success(function (data) {
                    cb(data)
                });



            },
        }
    return m;
}(Service || {}))



//var obj = {
//    ActorID:1,
//    ActorName:"Temur",
//    Actorİmage:"jpg"
//};
//var deleteObj = {
//    FilterCol: "ActorID",
//    FilterVal: ""
//};
//var sendObj = {
//    FilterCol: "ActorID",
//    FilterVal: "",
//    //ActorName: "aasdasdasd",
//    //Actorİmage: "jpg"

//}
$(document).ready(function () {
    $("#AddActorSave").click(function () {
        var actorname = $("#ActorName").val();
        var actorimage = $("#Actorİmage").val();

        var obj = {
            ActorID: "1",
            ActorName: actorname,
            Actorİmage:actorimage

        };
        
        Service.Actor.Add(obj, function (data) {
            var al = $('<div class="alert alert-success">' + actorname + ' Başarıyla eklendi..</div>');

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
    $("#ActorUptade").click(function () {
        var actorname = $("#ActorName").val();
        var actorimage = $("#Actorİmage").val();



        var sendObj = {
            FilterCol: "ActorID",
            FilterVal: GetID,
            ActorName: actorname,
            Actorİmage: actorimage
        };
        Service.Actor.Update(sendObj, function (data) {
            var alert = $('<div class="alert alert-success">' + actorname + ' Başarıyla Güncellendi..</div>');

            if (data.Type == 0) {
                $(alert).html("Kayıt güncelleme esnasında hata oluştu");
                $(alert).removeClass("alert-success");
                $(alert).addClass("alert-danger");
            }

            $(".infoArea").html("").append(alert);


        });
    });
    $("#ActorDelete").click(function () {
        var deleteObj = {
            FilterCol: "ActorID",
            FilterVal: GetID
        };

        Service.Actor.Remove(deleteObj, function (data) {
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

//Service.Actor.Add(obj, function (data) {
//    $(".infoArea").html("").append(alert);
//});
//Service.Actor.Remove(deleteObj, function (data) {
//    $(".infoArea").html("").append(alert);
//})
//Service.Actor.Update(sendObj, function (data) {
//    $(".infoArea").html("").append(alert);
//})
//Service.Actor.Get(obj, function (data) {
//    $(".infoArea").html("").append(alert);
//})