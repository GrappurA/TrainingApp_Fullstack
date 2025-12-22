container.addEventListener("click", async (e) => {
    if (e.target.classList.contains("edit-button")) {
        try {
            const itemToEdit = e.target.closest(".latest-trainings-item");
            const idToEdit = itemToEdit.dataset.trainingId;

            const overlay = document.getElementById("edit-overlay");
            const editMenu = document.getElementById("edit-menu");
            const editSubmitButton = document.getElementById("edit-submit-button");

            makeVisible();
            overlay.addEventListener("click", function (e) {
                if (e.target === this)
                    makeInvisible();
            });

            function makeInvisible() {
                overlay.classList.add("invisible");
                editMenu.classList.add("invisible");
            }

            function makeVisible() {
                overlay.classList.remove("invisible");
                editMenu.classList.remove("invisible");
            }

            editSubmitButton.onclick = async (event) => {
                try {
                    event.preventDefault();

                    const durationVal = document.getElementById("edit-duration").value;
                    const dateVal = document.getElementById("edit-date").value;

                    itemToEdit.dataset.name = document.getElementById("edit-name").value;
                    itemToEdit.dataset.dateTime = dateVal;
                    itemToEdit.dataset.duration = durationVal;
                    itemToEdit.dataset.description = document.getElementById("edit-description").value;

                    const newTraining = {
                        Id: parseInt(idToEdit),
                        Name:  document.getElementById("edit-name").value,//document.getElementById("edit-name").value,
                        Duration: parseInt(document.getElementById("edit-duration").value),//parseInt(document.getElementById("edit-duration").value),
                        DateTime: document.getElementById("edit-date").value,//document.getElementById("edit-date").value,
                        Description: document.getElementById("edit-description").value,
                        Calories: 0
                    };

                    try {
                        const itemToEdit = e.target.closest(".latest-trainings-item");
                        const idToEdit = itemToEdit.dataset.trainingId;

                        const url = `http://localhost:5042/api/training/patchtraining/${idToEdit}`;
                        const response = await fetch(url, {
                            method: "PATCH",
                            headers: {
                                "Content-Type": "application/json",
                                "Authorization": `Bearer ${token}`
                            },
                            body: JSON.stringify(newTraining)

                        });

                        if (!response.ok)
                            //alert("failed to update training");
                            alert(response.statusText)

                    } catch (error) {
                        console.log(error);
                    }

                    makeInvisible();
                    updateUI(itemToEdit, newTraining);

                    async function updateUI(itemToEdit, newTraining) {
                        itemToEdit.innerHTML = `
                <div class="crud-buttons ">
          <button id="edit-button"
            class="edit-button w-10 h-10 rounded-2xl border-3 hover:scale-110 transition-transform duration-200"><img
              src="img/edit.png" alt="edit_button" class="p-1 w-[100px] h-auto edit-button"></button>
          <button id="delete-button"
            class="delete-button w-10 h-10 rounded-2xl border-3 hover:scale-110 transition-transform duration-200"><img
              src="img/delete.png" alt="delete_button" class="p-1 w-[100px] h-auto delete-button"></button>
          <button id="more-button"
            class="w-10 h-10 rounded-2xl border-3 hover:scale-110 transition-transform duration-200"><img
              src="img/more.png" alt="delete_button" class="p-1 w-[100px] h-auto more-button"></button>
        </div>
         <h3 class="text-lg font-semibold">${newTraining.name} ID: №${newTraining.trainingId}</h3>
         <hr class="border-2">
    <p>${newTraining.dateTime}</p>
    <hr class="border-2">
    <p>${newTraining.duration} min</p> 
    <hr class="border-2">
    <p>${newTraining.description}</p>
                `;
                    }

                } catch (err) {
                    alert(err);
                }

            }

        } catch (err) {

        }
    }
});