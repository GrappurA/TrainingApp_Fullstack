const container = document.getElementById("training-items");

container.addEventListener("click", async (e) => {
    if (e.target.classList.contains("delete-button")) {
        try {
            const itemToRemove = e.target.closest(".latest-trainings-item");
            const idToRemove = itemToRemove.dataset.trainingId;

            const url = `http://localhost:5042/api/training/deletetraining/${idToRemove}`;
            await fetch(url, {
                method: "DELETE"
            });

            itemToRemove.classList.add('is-deleting');

            // 2. Listen for the animation to finish
            itemToRemove.addEventListener('transitionend', () => {
                // 3. NOW remove the element from the DOM
                itemToRemove.remove();
            }, { once: true });
        }
        catch (err) {
            console.error(err);
            alert(`Error adding training: ${err}`);
        }

    }
});
