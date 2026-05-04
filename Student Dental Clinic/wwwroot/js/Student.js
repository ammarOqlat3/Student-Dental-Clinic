function showView(name) {
    document.querySelectorAll(".st-view").forEach(v => v.classList.remove("active"));
    const target = document.getElementById("st-view-" + name);
    if (target) target.classList.add("active");

    document.querySelectorAll(".st-nav-link").forEach(l => {
        const clickCode = l.getAttribute("onclick") || "";
        l.classList.toggle("active", clickCode.includes("'" + name + "'"));
    });

    const titles = {
        dashboard: "Dashboard",
        profile: "My Profile",
        session: "Upcoming Session",
        cases: "All My Cases",
        reports: "My Reports",
        upload: "Upload Report",
        request: "Request a Case",
        "requests-list": "My Requests",
        notifications: "Notifications"
    };

    const titleEl = document.getElementById("stPageTitle");
    if (titleEl) titleEl.textContent = titles[name] || "Dashboard";
}

function filterMyReports(status, btn) {
    document.querySelectorAll("#st-view-reports .st-filter-tab").forEach(b => b.classList.remove("active"));
    if (btn) btn.classList.add("active");

    document.querySelectorAll("#myReportsGrid .st-report-col").forEach(card => {
        if (status === "all") {
            card.style.display = "";
        } else {
            card.style.display = card.dataset.status === status ? "" : "none";
        }
    });
}

function openPatientModal() {
    const modal = document.getElementById("patientModal");
    if (modal) { modal.style.display = "flex"; document.body.style.overflow = "hidden"; }
}

function closeStModal(id) {
    const modal = document.getElementById(id);
    if (modal) { modal.style.display = "none"; document.body.style.overflow = ""; }
}

document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".st-modal-overlay").forEach(m => {
        m.addEventListener("click", function (e) {
            if (e.target === this) { this.style.display = "none"; document.body.style.overflow = ""; }
        });
    });
});

// submitReport is defined in the page script (Students.cshtml) and handles the real backend call

function submitRequest() {
    const name = (document.getElementById("req-name")?.value || "").trim();
    const phone = (document.getElementById("req-phone")?.value || "").trim();
    const relation = (document.getElementById("req-relation")?.value || "").trim();
    const treatment = (document.getElementById("req-treatment")?.value || "").trim();

    if (!name || !phone || !relation || !treatment) {
        showStToast("Please fill all required fields.");
        return;
    }

    showStToast("Request form is valid. Backend save endpoint can be connected next.");
    setTimeout(() => showView("requests-list"), 900);
}

function showStToast(msg) {
    const container = document.getElementById("stToastContainer");
    if (!container) return;

    const toast = document.createElement("div");
    toast.className = "st-toast-item";
    toast.textContent = msg;
    container.appendChild(toast);

    setTimeout(() => {
        toast.style.opacity = "0";
        toast.style.transition = "opacity .25s";
        setTimeout(() => toast.remove(), 250);
    }, 2600);
}