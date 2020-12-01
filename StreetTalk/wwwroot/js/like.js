
function likepost(postid) {
    fetch("/PublicPost/PostLike/" + postid,
    {
        method: "POST"
    })
        .then(response => response.json())
        .then(data => handleresponse(data, postid));
}

function handleresponse(data, postid) {
    if (data.succes) {
        let likes = data.newLikes
        let likecounter = document.getElementById("likecounter-" + postid)

        likecounter.textContent = "+" + likes
    }
    else {
        showerrorpopup("Foutmelding", data.error)
    }
}