function showerrorpopup(title, message) {
    let titleelement = document.getElementById("errorpopuptitle")
    let messageelement = document.getElementById("errorpopupmessage")

    titleelement.textContent = title
    messageelement.textContent = message

    $('#errorpopup').modal('show')
}