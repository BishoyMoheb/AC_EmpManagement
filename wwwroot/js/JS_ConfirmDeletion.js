function ConfirmDelete(unique_UserID, IsClicked) {
    var DelRequest = 'DeleteRequest_' + unique_UserID;
    var DelConfirmation = 'DeleteConfirmation_' + unique_UserID;
    if (IsClicked) {
        $('#' + DelRequest).hide();
        $('#' + DelConfirmation).show();
    }
    else {
        $('#' + DelRequest).show();
        $('#' + DelConfirmation).hide();
    }
}