const loginButton = document.getElementById("login-button");
loginButton.onclick = async (event) => {
    event.preventDefault();

    const userLoginCreds = {
        Login: document.getElementById("login-email").value,
        Password: document.getElementById("login-password").value,
    };

    try {
        const response = await fetch("http://localhost:5042/api/user/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(userLoginCreds)
        });

        if (!response.ok) {
            const errorText = await response.text();
            alert(errorText);
        }
        else {

        }

    } catch (error) {
        alert(error.text)
    }

};