const registerButton = document.getElementById("register-button");
const token = localStorage.getItem("token");
if (!token)
       location.replace("./login.html");

registerButton.onclick = async (event) => {

       registerButton.disabled = true;
       event.preventDefault();

       const newUser = {
              login: document.getElementById("account-email").value,
              username: document.getElementById("account-username").value,
              password: document.getElementById("account-password").value

       };
       const response = await fetch(`http://localhost:5042/api/user/register`, {
              method: "POST",
              headers: {
                     "Content-Type": "application/json",
                     "Authorization": `Bearer ${token}`,
              },
              body: JSON.stringify(newUser)
       });

       if (!response.ok) {
              const errorText = await response.text();
              if (response.status == 400 && errorText == "User already exists!") {
                     alert("nice!")
              }
              alert(errorText);
              registerButton.disabled = false;
       }
       else {
              registerButton.disabled = false;
              //
              const userLoginCreds = {
                     Login: newUser.login,
                     Password: newUser.password,
              };

              try {
                     const response = await fetch("http://localhost:5042/api/user/login", {
                            method: "POST",
                            headers: { "Content-Type": "application/json" },
                            body: JSON.stringify(userLoginCreds)
                     });

                     if (!response.ok) {
                            const errorText = await response.text();
                            if (errorText.length > 30)
                                   alert("Error during log in!")
                            alert(errorText);
                     }
                     else {
                            //successful login
                            const data = await response.json();
                            localStorage.setItem("token", data.token);
                            location.replace("./index.html");
                     }

              } catch (error) {
                     if (error.text.length > 30)
                            alert("Invalid Operation");
                     alert(error.text)
              }
              //
              const section = document.getElementById("account-section");
              // section.dataset.currentUser =  
              //continue here, what comes after the registration
       }

};