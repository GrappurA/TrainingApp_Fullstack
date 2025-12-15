const registerButton = document.getElementById("register-button");
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
              headers: { "Content-Type": "application/json" },
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
       else
       {
              registerButton.disabled = false;
              
       }

};