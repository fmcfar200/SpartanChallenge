var currentList = {};

$(document).ready(function () {
    console.info("ready")

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "api/Equipment/",
        success: function (result) {
            console.info("Success");
            currentList = result;

            if (result != null)
            {
                showItems();
            }
        }
    });
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