// Step 1: Gather the elements of the page.
var userName = document.getElementById("username"), password = document.getElementById("password"), feedback = document.getElementById("loginFeedback"), clearButton = document.getElementById("clear");
var submitButton = document.getElementById("submit"), passwordIsEmpty = true, usernameIsEmpty = true;
// Step 2: Create our functions.
// Clears username and password.
var clear = function () {
    userName.value = "";
    password.value = "";
};
var checkInput = function () {
    usernameIsEmpty = userName.value.length <= 0;
    passwordIsEmpty = password.value.length <= 0;
    // TODO: Bug, if we fill out both fields and then clear username submit will still be enabled.
    if (!usernameIsEmpty && !passwordIsEmpty) {
        submitButton.disabled = false;
    }
    if (usernameIsEmpty || passwordIsEmpty) {
        submitButton.disabled = true;
    }
};
// Step 3: Add event listeners.
clearButton.addEventListener("click", clear);
password.addEventListener("input", checkInput);
userName.addEventListener("input", checkInput);
