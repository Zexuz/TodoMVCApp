// Write your Javascript code.
$(document).ready(function () {
    // the "href" attribute of .modal-trigger must specify the modal ID that wants to be triggered

    updateEventHandlers();

    // $("a.label-edit").click(function () {
    //     var label = $(this).siblings("label").first();
    //     setTimeout(function() {
    //         label.focus();
    //     }, 0);
    // })

});

function updateEventHandlers() {
    $('.modal').modal();

    $("input[type=checkbox]").click(syncCheckboxState);
    $("#add-new-checklistbutton").click(addNewChecklistItem);


    $(".checkbox-listener").change(checkboxChanged);
    $("article.checklist h2.title").blur(checkboxChanged);
    $("article.checklist h2.title").keyup(syncCheckboxTitle);

    $("label.item-name").blur(checkboxChanged);
    $("label.item-name").keyup(syncCheckboxItemTitle);
    $("label.item-name").keypress(preventEnterAndGoToNext);
    $("label.item-name").click(preventDefault)


    $("article.note h2.title").keyup(syncNoteTitle);
    $("article.note h2.title").blur(noteChanged);
    $("article.note h2.title").keypress(preventEnterAndGoToNext);
    $("article.note div.note").keyup(syncNoteText);
    $("article.note div.note").blur(noteChanged);

    $("a.remove-checklist-item").click(removeCheckListItem);

    $("a#create-new-note").click(createNewNote);
    $("a#create-new-checklist").click(createNewChecklist);

}

function createNewNote() {
    sendCreateNote(function (res) {
        //todo add it to the ui
    });
}

function createNewChecklist() {
    sendCreateNewChecklist(function (res) {
        //todo add it to the ui
    })
}
function removeCheckListItem() {
    var ele = $(this).closest("article.checklist");
    var checkListId = ele.data("id");
    var checkListItemId = $(this).data("id");

    var divsToRemove = ele.find('input[data-id='+checkListItemId+']');

    sendDeleteCheckboxRequest(checkListId, checkListItemId, function () {

        for (var i = 0; i < divsToRemove.length; i++) {
            var obj = $(divsToRemove[i]);
            obj.parent().remove();

        }
    })
}

function preventDefault(e) {
    var offset = e.offsetX;
    if (offset >= 35)
        e.preventDefault();
}

function addNewChecklistItem() {
    var ele = $(this).closest("article.checklist");
    var checkListId = ele.data("id");
    sendCreateNewChecListItem(checkListId, function (newId) {

        var newCheckListDiv = $("<div></div>");
        var newCheckListInput = $("<input/>");
        var newCheckListLabel = $("<label></label>");
        var newRemove = $("<a></a>");
        var newI = $(' <i class="right material-icons close">close</i>');


        newRemove.attr("href","#!");
        newRemove.attr("data-id",newId);
        newRemove.attr("class","remove-checklist-item");
        newRemove.append(newI);

        newRemove.click(removeCheckListItem);


        newCheckListInput.attr("type", "checkbox");
        newCheckListInput.attr("data-id", newId);
        newCheckListInput.attr("data-checklist-id", checkListId);
        newCheckListInput.attr("id", "indeterminate-checkbox-modal-" + newId);
        newCheckListInput.attr("class", ".checkbox-listener id-nr-" + newId);

        newCheckListInput.click(syncCheckboxState);
        newCheckListInput.change(checkboxChanged);

        newCheckListLabel.attr("data-id", newId);
        newCheckListLabel.attr("class", "item-name");
        newCheckListLabel.attr("contenteditable", "true");
        newCheckListLabel.attr("for", "indeterminate-checkbox-modal-" + newId);
        newCheckListLabel.text("Enter text here");

        newCheckListLabel.click(preventDefault);
        newCheckListLabel.keyup(syncCheckboxItemTitle);
        newCheckListLabel.blur(checkboxChanged);
        newCheckListLabel.keyup(syncCheckboxItemTitle);


        newCheckListDiv.append(newCheckListInput);
        newCheckListDiv.append(newCheckListLabel);
        newCheckListDiv.append(newRemove);


        var checkListContainer = $("#checkListItems");
        checkListContainer.append(newCheckListDiv.clone(true,true));
        var checkListContainerModal = $("#checkListItemsModal");
        checkListContainerModal.append(newCheckListDiv.clone(true,true));

        // $("label.item-name").blur(checkboxChanged);
        // $(".checkbox-listener").change(checkboxChanged);

    });
}

function preventEnterAndGoToNext(e) {
    if (e.which === 13) {
        // $(this).next().focus();
        return false;
    }
    return true;
}

function syncCheckboxItemTitle() {
    var ele = $(this).closest("article.checklist");
    var myId = $(this).data("id");
    var newText = $(this).text();
    ele.find("label").each(function (i, e) {
        if ($(e).text() === newText) {
            console.log("samne");
            return;
        }
        var eleId = $(e).data("id");
        if (myId === eleId)
            $(e).text(newText);
    });
}

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

function sendCreateNewChecListItem(id, callback) {

    $.ajax({
        url: '/todo/AddCheckbox?checkListId=' + id,
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        traditional: true,
        success: function (result) {
            console.log(result);
            callback(result.value.id)
        }
    });
}

function sendDeleteCheckboxRequest(checkListId, checkListItemId, callback) {

    $.ajax({
        url: '/todo/DeleteCheckbox?checkListId=' + checkListId + "&checkListItemId=" + checkListItemId,
        type: 'DELETE',
        success: function (result) {
            callback();
        }
    });
}


function sendCreateNewChecklist(callback) {

    $.ajax({
        url: '/todo/CreateChecklist',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        traditional: true,
        success: function (result) {
            console.log(result);
            callback(result)
        }
    });
}

function sendCreateNote(callback) {

    $.ajax({
        url: '/todo/CreateNote',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        traditional: true,
        success: function (result) {
            console.log(result);
            callback(result)
        }
    });
}