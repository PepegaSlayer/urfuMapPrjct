const buildings = document.querySelectorAll('.building');
const popupBg = document.querySelector('.inside');
const popup = document.querySelector('.info');
const up = document.querySelector('.up_button');
const svg = document.querySelector('svg');
const img = popup.querySelector('.inside_map')
const over = document.querySelector('.overlay');
var current = 0;
var photo = "";
var floor = 1;
var flag = false;
buildings.forEach(building => {
    building.addEventListener('click', function () {
        floor = 1;
        maxFloor = this.dataset.maxfloor;
        photo = this.dataset.photo;
        popupBg.classList.add('active');
        img.setAttribute('src', photo + floor + ".png");
    })
})
popup.querySelector('.up_button').addEventListener('click', function () {
    if (floor < maxFloor)
        floor++;
    popup.querySelector('.inside_map').setAttribute('src', photo + floor + ".png")

});

popup.querySelector('.down_button').addEventListener('click', function () {
    if (floor > 1)
        floor--;
    popup.querySelector('.inside_map').setAttribute('src', photo + floor + ".png")

});

document.addEventListener('click', (e) => {
    if (e.target === popupBg) {
        popupBg.classList.remove('active');
        var lines = over.getElementsByTagName("line");
        for (var i = lines.length - 1; i >= 0; i--) {
            over.removeChild(lines[i]);
        }
    }
})


const submitButton = document.getElementById('submitButton');

const next = document.querySelectorAll('.MoveButton');
//const next = document.getElementById('outside');
//const next2 = document.getElementById('inside');
let data;
submitButton.addEventListener('click', (event) => {
    event.preventDefault();
    var lines = svg.getElementsByTagName("line");
    for (var i = lines.length - 1; i >= 0; i--) { 
        svg.removeChild(lines[i]);
    }

    const pointASelect = document.getElementById('pointAname');
    const pointBSelect = document.getElementById('pointBname');
    const pointAText = document.getElementById('pointA').value;
    const pointBText = document.getElementById('pointB').value;

    const pointAValue = pointASelect.options[pointASelect.selectedIndex].value;
    const pointBValue = pointBSelect.options[pointBSelect.selectedIndex].value;

    const dataToSend = {
        pointAprefix: pointAValue,
        pointBprefix: pointBValue,
        pointA: pointAText,
        pointB: pointBText
    };
    console.log(JSON.stringify(dataToSend));
    fetch('https://localhost:7059/Home/GetPoints/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(dataToSend)
    })
        .then(response => response.json())
        .then(list => {
            console.log(list);
            data = list;
            current = 0;
            handleData(data);
           
        })
        .catch(error => console.error(error));

});

next.forEach(n => {
    n.addEventListener('click', (event) => {
        event.preventDefault();
        if (current < data.length - 1) {
            if (data[current].item1 != "Ул") {
                var lines = over.getElementsByTagName("line");
                for (var i = lines.length - 1; i >= 0; i--) {
                    over.removeChild(lines[i]);
                }
            }
            current = current + 1
            console.log(current);
            handleData(data);

        }
    })
})

let list;

function handleData(data) {
    list = data[current].item2;
    if (data[current].item1 != "Ул") {
      //  var temp = data[current].Item1.split(";");
        var name = data[current].item1[0];
        var floor = data[current].item1[1];
        var bld = Array.from(buildings).find(building => building.dataset.name === name);

        photo = bld.dataset.photo;
        img.setAttribute('src', photo + floor + ".png");
       
        popupBg.classList.add('active');
        flag = true;

    }
    else {
        popupBg.classList.remove('active');
        for (var i = 0; i < list.length - 1; i++) {
            console.log(list[i].X);
            const line = document.createElementNS("http://www.w3.org/2000/svg", "line");
            line.setAttribute("x1", list[i].x);
            line.setAttribute("y1", list[i].y);
            line.setAttribute("x2", list[i + 1].x);
            line.setAttribute("y2", list[i + 1].y);
            line.setAttribute('stroke', "red")
            line.setAttribute('stroke-width', "5")
            svg.appendChild(line);
        } }
}

img.addEventListener('load', function () {
  //  popup.height = popup.height + 200;
    if (flag) {
        let kx = img.width / img.naturalWidth;
        let ky = img.height / img.naturalHeight;
        console.log(img.width);
        console.log(img.naturalWidth);
        for (var i = 0; i < list.length - 1; i++) {
            const line = document.createElementNS("http://www.w3.org/2000/svg", "line");

            line.setAttribute("x1", list[i].x * kx);
            line.setAttribute("y1", list[i].y * ky);
            line.setAttribute("x2", list[i + 1].x * kx);
            line.setAttribute("y2", list[i + 1].y * ky);
            line.setAttribute('stroke', "red")
            line.setAttribute('stroke-width', "5")
            over.appendChild(line);
        }
        flag = false;
    }
});