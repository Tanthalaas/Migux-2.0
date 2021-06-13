const miguxContainer = `<div id="migux-container">
<div id="migux-header">
  <img src="assets/logo-migux-semselo.gif" alt="Migux Logo" id="migux-logo">
  <a class="header-button" href="https://twitter.com/MiguxBR" target="_blank">
    <img src="assets/twitter.svg" alt="Twitter logo">
    <p>Twitter</p>
  </a>
</div>
`;

const body = '<body>';

const miguxContainerEnd = '</div><script src="https://cdn.socket.io/3.1.3/socket.io.min.js"'

const socketIoStart = '<script src="https://cdn.socket.io/3.1.3/socket.io.min.js"';

const hiddenUnityFooter = '<div id="unity-footer" hidden>';
const unityFooter = '<div id="unity-footer">';

module.exports = {miguxContainer, body, miguxContainerEnd, socketIoStart, hiddenUnityFooter, unityFooter};