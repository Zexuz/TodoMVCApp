// Write your Javascript code.
$(document).ready(function () {
    // the "href" attribute of .modal-trigger must specify the modal ID that wants to be triggered
    $('.modal').modal();

    $("input[type=checkbox]").click(syncCheckboxState);

    $(".checkbox-listener").change(checkboxChanged);

});

function checkboxChanged() {
        var ele = $(this).closest("article.checklist");

        var checkListJson = [];

        ele.find("input[type=checkbox]").each(function (e, index, array) {
            debugger;
            var checked = $(e).is(":checked");
            var text = $(e).parent().find("label").first().text();
            var id = $(e).data("id");
            checkListJson.push({
                Id: id,
                Checked: checked,
                Text: text
            })
        });

        sendUpdateCheckboxRequest(
            {
                Id: ele.data("checklist-id"),
                CheckList: checkListJson,
                Title: ele.closest("h2.title").text()
            }
        )

}

function syncCheckboxState() {
    var ele = $(this);

    var dataId = ele.data("id");
    var classToLookFor = "id-nr-" + dataId;
    var checked = ele.is(":checked");


    var allInputToChange = $("." + classToLookFor).filter(function (i, e) {
        var innerChecked = $(e).is(":checked");
        return innerChecked !== checked;
    }).toArray();

    console.log(allInputToChange);
    if (allInputToChange.length === 0) return;

    allInputToChange.forEach(function (current, index, array) {
        $(current).prop('checked', checked).change();
    });
}

function sendUpdateCheckboxRequest(data) {

    $.ajax({
        url: './updateCheckbox',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        traditional: true,
        success: function (result) {
        }
    });
}


function sendDeleteRequest(dataId, elementId) {

    $.ajax({
        url: './delete',
        type: 'DELETE',
        data: {id: dataId},
        success: function (result) {
            $("#" + elementId).remove();
        }
    });
}