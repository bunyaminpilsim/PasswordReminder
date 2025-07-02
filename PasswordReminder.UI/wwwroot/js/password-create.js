function generatePassword(length = 16) {
    const chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=?";
    return Array.from({ length }, () => chars.charAt(Math.floor(Math.random() * chars.length))).join('');
}

function evaluatePasswordStrength(pwd) {
    const regexes = [/.{12,}/, /[A-Z]/, /[a-z]/, /[0-9]/, /[^A-Za-z0-9]/];
    const score = regexes.reduce((acc, rx) => acc + rx.test(pwd), 0);
    if (score <= 2) return { text: "Weak", color: "red" };
    if (score <= 4) return { text: "Medium", color: "orange" };
    return { text: "Strong", color: "green" };
}

document.addEventListener('DOMContentLoaded', () => {
    const input = document.getElementById('GeneratedPassword');
    const strength = document.getElementById('PasswordStrength');

    document.getElementById('GeneratePasswordBtn').addEventListener('click', () => {
        const pwd = generatePassword();
        input.value = pwd;
        input.dispatchEvent(new Event('input'));
    });

    input.addEventListener('input', () => {
        const result = evaluatePasswordStrength(input.value);
        strength.textContent = `Strength: ${result.text}`;
        strength.style.color = result.color;
    });

    document.getElementById('CopyPasswordBtn').addEventListener('click', () => {
        navigator.clipboard.writeText(input.value);
        alert("Password copied!");
    });
});
