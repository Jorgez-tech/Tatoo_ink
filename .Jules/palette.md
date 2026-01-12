## 2024-05-22 - Gallery Keyboard Accessibility
**Learning:** Using interactive `div`s with `onClick` completely blocks keyboard-only users from accessing core functionality like the image lightbox.
**Action:** Always use `<button>` for interactive elements that trigger actions, or explicitly add `role="button"`, `tabIndex={0}`, and `onKeyDown` handlers if semantic HTML isn't possible (though `<button>` is preferred).

## 2025-01-28 - Mobile Menu Accessibility
**Learning:** Icon-only buttons (like hamburger menus) are invisible to screen readers without explicit labels.
**Action:** Always add `aria-label` (dynamic if state changes), `aria-expanded`, and `aria-controls` to mobile toggle buttons.
