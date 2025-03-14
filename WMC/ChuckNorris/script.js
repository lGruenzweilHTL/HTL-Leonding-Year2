function getJoke() {
    // Get category
    let category = document.getElementById('category').value;

    // Get joke
    const container = document.getElementById('joke-container');
    container.innerHTML = 'Loading...';
    fetch(`https://api.chucknorris.io/jokes/random?category=${category}`)
        .then(response => response.json())
        .then(data => {
            container.innerHTML = data.value;
        });
}