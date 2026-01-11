## 2024-05-22 - Gallery Keyboard Accessibility
**Learning:** Using interactive `div`s with `onClick` completely blocks keyboard-only users from accessing core functionality like the image lightbox.
**Action:** Always use `<button>` for interactive elements that trigger actions, or explicitly add `role="button"`, `tabIndex={0}`, and `onKeyDown` handlers if semantic HTML isn't possible (though `<button>` is preferred).
