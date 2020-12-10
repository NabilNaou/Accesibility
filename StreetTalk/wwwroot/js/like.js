
async function likePost(postId) {
    let data = await apiService.likePost(postId)

    if (data.succes) {
        let likes = data.newLikes || "?"
        let likeCounter = document.getElementById(`likecounter-${postId}`)
        let likeButton = document.getElementById(`likebutton-${postId}`).firstElementChild

        likeCounter.textContent = "+" + likes
        likeButton.classList.toggle("likebuttonpressed")
    }
    else {
        showerrorpopup("Foutmelding", data.error)
    }
}