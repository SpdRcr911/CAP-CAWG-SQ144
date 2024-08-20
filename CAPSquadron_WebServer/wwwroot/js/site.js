function downloadFile(fileName, byteArray) {
    // Convert the byte array to a Blob
    var byteArrayBuffer = new Uint8Array(byteArray);
    var blob = new Blob([byteArrayBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    var url = URL.createObjectURL(blob);
    var anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = fileName;
    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);
    URL.revokeObjectURL(url); // Clean up the URL object
}
