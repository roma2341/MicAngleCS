var maxShort = 32768;
var data =[32768, 32969,33170,33371,33572,33774,33975,34176,
34377,34578,34779,34980,35180,35381,35582,35782];
function Point(x,y){
this.x=x;
this.y=y;
  }
  var points = [new Point(843.48118407,192.03251104),
  new Point(893.48118407,292.03251104),new Point(893.78118407,292.03251104)];
   var points2 = [new Point(843.48118407,392.03251104),
  new Point(893.48118407,292.03251104),new Point(893.78118407,292.03251104)];
     var points3 = [new Point(5481083.48118407,3300692.03251104),
  new Point(5479893.48118407,3300292.03251104),new Point(5479893.78118407,3300292.03251104)];
  var points4 = [new Point(5479893.48118407,3300292.03251104),
  new Point(5479893.78118407,3300292.03251104),new Point(5481083.48118407,3300692.03251104)];
  document.draw = function draw() {

    var cv = document.getElementById('cv');
    var ct = cv.getContext('2d');
    ct.save();
    
    var w = cv.width;
    var h = cv.height;
    var sizeKoffY = h / maxShort;
    var sizeKoffX = w / data.length;

   // ct.clearRect(0, 0, w, h);
   ct.beginPath();
   ct.moveTo(0,0);
    for(var i = 0; i < data.length; i++){
    var dataValue = data[i] * sizeKoffY;
    var xPosition = sizeKoffX*i;
    ct.lineTo(i+1,dataValue);
     // console.log('line to: '+i +' '+dataValue);
       ct.stroke();
    }
     ct.stroke();
        

    
    //ct.restore();
}
function drawPoints(){
var cv = document.getElementById('cv');
    var ct = cv.getContext('2d');
    ct.save();
    
    var w = cv.width;
    var h = cv.height;
    var sizeKoffY = h / maxShort;
    var sizeKoffX = w / data.length;

    ct.clearRect(0, 0, w, h);
    var colors = ["#FF0000","#00FF00","#0000FF","#AA0000","#BB0000"]

    for(var i = 0; i < points.length; i++){
      ct.fillStyle=colors[i];
      ct.fillRect(points[i].x,points[i].y,6,6);
    }
    //for (var i = 1; i < points.length; i++){
      console.log('angle '+find_angle(points4[0],points4[1],points4[2]));
    //}
     ct.stroke();
    
        
}
/*
 * Calculates the angle ABC (in radians) 
 *
 * A first point, ex: {x: 0, y: 0}
 * C second point
 * B center point
 */
function find_angle(A,B,C) {
    var AB = Math.sqrt(Math.pow(B.x-A.x,2)+ Math.pow(B.y-A.y,2));    
    var BC = Math.sqrt(Math.pow(B.x-C.x,2)+ Math.pow(B.y-C.y,2)); 
    var AC = Math.sqrt(Math.pow(C.x-A.x,2)+ Math.pow(C.y-A.y,2));
    return Math.acos((BC*BC+AB*AB-AC*AC)/(2*BC*AB))*(180/Math.PI);
}
function getAngleBetweenCuts(zeroPt,pt1,pt2){
  var x1 = pt1.x-zeroPt.x;
  var x2 = pt2.x-zeroPt.x;
  var y1 = pt1.y-zeroPt.y;
  var y2 = pt2.y-zeroPt.y;
  var z1=0;
  var z2=0;
  var top = x1*x2+y1*y2+z1*z2;
  var bottom = Math.sqrt(x1*x1+y1*y1+z1*z1)*Math.sqrt(x2*x2+y2*y2+z2*z2);
  var cosA = top / bottom;
  console.log('cosA:'+cosA);
  return Math.acos(cosA)*(180/Math.PI);
}
