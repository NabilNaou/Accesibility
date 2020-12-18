window.addEventListener("load", () => {
    document.querySelector(".hamburger").addEventListener("click", () => 
        document.querySelector(".st-navbar").classList.toggle("expanded")
    );
    
    /* This compensates for the height of the address bar in mobile browsers */
    window.addEventListener("resize", resizeNavbar);
    resizeNavbar();
    
    function resizeNavbar() {
        let vh = window.innerHeight * 0.01;
        let navbar = document.querySelector(".st-navbar");
        navbar.style.setProperty('--vh', `${vh}px`);
    }
})