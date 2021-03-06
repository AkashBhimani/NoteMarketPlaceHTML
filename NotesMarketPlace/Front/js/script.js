/* ---------- Hide & Show Password ---------- */
$(".toggle-password").click(function () {

	$(this).toggleClass("fa-eye fa-eye-slash");
	var input = $($(this).attr("toggle"));
	if (input.attr("type") == "password") {
		input.attr("type", "text");
	} else {
		input.attr("type", "password");
	}
});

/* ---------- Set Nav Position ---------- */
function setNavPosition() {
	if (window.pageYOffset >= 90) {
		$("nav").addClass("loginuser-fixed-header");
		$("nav").removeClass("loginuser-sticky-header");
	} else {
		$("nav").addClass("loginuser-sticky-header");
		$("nav").removeClass("loginuser-fixed-header");
	}
}

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

/* ---------- Home Page Nav ---------- */
function setFixedNavigation() {
	if (window.pageYOffset <= 99) {
		$("#navbar").addClass("sticky-header");
		$("#navbar").removeClass("fixed-header");
		$(".home #navbar .navbar-header img").attr("src", "images/general/top-logo.png");
		$(".home #navbar .hamburger-icon").css("color", "#fff");
	} else {
		$("#navbar").addClass("fixed-header");
		$("#navbar").removeClass("sticky-header");
		$(".home #navbar .navbar-header img").attr("src", "images/Dashboard/logo.png");
		$(".home #navbar .hamburger-icon").css("color", "#6255a6");
	}
}

$(window).on('load', function () {
	setFixedNavigation();
});

window.onscroll = function () {
	setNavPosition();
	setFixedNavigation();
};

/* ---------- FAQ ---------- */
var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
	acc[i].addEventListener("click", function () {
		this.classList.toggle("active");
		var panel = this.nextElementSibling;
		if (panel.style.maxHeight) {
			panel.style.maxHeight = null;
		} else {
			panel.style.maxHeight = panel.scrollHeight + "px";
		}
	});
}

$("button.accordion").click(function () {
	$(".accordion.active").next().css('border-bottom', '1px solid #f3f3f3');
})
$("button.accordion").click(function () {
	$(".accordion.active").next().css('border-bottom', '1px solid #d1d1d1');
})
