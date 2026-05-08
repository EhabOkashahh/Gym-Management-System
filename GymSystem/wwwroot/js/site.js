/* ══ HAMBURGER ══════════════════════════════════════════════ */
(function () {
    const hamburger = document.getElementById('pfHamburger');
    const drawer = document.getElementById('pfDrawer');
    if (!hamburger || !drawer) return;

    hamburger.addEventListener('click', () => {
        hamburger.classList.toggle('open');
        drawer.classList.toggle('open');
    });

    document.addEventListener('click', e => {
        if (!e.target.closest('.pf-nav') && !e.target.closest('.pf-drawer')) {
            hamburger.classList.remove('open');
            drawer.classList.remove('open');
        }
    });
})();

/* ══ ACTIVE NAV LINK ════════════════════════════════════════ */
(function () {
    const currentPath = window.location.pathname.toLowerCase();
    
    // Normalize path: ensure it ends with / for consistent comparison if needed, 
    // or compare segments.
    const pathSegments = currentPath.split('/').filter(s => s.length > 0);

    document.querySelectorAll('.pf-links a, .pf-drawer a').forEach(link => {
        const href = link.getAttribute('href');
        if (!href) return;
        const hrefLower = href.toLowerCase();
        
        if (hrefLower === '/' && (currentPath === '/' || currentPath === '')) {
            link.classList.add('pf-active');
            return;
        }

        const hrefSegments = hrefLower.split('/').filter(s => s.length > 0);
        
        // Check if all segments of href are at the start of path segments
        let match = hrefSegments.length > 0 && pathSegments.length >= hrefSegments.length;
        if (match) {
            for (let i = 0; i < hrefSegments.length; i++) {
                if (hrefSegments[i] !== pathSegments[i]) {
                    match = false;
                    break;
                }
            }
        }

        if (match) {
            link.classList.add('pf-active');
        }
    });
})();

/* ══ CHAT ════════════════════════════════════════════════════ */
function toggleChat() {
    const win = document.getElementById('chatWindow');
    if (win) win.classList.toggle('open');
}

function sendChat() {
    const input = document.getElementById('chatInputField');
    const messages = document.getElementById('chatMessages');
    if (!input || !messages) return;
    const text = input.value.trim();
    if (!text) return;

    const bubble = document.createElement('div');
    bubble.className = 'chat-bubble sent';
    bubble.textContent = text;
    messages.appendChild(bubble);
    input.value = '';
    messages.scrollTop = messages.scrollHeight;
}

document.addEventListener("DOMContentLoaded", () => {
    const alert = document.getElementById("pfAlert");

    if (alert) {
        setTimeout(() => {
            alert.style.opacity = "0";
            alert.style.transition = "opacity 0.4s ease";

            setTimeout(() => alert.remove(), 400);
        }, 2000);
    }
});

document.addEventListener("DOMContentLoaded", () => {

    function format(date) {
        return date.toISOString().slice(0, 16);
    }

    const start = document.getElementById("StartDateInput");
    const end = document.getElementById("EndDateInput");

    const now = new Date();

    if (start && !start.value) {
        start.value = format(now);
    }

    if (end && !end.value) {
        const later = new Date(now.getTime() + 60 * 60 * 1000);
        end.value = format(later);
    }
});

