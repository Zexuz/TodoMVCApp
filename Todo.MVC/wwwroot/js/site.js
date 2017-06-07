// Write your Javascript code.
$(document).ready(function () {
    // the "href" attribute of .modal-trigger must specify the modal ID that wants to be triggered
    $('.modal').modal();

    $("input[type=checkbox]").click(syncCheckboxState);

    $(".checkbox-listener").change(checkboxChanged);
    $("article.checklist h2.title").blur(checkboxChanged);
    $("article.checklist h2.title").keyup(syncCheckboxTitle);

    $("article.note h2.title").keyup(syncNoteTitle);
    $("article.note h2.title").blur(noteChanged);
    $("article.note div.note").keyup(syncNoteText);
    $("article.note div.note").blur(noteChanged);


    $("label").click(function (e) {
        var offset = e.offsetX;
        if (offset >= 35)
            e.preventDefault();
    })

    // $("a.label-edit").click(function () {
    //     var label = $(this).siblings("label").first();
    //     setTimeout(function() {
    //         label.focus();
    //     }, 0);
    // })

});

function checkboxChanged() {
    var ele = $(this).closest("article.checklist");

    var checkListJson = [];

    ele.find("input[type=checkbox]").each(function (index, e) {
        var id = $(e).data("id");
        if (checkListJson.map(function (p1, p2, p3) {
                return p1.Id
            }).indexOf(id) > -1) return;

        var checked = $(e).is(":checked");
        var siblings = $(e).siblings();
        var label = siblings.first();
        var text = label.text().trim();
        checkListJson.push({
            Id: id,
            Checked: checked,
            Text: text
        })
    });
    var dataToSend = {
        Id: ele.data("id"),
        CheckList: checkListJson,
        Title: ele.find("h2.title.editable").text().trim()
    };
    sendUpdateCheckboxRequest(dataToSend);
}

function noteChanged() {
    var ele = $(this).closest("article.note");

    var dataToSend = {
        Id: ele.data("id"),
        Note: ele.find("div.note.editable").text().trim(),
        Title: ele.find("h2.title.editable").text().trim()
    };
    sendUpdateNoteRequest(dataToSend);
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


function syncCheckboxTitle(event) {
    var ele = $(this).closest("article.checklist");
    var newText = $(this).text();
    ele.find("h2").each(function (i, e) {
        if ($(e).text() === newText) {
            console.log("samne");
            return;
        }
        $(e).text(newText);
    });
}

function syncNoteTitle(event) {
    var ele = $(this).closest("article.note");
    var newText = $(this).text();
    ele.find("h2").each(function (i, e) {
        if ($(e).text() === newText) {
            console.log("samne");
            return;
        }
        $(e).text(newText);
    });
}

function syncNoteText(evnet) {
    var ele = $(this).closest("article.note");
    var newText = $(this).text();
    ele.find("div.note").each(function (i, e) {
        if ($(e).text() === newText) {
            console.log("samne");
            return;
        }
        $(e).text(newText);
    });
}

function sendUpdateCheckboxRequest(data) {

    $.ajax({
        url: '/todo/updateCheckbox',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        traditional: true,
        success: function (result) {
        }
    });
}
function sendUpdateNoteRequest(data) {

    $.ajax({
        url: '/todo/updateNote',
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
        url: '/todo/delete',
        type: 'DELETE',
        data: {id: dataId},
        success: function (result) {
            $("#" + elementId).remove();
        }
    });
}