// Step 1: Gather the elements of the page.
const userName = document.getElementById("username") as HTMLInputElement,
    password = document.getElementById("password") as HTMLInputElement,
    feedback = document.getElementById("loginFeedback") as HTMLInputElement,
    clearButton = document.getElementById("clear");

let submitButton = document.getElementById("submit") as HTMLInputElement,
    passwordIsEmpty: boolean = true,
    usernameIsEmpty: boolean = true;

// Step 2: Create our functions.

// Clears username and password.
const clear = () => {
    userName.value = "";
    password.value = "";
}

const checkInput = () => {
    usernameIsEmpty = userName.value.length <= 0;
    passwordIsEmpty = password.value.length <= 0;
    if (!usernameIsEmpty && !passwordIsEmpty) {
        submitButton.disabled = false;
    }

    if (usernameIsEmpty || passwordIsEmpty) {
        submitButton.disabled = true;
    }
}

// Step 3: Add event listeners.
clearButton.addEventListener("click", clear);
password.addEventListener("input", checkInput);
userName.addEventListener("input", checkInput);