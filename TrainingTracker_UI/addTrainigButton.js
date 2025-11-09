const form = document.getElementById("training-form");
const trainingItems = document.getElementById("training-items");

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
    div.innerHTML = `<div class="crud-buttons ">
          <button id="edit-button"
            class="w-10 h-10 rounded-2xl border-3 hover:scale-110 transition-transform duration-200"><img
              src="img/edit.png" alt="edit_button" class="p-1 w-[100px] h-auto edit-button"></button>
          <button id="delete-button"
            class="delete-button w-10 h-10 rounded-2xl border-3 hover:scale-110 transition-transform duration-200"><img
              src="img/delete.png" alt="delete_button" class="p-1 w-[100px] h-auto delete-button"></button>
          <button id="more-button"
            class="w-10 h-10 rounded-2xl border-3 hover:scale-110 transition-transform duration-200"><img
              src="img/more.png" alt="delete_button" class="p-1 w-[100px] h-auto more-button"></button>
        </div>
         <h3 class="text-lg font-semibold">${training.name} ID: №${training.trainingId}</h3>
         <hr class="border-2">
    <p>${training.dateTime}</p>
    <hr class="border-2">
    <p>${training.duration} min</p> 
    <hr class="border-2">
    <p>${training.description}</p>   
    `;

    div.dataset.trainingId = training.trainingId;
    div.classList.add('is-entering');

    trainingItems.append(div);

    // 2. Use requestAnimationFrame to ensure the browser has painted
    //    the 'is-entering' state before applying 'entered'.
    //    This makes the transition visible.
    requestAnimationFrame(() => {
        div.classList.add('entered');
        // Optional: Remove the classes after the animation to keep the DOM clean
        // You could also listen for 'transitionend' like with deletion
        div.addEventListener('transitionend', () => {
            div.classList.remove('is-entering', 'entered');
        }, { once: true });
    });
}