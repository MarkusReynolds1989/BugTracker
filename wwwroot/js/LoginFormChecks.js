// Username must contain a character as the first part but the rest can be numbers + letters as they like, no symbols.
var regUsername = /[A-Za-z][A-Za-z0-9]+/;
// Password can contain any combo of letters and numbers but also must contain symbols.
// We can reuse all this code when we go over to creating users.
var regPassword = /[A-Za-z0-9]+[*\\%#@]/;
var userName = document.getElementById("username");
var password = document.getElementById("password");
var feedback = document.getElementById("loginFeedback");
var submit = document.getElementById("submit");
var verifyUsername = function () {
    if (regUsername.test(userName.value)) {
        feedback.hidden = true;
        password.style.backgroundColor = "White";
        return true;
    }
    userName.style.backgroundColor = "Red";
    feedback.hidden = false;
    feedback.innerText = "Incorrect username";
    return false;
};
var verifyPassword = function () {
    if (regPassword.test(password.value)) {
        feedback.hidden = true;
        password.style.backgroundColor = "White";
        return true;
    }
    userName.style.backgroundColor = "Red";
    feedback.hidden = false;
    feedback.innerText = "Incorrect password";
    return false;
};
if (!verifyUsername() || !verifyPassword()) {
    submit.disabled = true;
}
