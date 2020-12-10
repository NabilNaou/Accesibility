(() => {
    let btn = document.getElementById('filter-button')
    
    if(btn) {
        btn.addEventListener('click', () => {
            document.querySelector('#filters').classList.toggle('show');
        })
    }
})()