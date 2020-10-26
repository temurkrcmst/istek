var uri = 'api/values/UserAdd';
var Service = (function (m) {
    m.index = { 
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
        m.Movies = {
        Add: function (obj,cb) {
          
            $.ajax({
                type: 'POST',
                data: JSON.stringify(obj),
                dataType: "json",
                contentType: "application/json",
                url: '/MP/api/v1/movie/AddMovie'
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
                url: '/MP/api/v1/movie/DeleteMovie'
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
                url: '/MP/api/v1/movie/UptadeMovie'
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
                url: '/MP/api/v1/movie/GetMovie'
            }).success(function (data) {
                cb(data)
            });



        },
    }
    return m;
}(Service || {}))



       




//var obj = {
//    Id: 1,
//    MovieName: "It",
//    MovieType: "Korku",
//    Time: 120,
//    PublishedDate: '01.08.2018',
//    Actors: "Temur",
//    Country: "TR",
//    Company: "Anonim",
//    Scenario: "Anonim",
//    IMDB: 9.8

//};
//var deleteObj = {
//    FilterCol: "Id",
//    FilterVal: ""
//};
//var sendObj = {
//    FilterCol: "Id",
//    FilterVal: "",
    
//    MovieName: "",
//    MovieType: "",
//    Time: ,
//    PublishedDate: '',
//    Actors: "",
//    Country: "",
//    Company: "",
//    Scenario: "",
//    IMDB: 
//}
$(document).ready(function () {
    $("#AddMovieSave").click(function () {
        var moviename = $("#MovieName").val();
        var movietype = $("#MovieType").val();
        var time = $("#Time").val();
        var publisheddate = $("#PublishedDate").val();
        var actors = $("#Actors").val();
        var country = $("#Country").val();
        var company = $("#Company").val();
        var scenario = $("#Scenario").val();
        var ımdb = $("#IMDB").val();

        var obj = {
            Id:1,
            MovieName: moviename,
            MovieType: movietype,
            Time: time,
            PublishedDate: publisheddate,
            Actors: actors,
            Country: country,
            Company: company,
            Scenario: scenario,
            IMDB: ımdb

        };

        Service.Movies.Add(obj, function (data) {
            var al = $('<div class="alert alert-success">' + moviename + ' Başarıyla eklendi..</div>');

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
    $("#MovieUptade").click(function () {
        var moviename = $("#MovieName").val();
        var movietype = $("#MovieType").val();
        var time = $("#Time").val();
        var publisheddate = $("#PublishedDate").val();
        var actors = $("#Actors").val();
        var country = $("#Country").val();
        var company = $("#Company").val();
        var scenario = $("#Scenario").val();
        var ımdb = $("#IMDB").val();

        var sendObj = {
            FilterCol: "MovieID",
            FilterVal: GetID,
            MovieName: moviename,
            MovieType: movietype,
            Time: time,
            PublishedDate: publisheddate,
            Actors: actors,
            Country: country,
            Company: company,
            Scenario: scenario,
            IMDB: ımdb
        };
        Service.Movies.Update(sendObj, function (data) {
            var alert = $('<div class="alert alert-success">' + moviename + ' Başarıyla Güncellendi..</div>');

            if (data.Type == 0) {
                $(alert).html("Kayıt güncelleme esnasında hata oluştu");
                $(alert).removeClass("alert-success");
                $(alert).addClass("alert-danger");
            }

            $(".infoArea").html("").append(alert);


        });
    });
    $("#MovieDelete").click(function () {
        var deleteObj = {
            FilterCol: "MovieID",
            FilterVal: GetID
        };

        Service.Movies.Remove(deleteObj, function (data) {
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


//Service.Movies.Add(obj, function(data){
//    $(".infoArea").html("").append(alert);
//});
//Service.Movies.Remove(deleteObj, function (data) {
//    $(".infoArea").html("").append(alert);
//})
//Service.Movies.Update(sendObj, function (data) {
//    $(".infoArea").html("").append(alert);
//})
//Service.Movies.Get(obj, function (data) {
//    $(".infoArea").html("").append(alert);
//})



//$(document).ready(function (obj) {
//$.ajax({
//    type: 'POST',
//    data: JSON.stringify(obj),
//    dataType: "json",
//    contentType: "application/json",
//    url: 'api/products'
//    })

//})
//$(document).ready(function () {
//    // Send an AJAX request
//    $.getJSON(uri)
//        .done(function (data) {
//            // On success, 'data' contains a list of products.
//            $.each(data, function (key, item) {
//                // Add a list item for the product.
//                $('<li>', { text: formatItem(item) }).appendTo($('#products'));
//            });
//        });
//});



//function formatItem(item) {
//    return item.MovieName + ': ' + item.MovieType + ' :' + item.Time + ' Dakikadır.';
//}

//function find() {
//    var id = $('#prodId').val();
//    $.getJSON(uri + '/' + id)
//        .done(function (data) {
//            $('#product').text(formatItem(data));
//        })
//        .fail(function (jqXHR, textStatus, err) {
//            $('#product').text('Error: ' + err);
//        });
//}