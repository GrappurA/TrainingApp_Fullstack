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
            if(errorText.length > 30)
                alert("Error during log in!")
            alert(errorText);
        }
        else {
            //successful login
            const data = await response.json();
            localStorage.setItem("token" , data.token);
            location.replace("./index.html");
        }

    } catch (error) {
        if(error.text.length > 30)
            alert("Invalid Operation");
        alert(error.text)
    }

};