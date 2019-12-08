var postType = document.getElementById("postTypeField").value;

function HandlePostTypes() {
    postType = document.getElementById("postTypeField").value;

    if (postType == "Picture") {
        document.getElementById("urlField").style.display = "block";
    }
    else {
        document.getElementById("urlField").style.display = "none";
    }
}