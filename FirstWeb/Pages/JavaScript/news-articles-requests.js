const apiKey = '66a732e14db345bdb83b189e0925753e';
const listElement = document.getElementById('list');

document.getElementById('searchButton').addEventListener('click', searchByParameters);

function searchByParameters() {
    listElement.innerHTML = '';
    fetch('https://newsapi.org/v2/top-headlines?country=us&apiKey=' + apiKey)
        .then(response => response.json())
        .then(data => {
            const articles = data.articles;
            const lowercaseKeyword = document.getElementById('keywordInput').value.toLowerCase();
            const filteredArticles = articles.filter(article => {
                const lowercaseTitle = (article.title || "").toLowerCase();
                const lowercaseDescription = (article.description || "").toLowerCase();
                const hasKeyword = lowercaseTitle.includes(lowercaseKeyword) || lowercaseDescription.includes(lowercaseKeyword);
                return hasKeyword;
            });

            filteredArticles.forEach(article => {
                const listItem = document.createElement('li');
                const titleElement = document.createElement('div');
                titleElement.textContent = article.title;

                const descriptionElement = document.createElement('div');
                descriptionElement.textContent = article.description;
                descriptionElement.classList.add('hidden');

                listItem.appendChild(titleElement);
                listItem.appendChild(descriptionElement);

                listItem.addEventListener('click', () => {
                    descriptionElement.classList.toggle('hidden');
                });

                listElement.appendChild(listItem);
            });
        })
        .catch(error => {
            console.error(error);
        });
}