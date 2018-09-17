function OnSuccessAddComment() {

    this.reset();
    $('#usercomment').val(''); //Modified, put it AFTER reset()
}

$("#showAllComments").click(function () {

    $("#showAllComments").hide();
    $("#allComments").removeAttr("hidden");

})