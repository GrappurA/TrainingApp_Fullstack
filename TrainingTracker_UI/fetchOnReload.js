document.addEventListener("DOMContentLoaded", async () => {
    const form = document.getElementById("training-form");
    const trainingItems = document.getElementById("training-items");

    const token = localStorage.getItem("token");
    if (!token) {
        location.replace("./login.html");
        return;
    }

    
    try {
        const response = await fetch("http://localhost:5042/api/training/gettraining", {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
        
        if (!response.ok)
            throw new Error("Failed to fetch trainings");
        
        const saved = await response.json();
        saved.forEach(addTrainingToUI);
        
    } catch (err) {
        console.error(err);
        alert("Error loading trainings");
    }
    
    function addTrainingToUI(training) {
        const div = document.createElement("div");
        div.classList.add("latest-trainings-item");

        div.innerHTML = `<div class="crud-buttons">
        <button id="more-button"
          class="w-10 h-10 rounded-2xl border-3 hover:scale-110 transition-transform duration-200 bg-[#206676]"><img
            src="img/more.png" alt="delete_button" class="p-1 w-[100px] h-auto more-button"></button>
          <button id="edit-button"
            class="edit-button w-10 h-10 rounded-2xl border-3 hover:scale-110 transition-transform duration-200 bg-[#FF7F00]"><img
              src="img/edit.png" alt="edit_button" class="p-1 w-[100px] h-auto edit-button"></button>
          <button id="delete-button"
            class="delete-button w-10 h-10 rounded-2xl border-3  hover:scale-110 transition-transform duration-200 bg-[#FF1E00]"><img
              src="img/delete.png" alt="delete_button" class="p-1 w-[100px] h-auto delete-button"></button>
        </div>
         <h3 class="text-lg font-semibold text-[#ffffff]">${training.name} ID: №${training.trainingId}</h3>
          <hr class="border-2 text-[#f09d51]">
    <p class="text-[#ffffff]">${training.dateTime}</p>
    <hr class="border-2 text-[#f09d51]">
    <p class="text-[#ffffff]">${training.duration} min</p> 
     <hr class="border-2 text-[#f09d51]">
    <p class="text-[#ffffff]">${training.description}</p>   
    `;
    
        div.dataset.trainingId = training.trainingId;
        div.dataset.name = training.name;
        div.dataset.dateTime = training.dateTime;
        div.dataset.description = training.description;
        div.dataset.duration = training.duration;
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
});
