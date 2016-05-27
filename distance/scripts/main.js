function processDistance() {
var xCordElement1 = document.getElementById('x_cord1');
var xCordElement2 = document.getElementById('x_cord2');
var yCordElement1 = document.getElementById('y_cord1');
var yCordElement2 = document.getElementById('y_cord2');
var x1 = Number(xCordElement1.value);
var x2 = Number(xCordElement2.value);
var y1 = Number(yCordElement1.value);
var y2 = Number(yCordElement2.value);
var resultElement = document.getElementById('result');
resultElement.textContent = getDistance(x1,y1,x2,y2);

}
function getDistance(x1,y1,x2,y2){
  return Math.sqrt((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2));
}