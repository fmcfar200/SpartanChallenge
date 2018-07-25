var currentList = {};
var searchList = {};


$(document).ready(function () {
    console.info("ready")

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "api/Equipment",
        success: function (result) {
            console.info("Success");
            currentList = result;

            if (result != null)
            {
                showItems();
            }
        }
    });


    $("#searchBarId").keyup(function (event) {
        if (event.keyCode == 13)
        {
            var idQuery = $("#searchBarId").val();
            search(idQuery);
        }
     })
});

function showItems()
{
    var $list = $("#equipmentListItems").empty();

    for (var i = 0; i < currentList.length; i++)
    {
        var item = currentList[i];
        var $li = $("<li>").html(item.UnitId + "<br />" + item.ItemId + "<br />" + item.Description);


        $li.appendTo($list);

    }
}

function search(query)
{
    var theQuery = JSON.stringify(query);
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/Equipment",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(query),
        success: function (result) {
            console.info(result);

            currentList = result;
            showItems();
        }
    });

}