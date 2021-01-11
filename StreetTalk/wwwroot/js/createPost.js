async function checkTitleSimilarity(title) {
    hideSimilarTitleWarning();
    let result = await fetch(`/PublicPost/CheckPostTitleSimilarity?title=${title}`);
    
    if(result.status === 200) {
        let data = await result.json();
        showSimilarTitleWarning(data.title);
    }
}

function showSimilarTitleWarning(title) {
    let element = document.getElementById("similarTitleWarning");
    element.classList.remove("d-none");
    element.textContent = `Uw titel lijkt op die van een bestaande melding: "${title}"`;
}

function hideSimilarTitleWarning() {
    let element = document.getElementById("similarTitleWarning");
    element.classList.add("d-none");
}