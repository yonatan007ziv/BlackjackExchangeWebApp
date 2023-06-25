const loginForm = document.getElementById('loginForm');
const registerForm = document.getElementById('registerForm');
const resultLabel = document.getElementById('resultLabel');

loginForm.addEventListener('submit', submitLoginForm);
registerForm.addEventListener('submit', submitRegisterForm);

function submitLoginForm(event) {
    event.preventDefault();
    const credentials = {
        request: "Login",
        username: document.getElementById("usernameL").value,
        password: document.getElementById("passwordL").value,
        email: "",
        twoFA: 0
    };

    fetch('/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
    })
        .then(response => response.text())
        .then(body => {
            console.log('Response: ' + body);
            if (body === "Success")
                GetArticleSearchHtml();
            else
                resultLabel.textContent = body;
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function submitRegisterForm(event) {
    event.preventDefault();
    const credentials = {
        request: "Register",
        username: document.getElementById("usernameR").value,
        password: document.getElementById("passwordR").value,
        email: document.getElementById("emailR").value,
        twoFA: parseInt(document.getElementById("2faR").value)
    };

    fetch('/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
    })
        .then(response => response.text())
        .then(body => {
            console.log('Response: ' + body);
            if (body === "Success")
                GetArticleSearchHtml();
            else
                resultLabel.textContent = body;
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

async function GetArticleSearchHtml() {
    fetch('ArticleSearcher.html')
        .then(response => response.text())
        .then(htmlContent => {
            document.documentElement.innerHTML = htmlContent;
            const scriptElement = document.createElement('script');
            scriptElement.src = '../JavaScript/news-articles-requests.js';
            document.head.appendChild(scriptElement);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}