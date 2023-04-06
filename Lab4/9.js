function sortAndDisplay() {
    const inputText = document.getElementById('input').value.trim();
    
    const inputArray = inputText.split(/\s+/).map(Number);
    
    inputArray.sort((a, b) => a - b);
    
    const numRows = Math.ceil(inputArray.length / 5);
    let outputHtml = '<table>';
    for (let i = 0; i < numRows; i++) {
      outputHtml += '<tr>';
      for (let j = 0; j < 5; j++) {
        const index = i * 5 + j;
        if (index < inputArray.length) {
          outputHtml += `<td>${inputArray[index]}</td>`;
        } else {
          outputHtml += '<td></td>';
        }
      }
      outputHtml += '</tr>';
    }
    outputHtml += '</table>';
    
    document.getElementById('output').innerHTML = outputHtml;

    //12 13 2 4 5 6 7 8 2 3 213 3 3213 312 22 12
  }