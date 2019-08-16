let allUsersBtn = document.getElementById("button1");
let userByIdBtn = document.getElementById("button2");
let checkIfUserAdultBtn = document.getElementById("button3");
let addNewUserBtn = document.getElementById("button4");
let userByIdInput = document.getElementById("inputField2");
let checkAdultInput = document.getElementById("inputField3");
let newUserInput1 = document.getElementById("inputField4fn");
let newUserInput2 = document.getElementById("inputField4ln");
let newUserInput3 = document.getElementById("inputField4Ag");

let port = "58554"

let allUsers = async () => {
    let url = "http://localhost:" + port + "/api/Users";
    console.log(url);
    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
};

let userById = async () => {
    let url = "http://localhost:" + port + "/api/users/" + userByIdInput.value;

    let response = await fetch(url);
    let data = await response.text();
    console.log(data);
};

let checkIfUserAdult = async () => {
    let url = "http://localhost:" + port + "/api/users/age/" + checkAdultInput.value;

    let response = await fetch(url);
    try {
        let data = await response.text();
        console.log(data);
    } catch (e) {
        console.log("There was a problem with this request");
    }
};

let newUser = async () => {
    let url = "http://localhost:" + port + "/api/users";
    let user = { FirstName: newUserInput1.value, LastName: newUserInput2.value, Age: newUserInput3.value };
    await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    });
}

allUsersBtn.addEventListener("click", allUsers);
userByIdBtn.addEventListener("click", userById);
checkIfUserAdultBtn.addEventListener("click", checkIfUserAdult);
addNewUserBtn.addEventListener("click", newUser);