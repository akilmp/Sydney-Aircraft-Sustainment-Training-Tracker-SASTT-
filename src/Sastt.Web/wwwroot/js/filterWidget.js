export function attachFilter(inputId, tableId) {
    const input = document.getElementById(inputId);
    const table = document.getElementById(tableId);
    if (!input || !table) return;
    input.addEventListener('keyup', () => {
        const filter = input.value.toLowerCase();
        Array.from(table.getElementsByTagName('tr')).forEach(row => {
            const text = (row.textContent || '').toLowerCase();
            row.style.display = text.includes(filter) ? '' : 'none';
        });
    });
}
