// Username must contain a character as the first part but the rest can be numbers + letters as they like, no symbols.
const regUsername: RegExp = /[A-Za-z][A-Za-z0-9]+/;
// Password can contain any combo of letters and numbers but also must contain symbols.
// We can reuse all this code when we go over to creating users.
const regPassword: RegExp = /[A-Za-z0-9]+[*\\%#@]/;

let userName = document.getElementById("username") as HTMLInputElement;
let password = document.getElementById("password") as HTMLInputElement;
let feedback = document.getElementById("loginFeedback") as HTMLInputElement;
let submit = document.getElementById("submit") as HTMLInputElement;

const verifyUsername = (): boolean => {
    if (regUsername.test(userName.value)) {
        feedback.hidden = true;
        password.style.backgroundColor = "White";
        return true;
    }

    userName.style.backgroundColor = "Red";
    feedback.hidden = false;
    feedback.innerText = "Incorrect username";
    return false;
}

const verifyPassword = (): boolean => {
    if (regPassword.test(password.value)) {
        feedback.hidden = true;
        password.style.backgroundColor = "White";
        return true;
    }

    userName.style.backgroundColor = "Red";
    feedback.hidden = false;
    feedback.innerText = "Incorrect password";
    return false;
}

if (!verifyUsername() || !verifyPassword()) {
    submit.disabled = true;
}