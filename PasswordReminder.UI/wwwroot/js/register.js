// Global Functions
function evaluatePassword(password) {
    const regexes = [
        /.{12,}/,         // At least 12 characters
        /[A-Z]/,          // Uppercase letter
        /[a-z]/,          // Lowercase letter
        /[0-9]/,          // Digit
        /[^A-Za-z0-9]/    // Special character
    ];
    const score = regexes.reduce((acc, regex) => acc + (regex.test(password) ? 1 : 0), 0);

    if (score < 3) return { text: 'Weak', color: 'red' };
    if (score <= 4) return { text: 'Medium', color: 'orange' };
    return { text: 'Strong', color: 'green' };
}

function generatePassword(length) {
    const chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=?";
    return Array.from({ length }, () => chars.charAt(Math.floor(Math.random() * chars.length))).join('');
}

document.addEventListener('DOMContentLoaded', function () {
    // Show/Hide Password
    document.querySelectorAll('.toggle-password').forEach(btn => {
        btn.addEventListener('click', function () {
            const input = document.getElementById(this.dataset.target);
            const icon = this.querySelector('i');
            if (input && icon) {
                input.type = input.type === 'password' ? 'text' : 'password';
                icon.classList.toggle('bi-eye');
                icon.classList.toggle('bi-eye-slash');
            }
        });
    });

    // Password Strength Evaluation
    const passwordInput = document.getElementById('PasswordInput');
    const strengthDiv = document.getElementById('PasswordStrength');

    if (passwordInput && strengthDiv) {
        passwordInput.addEventListener('input', function () {
            const { text, color } = evaluatePassword(this.value);
            strengthDiv.textContent = `Password Strength: ${text}`;
            strengthDiv.style.color = color;
        });
    }

    // Password Generator
    const generateBtn = document.getElementById('GenerateBtn');
    if (generateBtn && passwordInput) {
        generateBtn.addEventListener('click', () => {
            const pwd = generatePassword(16);
            passwordInput.value = pwd;
            passwordInput.dispatchEvent(new Event('input'));
        });
    }
});
