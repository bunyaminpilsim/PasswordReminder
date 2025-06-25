document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.toggle-password').forEach(btn => {
        btn.addEventListener('click', function () {
            const input = document.getElementById(this.dataset.target);
            const icon = this.querySelector('i');
            if (input && icon) {
                const isPassword = input.type === 'password';
                input.type = isPassword ? 'text' : 'password';

                // Ikonları açık-kapalıya göre sıfırdan ayarla
                icon.classList.remove('bi-eye', 'bi-eye-slash');
                icon.classList.add(isPassword ? 'bi-eye-slash' : 'bi-eye');
            }
        });

        // İlk durum: input password ise icon bi-eye, değilse bi-eye-slash
        const input = document.getElementById(btn.dataset.target);
        const icon = btn.querySelector('i');
        if (input && icon) {
            icon.classList.remove('bi-eye', 'bi-eye-slash');
            icon.classList.add(input.type === 'password' ? 'bi-eye' : 'bi-eye-slash');
        }
    });
});
