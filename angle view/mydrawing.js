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



function mydraw(){
	var context = document.getElementById('mycanvas').getContext("2d");
	//microphones line
context.moveTo(p1.x,p2.y);
context.lineTo(p3.x,p3.y);

//sound to main mic position
context.moveTo(p1.x,p1.y);
context.lineTo(p3.x,p3.y);

//paint angle value
context.fillText(find_angle(p1,p3,p2),p3.x-3,p3.y-3);
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