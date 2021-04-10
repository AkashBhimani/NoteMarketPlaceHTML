/* ---------------------------- Password Show & Hide JS ---------------------------*/
$(".toggle-password").click(function () {

	$(this).toggleClass("fa-eye fa-eye-slash");
	var input = $($(this).attr("toggle"));
	if (input.attr("type") == "password") {
		input.attr("type", "text");
	} else {
		input.attr("type", "password");
	}
});

/* When the user clicks on the button, 
toggle between hiding and showing the dropdown content */
function showhideDropdown(id) {
	document.getElementById("showhideDropdown_" + id).classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
	if (!event.target.matches('.dropbtn')) {
		var dropdowns = document.getElementsByClassName("dropdown-content");
		var i;
		for (i = 0; i < dropdowns.length; i++) {
			var openDropdown = dropdowns[i];
			if (openDropdown.classList.contains('show')) {
				openDropdown.classList.remove('show');
			}
		}
	}
}

/*-------------- Upload Picture Extension Validations -------------*/
function ImageValidation1() {
	var fileInput = document.getElementById('image-input-1');
	var filePath = fileInput.value;
	var allowedExtensions = /(\.jpg)$/i;

	if (!allowedExtensions.exec(filePath)) {
		$("#img-extension-error-1").show();
		fileInput.value = '';
	}
	else {
		$("#img-extension-error-1").hide();
	}
}

function ImageValidation2() {
	var fileInput = document.getElementById('image-input-2');
	var filePath = fileInput.value;
	var allowedExtensions = /(\.jpg)$/i;

	if (!allowedExtensions.exec(filePath)) {
		$("#img-extension-error-2").show();
		fileInput.value = '';
	}
	else {
		$("#img-extension-error-2").hide();
	}
}
