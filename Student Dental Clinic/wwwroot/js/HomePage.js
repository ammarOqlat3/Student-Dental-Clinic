// Service Data
const servicesData = {
    'Cosmetic Fillings': {
        icon: '<i class="bi bi-brush fs-1 text-primary"></i>',
        description: 'Tooth-colored composite fillings that blend seamlessly with your natural teeth. Made from durable materials that match the color of your existing teeth.',
        details: '<ul class="list-unstyled"><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Natural appearance</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Durable and long-lasting</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> No mercury content</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Completed in one visit</li></ul>'
    },
    'Root Canal': {
        icon: '<i class="bi bi-bandaid fs-1 text-primary"></i>',
        description: 'Root canal treatment removes infected pulp, cleans the canal, and seals it to prevent further infection. Saves your natural tooth from extraction.',
        details: '<ul class="list-unstyled"><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Painless procedure</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Saves natural tooth</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Prevents infection spread</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Restores normal function</li></ul>'
    },
    'Cavities': {
        icon: '<i class="bi bi-eyedropper fs-1 text-primary"></i>',
        description: 'Remove decayed tooth structure and restore with appropriate filling material. Prevents further decay and tooth damage.',
        details: '<ul class="list-unstyled"><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Quick procedure</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Prevents further decay</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Restores tooth structure</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Pain-free treatment</li></ul>'
    },
    'Teeth Cleaning': {
        icon: '<i class="bi bi-droplet fs-1 text-primary"></i>',
        description: 'Professional deep cleaning removes plaque, tartar, and surface stains. Essential for preventing gum disease.',
        details: '<ul class="list-unstyled"><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Removes plaque & tartar</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Prevents gum disease</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Freshens breath</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Polishes teeth</li></ul>'
    },
    'Complete Dentures': {
        icon: '<i class="bi bi-teeth fs-1 text-primary"></i>',
        description: 'Custom-made complete dentures designed for comfort, fit, and natural appearance. Restore your smile and confidence.',
        details: '<ul class="list-unstyled"><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Custom fitted</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Natural appearance</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Comfortable wear</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Durable materials</li></ul>'
    },
    'Teeth Whitening': {
        icon: '<i class="bi bi-stars fs-1 text-primary"></i>',
        description: 'Professional whitening treatment that safely removes stains and discoloration for a brighter, more confident smile.',
        details: '<ul class="list-unstyled"><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Safe and effective</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Immediate results</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Removes stubborn stains</li><li><i class="bi bi-check-circle-fill text-primary me-2"></i> Professional supervision</li></ul>'
    }
};

// Add click event to all service cards
document.querySelectorAll('.service-card').forEach(card => {
    card.style.cursor = 'pointer';
    card.addEventListener('click', () => {
        const title = card.querySelector('h4').innerText;
        const data = servicesData[title];
        if (data) {
            document.getElementById('modalTitle').innerText = title;
            document.getElementById('modalIcon').innerHTML = data.icon;
            document.getElementById('modalDescription').innerText = data.description;
            document.getElementById('modalDetails').innerHTML = data.details;

            // Show modal using Bootstrap 5
            const modal = new bootstrap.Modal(document.getElementById('serviceModal'));
            modal.show();
        }
    });
});
// Counter Animation
const counters = document.querySelectorAll('.counter');
const speed = 200;

const animateCounters = () => {
    counters.forEach(counter => {
        const target = parseInt(counter.getAttribute('data-target'));
        const count = parseInt(counter.innerText);
        const increment = Math.ceil(target / speed);

        if (count < target) {
            counter.innerText = count + increment;
            setTimeout(animateCounters, 30);
        } else {
            counter.innerText = target;
        }
    });
};

// Start counters when visible
const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            animateCounters();
            observer.unobserve(entry.target);
        }
    });
});

const statsSection = document.querySelector('.stats-section');
if (statsSection) {
    observer.observe(statsSection);
}
// Back to Top
const backToTop = document.getElementById('backToTop');

window.addEventListener('scroll', () => {
    if (window.scrollY > 300) {
        backToTop.style.display = 'block';
    } else {
        backToTop.style.display = 'none';
    }
});

if (backToTop) {
    backToTop.addEventListener('click', () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });
}