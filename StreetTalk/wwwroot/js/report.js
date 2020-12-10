async function reportPost(postId) {
    let data = await apiService.reportPost(postId)
    console.log(data);
    if (!data.succes) {
        showerrorpopup("Foutmelding", data.error);
    }
    else {
        document.getElementById(`reportbutton-${postId}`).disabled = true;
    }
}