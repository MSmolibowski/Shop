window.showToast = function (message, type = 'info', delay = 3000) {
    const types = {
        success: 'text-white bg-success',
        danger: 'text-white bg-danger',
        warning: 'text-dark bg-warning',
        info: 'text-white bg-primary'
    };

    const icons = {
        success: 'bi-check-circle-fill',
        danger: 'bi-x-circle-fill',
        warning: 'bi-exclamation-triangle-fill',
        info: 'bi-info-circle-fill'
    };

    const toastId = `toast-${Date.now()}`;
    const css = types[type] || types.info;
    const icon = icons[type] || icons.info;

    const toastHTML = `
        <div id="${toastId}" class="toast align-items-center ${css}" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="${delay}">
            <div class="d-flex">
                <div class="toast-body d-flex align-items-center gap-2">
                    <i class="bi ${icon}"></i>
                    <span>${message}</span>
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>`;

    const container = document.getElementById('toastContainer');
    container.insertAdjacentHTML('beforeend', toastHTML);

    const toastElement = document.getElementById(toastId);
    const toast = new bootstrap.Toast(toastElement);
    toast.show();

    toastElement.addEventListener('hidden.bs.toast', () => toastElement.remove());
};

window.showToastSuccess = function (message, delay = 3000) {
    window.showToast(message, 'success', delay);
};

window.showToastError = function (message, delay = 3000) {
    window.showToast(message, 'danger', delay);
};

window.showToastWarning = function (message, delay = 3000) {
    window.showToast(message, 'warning', delay);
};

window.showToastInfo = function (message, delay = 3000) {
    window.showToast(message, 'info', delay);
};