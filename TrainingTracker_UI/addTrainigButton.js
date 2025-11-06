const form = document.getElementById("training-form");
const trainingList = document.getElementById("latest-trainings-list");

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const training = {
        name: document.getElementById("name").value,
        duration: parseInt(document.getElementById("duration").value),
        dateTime: document.getElementById("date").value,
        description: document.getElementById("description").value,
    };

    try {
        const response = await fetch("http://localhost:5042/api/training/posttraining", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(training)
        });
        if (!response.ok)
            throw new Error("Failed to add training" + response.error);

        const saved = await response.json();
        addTrainingToUI(saved);
        form.reset();

    }
    catch (err) {
        console.error(err);
        alert(`Error adding training: ${err}`);
    }

});

function addTrainingToUI(training) {
    const div = document.createElement("div");
    div.classList.add("latest-trainings-item");
    div.innerHTML = `<div class="crud-buttons grid grid-cols-3 grid-rows-1 gap-5">
          <button id="edit-button"
            class="w-10 h-10 rounded-2xl border-2 hover:scale-110 transition-transform duration-200"><img
              src="img/edit.png" alt="edit_button" class="p-1 w-[100px] h-auto"></button>
          <button id="delete-button"
            class="w-10 h-10 rounded-2xl border-2 hover:scale-110 transition-transform duration-200"><img
              src="img/delete.png" alt="delete_button" class="p-1 w-[100px] h-auto"></button>
          <button id="more-button"
            class="w-10 h-10 rounded-2xl border-2 hover:scale-110 transition-transform duration-200"><img
              src="img/more.png" alt="delete_button" class="p-1 w-[100px] h-auto"></button>
        </div>
         <h3 class="text-lg font-semibold">${training.name}</h3>
         <hr class="border-2">
    <p>${training.dateTime}</p>
    <hr class="border-2">
    <p>${training.duration} min</p>
    <hr class="border-2">
    <p>${training.description}</p>
        `;
    trainingList.append(div);
}