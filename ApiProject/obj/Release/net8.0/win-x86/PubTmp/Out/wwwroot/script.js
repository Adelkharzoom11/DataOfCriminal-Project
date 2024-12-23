document.getElementById("criminalForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const form = event.target;
    const submitButton = form.querySelector('button[type="submit"]');
    const originalButtonText = submitButton.textContent;

    // ÅäÔÇÁ FormData ãä ÇáäãæÐÌ
    const formData = new FormData(form);

    // ÊÍÞÞ ãä ÍÞá ÇáÇÓã: íÌÈ Ãä íßæä ãæÌæÏðÇ
    if (!formData.get("nameCriminal")) {
        alert("ÇÓã ÇáãÌÑã ãØáæÈ");
        return;
    }

    // ÇáÍÞæá ÇáÊí íãßä Ãä Êßæä ÝÇÑÛÉ æíÌÈ ãáÄåÇ ÈÇáÞíã ÇáÇÝÊÑÇÖíÉ
    const fields = [
        "nickName",
        "fatherName",
        "MotherName",
        "Adjective",
        "phoneNumberCriminal",
        "facebookCriminalURL",
        "DescriptionOfCriminal",
        "Address.Governorate",
        "Address.city",
        "Address.village",
        "Address.district"
    ];

    fields.forEach(field => {
        if (!formData.get(field)) {
            formData.set(field, "string"); // ÊÚííä ÇáÞíãÉ ÇáÇÝÊÑÇÖíÉ "string" ÅÐÇ ßÇäÊ ÇáÍÞæá ÝÇÑÛÉ
        }
    });

    // ÊÍÞÞ ãä ÇáÈÑíÏ ÇáÅáßÊÑæäí: ÅÐÇ áã íÊã ÅÏÎÇáå¡ ÓíÊã ÊÚííä "string@gmail.com"
    if (!formData.get("personalEmail")) {
        formData.set("personalEmail", "astring@gmail.com");
    }

    // ÊÍÞÞ ãä ÇáÕæÑÉ: ÅÐÇ áã íÊã ÊÍãíáåÇ¡ ÓíÊã ÊÚííä ÕæÑÉ ÇÝÊÑÇÖíÉ string.png
    const imageField = formData.get("imageCriminal");
    if (!imageField) {
        // Þã ÈÊÍãíá ÕæÑÉ ÇÝÊÑÇÖíÉ ãæÌæÏÉ Ýí ÇáãÓÇÑ wwwroot/Images/string.png
        const defaultImagePath = "/Images/string.png";  // ÇáãÓÇÑ ÇáÕÍíÍ ááÕæÑÉ Ýí ÇáÎÇÏã
        const defaultImage = await fetch(defaultImagePath)
            .then(response => response.blob())  // ÇáÍÕæá Úáì ÇáÕæÑÉ ßÜ Blob
            .then(blob => new File([blob], "string.png", { type: "image/png" })); // ÊÍæíáåÇ Åáì File

        formData.set("imageCriminal", defaultImage);  // ÊÚííä ÇáÕæÑÉ Ýí FormData
    }

    try {
        setLoadingState(submitButton, true);

        // ÅÑÓÇá ÇáÈíÇäÇÊ ÈÇÓÊÎÏÇã FormData
        const response = await fetch("/api/Criminal", {
            method: 'POST',
            body: formData,
        });

        const result = await response.json();

        if (response.ok) {
            showSuccessMessage(result.message || "ÊãÊ ÅÖÇÝÉ ÇáÈíÇäÇÊ ÈäÌÇÍ!");
            alert(result.message || "ÊãÊ ÅÖÇÝÉ ÇáÈíÇäÇÊ ÈäÌÇÍ!");
            form.reset();
        } else {
            showErrorMessage(result.message || "An error occurred while adding data.");
        }
    } catch (error) {
        showErrorMessage("An error occurred while connecting to the server: " + error.message);
    } finally {
        setLoadingState(submitButton, false, originalButtonText);
    }
});

function setLoadingState(button, isLoading, originalText = "") {
    const spinner = button.querySelector(".spinner");
    const buttonText = button.querySelector("span");

    if (isLoading) {
        button.disabled = true;
        buttonText.textContent = "Sending...";
        spinner.style.display = "inline-block";
    } else {
        button.disabled = false;
        buttonText.textContent = originalText;
        spinner.style.display = "none";
    }
}

function showSuccessMessage(message) {
    const messageBox = createMessageElement(message, "success");
    document.body.appendChild(messageBox);

    setTimeout(() => {
        messageBox.remove();
    }, 3000);
}

function showErrorMessage(message) {
    const messageBox = createMessageElement(message, "error");
    document.body.appendChild(messageBox);

    setTimeout(() => {
        messageBox.remove();
    }, 5000);
}

function createMessageElement(message, type) {
    const messageBox = document.createElement("div");
    messageBox.className = type === "success" ? "success-message" : "error-message";
    messageBox.textContent = message;
    return messageBox;
}
