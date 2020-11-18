window.addEventListener("load", () => {
    document.querySelectorAll(".st-nav-expand").forEach((el) => {
        el.addEventListener("click", () => {
            document.querySelector(".st-navbar").classList.toggle("expanded");
        })
    })
})