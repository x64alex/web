

function getUsers(name,email,age,home,picture, callbackFunction) {
	$.getJSON(
		"userController",
		{ action: 'getAll', name: name, email: email, age: age, home: home, picture: picture },
	 	callbackFunction
	);
}

// function updateAsset(id, userid, description, value, callbackFunction) {
//     $.get("AssetsController",
// 		{ action: "update",
// 			id: id,
// 			userid: userid,
// 			description: description,
// 			value: value
// 		},
// 		callbackFunction
// 	);
// }
