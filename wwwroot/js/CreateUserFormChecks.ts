const firstName = document.getElementById("firstName") as HTMLInputElement,
    lastName = document.getElementById("lastName") as HTMLInputElement,
    userName = document.getElementById("userName") as HTMLInputElement,
    password = document.getElementById("password") as HTMLInputElement,
    confirmPassword = document.getElementById("confirmPassword") as HTMLInputElement,
    email = document.getElementById("email") as HTMLInputElement,
    submit = document.getElementById("submit") as HTMLInputElement,
    clear = document.getElementById("clear") as HTMLInputElement;

const clearFields = (): void => {
    firstName.value = "";
    lastName.value = "";
    userName.value = "";
    password.value = "";
    confirmPassword.value = "";
    email.value = "";
}

const passwordsMatch = (): boolean => {
    if (password.value === confirmPassword.value) {
        return true;
    }
    return false;
}

clear.addEventListener("click", clearFields);
confirmPassword.addEventListener("focusin", passwordsMatch);