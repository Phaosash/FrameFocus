window.scrollTabIntoView = (tabId) => {
    const tab = document.getElementById(tabId);
    if (tab) {
        tab.scrollIntoView({ behavior: 'smooth', inline: 'center' });
    }
};