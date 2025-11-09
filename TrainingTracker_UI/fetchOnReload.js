document.onload = loadData;

async function loadData() {
    /*
     //testing
    const test_trainings = [
        {
            name: "Morning Cardio",
            duration: 45,
            dateTime: "2025-10-28T07:30:00",
            description: "High-intensity interval running and cycling to boost endurance."
        },
        {
            name: "Strength Training",
            duration: 60,
            dateTime: "2025-10-28T18:00:00",
            description: "Full-body weight training focusing on compound lifts."
        },
        {
            name: "Yoga Session",
            duration: 50,
            dateTime: "2025-10-29T08:15:00",
            description: "Flexibility and mindfulness training to improve recovery and balance."
        },
        {
            name: "Kickboxing Practice",
            duration: 70,
            dateTime: "2025-10-30T17:00:00",
            description: "Punch, kick, and combo drills to enhance coordination and power."
        },
        {
            name: "Evening Stretch",
            duration: 30,
            Date: "2025-10-31T21:00:00",
            description: "Relaxed stretching and mobility exercises before bedtime."
        }
    ];

    for (test_training of test_trainings) {
        addTrainingToUI(test_trainings);
    }    */

    try {
        const response = await fetch("http://localhost:5042/api/training/gettraining");
        if (!response.ok)
            throw new Error("Failed to fetch data on page load.");

        const saved = await response.json();
        saved.forEach(el => {
            addTrainingToUI(el);
        });

    } catch (err) {
        console.log(err);
        alert(`Error loading trainings: ${err}`);
    }
}