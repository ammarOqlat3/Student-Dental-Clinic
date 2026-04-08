const translations = {
    en: {
        title: "Clinical Cases Platform",
        subtitle: "Browse and book available training cases",
        caseType: "Case Type",
        ageGroup: "Age Group",
        allCases: "All Cases",
        allAges: "All Ages",
        filling: "Filling",
        endo: "Root Canal (Endo)",
        ortho: "Orthodontics",
        scaling: "Scaling",
        updateResults: "Update Results",
        age: "Age",
        year: "years",
        address: "Location",
        phone: "Phone",
        gender: "Gender",
        email: "Email",
        location: "Address",
        bookNow: "Book Now",
        alreadyBooked: "Already Booked",
        sendToDoctor: "Send to Doctor",
        status_متاح: "Available",
        status_محجوز: "Booked"
    },
    ar: {
        title: "منصة الحالات السريرية",
        subtitle: "استعرض وحجز الحالات التدريبية المتاحة",
        caseType: "نوع الحالة",
        ageGroup: "الفئة العمرية",
        allCases: "كل الحالات",
        allAges: "كل الأعمار",
        filling: "حشوة",
        endo: "سحب عصب",
        ortho: "تقويم",
        scaling: "تنظيف",
        updateResults: "تحديث النتائج",
        age: "العمر",
        year: "سنة",
        address: "السكن",
        phone: "رقم الهاتف",
        gender: "الجنس",
        email: "الايميل",
        location: "مكان السكن",
        bookNow: "احجز الآن",
        alreadyBooked: "محجوز",
        sendToDoctor: "إرسال للدكتور",
        status_متاح: "متاح",
        status_محجوز: "محجوز"
    }
};
function changeLanguage(lang) {
    document.querySelectorAll("[data-key]").forEach(el => {
        const key = el.getAttribute("data-key");
        if (translations[lang][key]) {
            el.innerText = translations[lang][key];
        }
    });
}