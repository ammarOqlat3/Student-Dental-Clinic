document.addEventListener("DOMContentLoaded", () => {
    const container = document.getElementById("container");
    const goRegister = document.getElementById("goRegister");
    const goLogin = document.getElementById("goLogin");

    // التأكد من أن الأزرار تعمل وتغير الـ Class
    if (goRegister) {
        goRegister.onclick = () => {
            container.classList.add("active");
        };
    }

    if (goLogin) {
        goLogin.onclick = () => {
            container.classList.remove("active");
        };
    }
});