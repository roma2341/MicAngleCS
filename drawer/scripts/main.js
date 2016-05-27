var maxShort = 32768;
var data =[32768, 32969,33170,33371,33572,33774,33975,34176,
34377,34578,34779,34980,35180,35381,35582,35782];
function Point(x,y){
this.x=x;
this.y=y;
  }
  var points = [new Point(843.48118407,192.03251104),
  new Point(893.48118407,292.03251104),new Point(893.78118407,292.03251104)];
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
     ct.stroke();
        
}
