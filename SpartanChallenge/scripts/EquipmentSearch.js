var currentList = {};
var searchList = {};

var searchType;

$(document).ready(function () {
    console.info("ready")

    searchType = 0;
    selectSearchType(searchType);

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

function selectSearchType(type)
{
    var unitButton = $("#btnUnitNoId");
    var typeButton = $("#btnTypeId");

    switch(type)
    {
        case 0:
            unitButton.css('background-color', 'red');
            typeButton.css('background-color', 'white');
            searchType = 0;

            console.info(searchType);
            break;

        case 1:
            unitButton.css('background-color', 'white');
            typeButton.css('background-color', 'red');
            searchType = 1;

            console.info(searchType);

            break;


    }
}

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
    var searchModel = { sTerm: query, sType: searchType }

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/Equipment",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(searchModel),
        success: function (result) {
            console.info(result);

            currentList = result;
            showItems();
        }
    });

}

/*
function searchByItem(query) {
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
*/