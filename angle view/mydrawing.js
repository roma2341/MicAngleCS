var p1 = {//Sound pos
	x: 0,
	y: 0
}
var p2 = {//Mic1 pos
	x: 500,
	y: 100
}
var p3 = {//Mic2 pos
	x: 500.3,
	y: 100
}
function process(){
	readPts();
	mydraw();
}
function readPts(){
	p1.x=document.getElementById("pt1x").value;
	p1.y=document.getElementById("pt1y").value;
	p2.x=document.getElementById("pt2x").value;
	p2.y=document.getElementById("pt2y").value;
	p3.x=document.getElementById("pt3x").value;
	p3.y=document.getElementById("pt3y").value;
}


function mydraw(){
	var canvas = document.getElementById('mycanvas');
	var context = canvas.getContext("2d");
	//microphones line
	//context.setfillstyle("#aaaaaa");
	context.beginPath();
	context.clearRect(0,0,canvas.width,canvas.height);

context.moveTo(p1.x,p2.y);
context.lineTo(p3.x,p3.y);

//sound to main mic position
context.moveTo(p1.x,p1.y);
context.lineTo(p3.x,p3.y);

//paint angle value
context.fillText(find_angle(p1,p3,p2),p3.x-3,p3.y-3);
context.closePath();
context.stroke();

}

 /*
 * Calculates the angle ABC (in radians) 
 *
 * A first point
 * C second point
 * B center point
 */
function find_angle(A,B,C) {
    var AB = Math.sqrt(Math.pow(B.x-A.x,2)+ Math.pow(B.y-A.y,2));    
    var BC = Math.sqrt(Math.pow(B.x-C.x,2)+ Math.pow(B.y-C.y,2)); 
    var AC = Math.sqrt(Math.pow(C.x-A.x,2)+ Math.pow(C.y-A.y,2));
    return (Math.acos((BC*BC+AB*AB-AC*AC)/(2*BC*AB))* 180 / Math.PI).toFixed(3);
}