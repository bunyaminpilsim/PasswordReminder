document.addEventListener('DOMContentLoaded', function () {
    // 🗑️ Silme işlemi
    document.querySelectorAll('.delete-btn').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            const id = this.dataset.id;
            if (confirm("Are you sure you want to delete this password?")) {
                document.getElementById('deleteId').value = id;
                document.getElementById('deleteForm').submit();
            }
        });
    });

    // 💬 Tooltip'ler
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // 🔍 Dinamik arama
    const searchInput = document.getElementById('searchInput');
    const cards = document.querySelectorAll('.password-card');

    if (searchInput) {
        searchInput.addEventListener('input', () => {
            const keyword = searchInput.value.toLowerCase();

            cards.forEach(card => {
                const title = card.dataset.title;
                const username = card.dataset.username;
                const url = card.dataset.url;

                const match = title.includes(keyword) || username.includes(keyword) || url.includes(keyword);
                card.style.display = match ? 'block' : 'none';
            });
        });
    }
});
