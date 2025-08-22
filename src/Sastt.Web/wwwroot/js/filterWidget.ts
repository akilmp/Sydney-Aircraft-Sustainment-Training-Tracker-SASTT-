export function attachFilter(inputId: string, tableId: string): void {
    const input = document.getElementById(inputId) as HTMLInputElement | null;
    const table = document.getElementById(tableId) as HTMLTableElement | null;
    if (!input || !table) return;

    input.addEventListener('keyup', () => {
        const filter = input.value.toLowerCase();
        Array.from(table.getElementsByTagName('tr')).forEach(row => {
            const text = row.textContent?.toLowerCase() ?? '';
            row.style.display = text.includes(filter) ? '' : 'none';
        });
    });
}
