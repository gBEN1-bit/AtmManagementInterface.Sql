





const currentLocation = location.href;
const linkItem = document.querySelectorAll('a');
const linkLength = linkItem.length;
for (let i = 0; i < linkLength; i++) {
    if (linkItem[i].href === currentLocation) {
        linkItem[i].className = "active"
        linkItem[i].style.color = "black"
        linkItem[i].style.backgroundColor = "white";
    }
}