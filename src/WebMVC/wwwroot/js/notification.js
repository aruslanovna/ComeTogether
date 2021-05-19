// Modal Function

let siteBody = document.querySelector("body");
let mainWindow = document.querySelector(".mainWindow");
let popUpBtn = document.querySelectorAll(".popUpBtn");
let popUp = document.querySelectorAll(".popUp");
let modalWindow = document.querySelector(".modalWindow");
let closeModal = document.querySelectorAll(".closeModal");
let notificationBell = document.querySelector(".notificationBell");
let notificationBar = document.querySelector(".notificationBar");



function openLayer() {
    mainWindow.classList.toggle("mainWindow--inactive");
    siteBody.classList.toggle("disableScrolling");
    modalWindow.classList.toggle("modalWindow--inactive");
}

function closeLayer() {
    mainWindow.classList.toggle("mainWindow--inactive");
    siteBody.classList.toggle("disableScrolling");
    modalWindow.classList.add("modalWindow--inactive");
}

popUpBtn.forEach(e => {
    e.addEventListener("click", () => {
        openLayer();
        let targetPopUp = document.getElementById(e.dataset.targetPopup);
        targetPopUp.classList.remove("popUp--inactive");
    });
});

notificationBell.addEventListener("click", () => {
    openLayer();
    notificationBar.classList.remove("notificationBar--inactive");
});

closeModal.forEach(e => {
    e.addEventListener("click", () => {
        closeLayer();
        popUp.forEach(popUp => {
            popUp.classList.add("popUp--inactive");
        });
        notificationBar.classList.add("notificationBar--inactive");


    });
});

// Single Page Functionality
let darkModeSet = "false";
let singlePageLinks = document.querySelectorAll(".singlePageLink");
let contentSinglePage = document.querySelectorAll(".content--singlePage");
singlePageLinks.forEach(e => {
    e.addEventListener("click", () => {
        contentSinglePage.forEach(page => {
            page.classList.remove("content--singlePage--active");
        });
        let targetPageId = "content--" + e.dataset.targetRef;
        let targetPage = document.getElementById(targetPageId);
        targetPage.classList.add("content--singlePage--active");
        sizeProjectTable();
        if (darkModeSet == "true") {
            singlePageLinks.forEach(s => {
                s.classList.remove('navigation__item--active--dark');
            });
            e.classList.toggle('navigation__item--active--dark');
        } else {
            singlePageLinks.forEach(s => {
                s.classList.remove('navigation__item--active--dark');
            });
        }
    });
});

//singlePageLinks[0].click();

// Navbar Functionality

//let navItems = document.querySelectorAll(".navigation__item");

//navItems.forEach(e => {
//    e.addEventListener("click", () => {
//        navItems.forEach(item => {
//            item.classList.remove("navigation__item--active");
//        });
//        e.classList.add("navigation__item--active");
//    });
//});

//navItems[0].click();

//// New Project Button Single Page Functionality

//document.querySelector('#createProjectBtn').addEventListener('click', () => {
//    navItems.forEach(item => {
//        item.classList.remove("navigation__item--active");
//    });
//    navItems.forEach(item => {
//        if (item.dataset.targetRef == 'project') {
//            item.classList.add("navigation__item--active");
//        }
//    });

//});

//// Current Amount of Notifications Functionality
//let newNotifications = document.querySelectorAll(".newNotification").length;
//let notificationBellValue = document.querySelector(".notificationBell__value");

//notificationBellValue.innerText = newNotifications;

//// Notifications Remove Functionality
//let deleteNotification = document.querySelectorAll(
//    ".notificationBox__header__delete"
//);
//let leftNotifications = deleteNotification.length;

//deleteNotification.forEach(e => {
//    e.addEventListener("click", () => {
//        let notificationBox = e.parentNode.parentNode;
//        notificationBar.removeChild(notificationBox);
//        leftNotifications--;
//        if (notificationBox.classList.contains("newNotification")) {
//            newNotifications--;
//        }
//        if (newNotifications != 0) {
//            notificationBellValue.innerText = newNotifications;
//        } else {
//            notificationBellValue.style.display = "none";
//            document.querySelector('.notificationBox__noResults').classList.toggle('notificationBox__noResults--inactive');
//        }
//    });
//});

// HR Filter Function







// Dark Mode functionality

let darkModeBtn = document.querySelector(".darkModeBtn");
let navbar = document.querySelector(".navbar");
let topbar = document.querySelector(".topbar");
let popUpProjectNotes = document.querySelectorAll(".popUp__body__notes");
let popUpProjectEdit = document.querySelectorAll(".popUp__editProject");
let kpiTiles = document.querySelectorAll(".kpiComponent__tile");
let employeeTile = document.querySelectorAll(".hrWidget__employeeTile");
let navigationItem = document.querySelectorAll('.navigation__item');

let logOut = document.querySelector('.logout');
let blackFont = document.querySelectorAll(".blackFont");


darkModeBtn.addEventListener("click", () => {
    if (darkModeSet == "false") {
        darkModeSet = "true";
    } else {
        darkModeSet = "false";
    }
    siteBody.classList.toggle("body--dark");
    navbar.classList.toggle("navbar--dark");
    topbar.classList.toggle("topbar--dark");
    kpiTiles.forEach(e => {
        e.classList.toggle("kpiComponent__tile--dark");
    });
    blackFont.forEach(e => {
        e.classList.toggle("blackFont--dark");
    });
    darkModeBtn.classList.toggle("darkModeBtn--dark");
    popUp.forEach(popUp => {
        popUp.classList.toggle("popUp--dark");
    });
    popUpProjectNotes.forEach(e => {
        e.classList.toggle("popUp__body__notes--dark");
    });
    popUpProjectEdit.forEach(e => {
        e.classList.toggle("popUp__editProject--dark");
    });
    notificationBar.classList.toggle("notificationBar--dark");
    employeeTile.forEach(e => {
        e.classList.toggle("hrWidget__employeeTile--dark");
    });
    navigationItem.forEach(e => {
        e.classList.toggle('navigation__item--dark')
    });


    document.querySelector('.navigation__item--active').classList.toggle('navigation__item--active--dark');
    logOut.classList.toggle('logout--dark');
});
