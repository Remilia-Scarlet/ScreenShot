var g_pos = 0;
var g_backSpacePos = 0;
var g_bytePos = 0;
var g_indexPos = 0;
var g_lastValPos0 = 0;
var g_lastValPos1 = 0;
var g_dataStart = 0;
var g_sendFinishedPos = 0;
var g_canvas = document.getElementById('pic');
var g_ctx = g_canvas.getContext('2d');
var DataType = {"Char" : 0 , "Short" : 1 , "Int" : 2 ,"Pixel" : 3};
var g_byteType = 0;
var g_data = new Array();
var g_fileEntry;
var g_backNum = 0;
var g_lastIntervalIndex = -1;
var g_intervalTimer;

document.getElementById("tinput").style.visibility = "hidden";
initCanvas();
chrome.fileSystem.chooseEntry({
    type: 'saveFile',
    suggestedName: 'log.txt'
}, function(fileEntry) {
    g_fileEntry = fileEntry;
});

function initCanvas(){

	g_ctx.fillStyle='#DEDEDE';
	g_ctx.fillRect(0,0,g_canvas.width,g_canvas.height);

	g_ctx.fillStyle='#000000';
	g_ctx.fillRect(0,0,1,1);
	g_ctx.fillStyle='#0000FF';
	g_ctx.fillRect(1,0,1,1);
	g_ctx.fillStyle='#00FF00';
	g_ctx.fillRect(2,0,1,1);
	g_ctx.fillStyle='#00FFFF';
	g_ctx.fillRect(3,0,1,1);
	g_ctx.fillStyle='#FF0000';
	g_ctx.fillRect(4,0,1,1);
	g_ctx.fillStyle='#FF00FF';
	g_ctx.fillRect(5,0,1,1);
	g_ctx.fillStyle='#FFFF00';
	g_ctx.fillRect(6,0,1,1);
	g_ctx.fillStyle='#FFFFFF';
	g_ctx.fillRect(7,0,1,1);

	g_pos = 8;
	appendData(g_canvas.width , DataType.Short);
	appendData(g_canvas.height , DataType.Short);
	
	g_backSpacePos = g_pos;
	g_ctx.fillStyle='#AAAAAA';
	g_ctx.fillRect(g_backSpacePos,0,1,1);
	g_pos = g_pos + 1;

	g_bytePos = g_pos;
	appendData(g_byteType,DataType.Pixel);

	g_indexPos = g_pos;
	appendData(0,DataType.Int);
	g_lastValPos0 = g_pos;
	appendData(0,DataType.Char);
	g_lastValPos1 = g_pos;
	appendData(0,DataType.Char);

	g_dataStart = g_pos;
	g_sendFinishedPos = g_dataStart + 65535 + 1;
	setData(0,DataType.Pixel,g_sendFinishedPos );
}
function appendData(data,type){
	g_pos = g_pos + setData(data,type,g_pos);
}
function setData(data,type,pos){
	var mask = 0;
	var ret = 0;
	var color = '#';
	switch(type){
		case DataType.Char:
				mask = 128;
				data = data & 255;
				ret = 3;
				color = '#00';
			break;
		case DataType.Short:
				mask = 32768;
				data = data & 65535;
				ret = 6;
				color = '#0000';
			break;
		case DataType.Int:
				mask = 2147483648;
				data = data & 4294967295;
				ret = 11;
				color = '#00';
			break;
		case DataType.Pixel:
				mask = 4;
				data = data & 7;
				ret = 1;
				color = "#";
			break;
	}
	while(mask != 0){
		var num = data & mask;
		if( num != 0)
			color = color + "FF";
		else
			color = color + "00";
		if( color.length == 7){
			g_ctx.fillStyle = color;
			g_ctx.fillRect(parseInt(pos % g_canvas.width),parseInt(pos / g_canvas.width) ,1,1);
			pos = pos + 1;
			color = '#';
		}
		mask = mask >> 1;
		if(type == DataType.Int)
			mask = mask & 2147483647;
	}
	return ret;
}
g_canvas.onmousedown = function(){
	var e = window.event;
	var posx = e.clientX - g_canvas.offsetLeft;
	var posy = e.clientY - g_canvas.offsetTop;
	var pos = posy * g_canvas.width + posx;
	AddLog(""+pos+"     ");
	if(pos == g_bytePos)
		ChangeTypeBit();
	else if(pos == g_backSpacePos )
		SetBackSpace();
	else if(pos == g_sendFinishedPos )
		SendFinished();
	else
		HandleSend(pos);
	AddLog("<br/>");
	if(g_lastIntervalIndex == -1)
	{
		g_lastIntervalIndex = 0;
		g_intervalTimer = setInterval(RefreshState,1000)
	}
	RefreshInfo();
}
function ChangeTypeBit()
{
	if(g_byteType == 1)
		g_byteType = 0;
	else
		g_byteType = 1;
	setData(g_byteType ,DataType.Pixel, g_bytePos);
	AddLog("type:"+g_byteType +" ");
}
function SetBackSpace()
{
	if(g_data.lenth == 0)
		return;
	g_backNum++;
	g_data.length--;
	setData(g_data.length,DataType.Int, g_indexPos);
	setData(g_data[g_data.length - 1],DataType.Char,g_lastValPos1);
	if(g_data.length > 1)
		setData(g_data[g_data.length - 2],DataType.Char,g_lastValPos0);
}
function HandleSend(pos)
{
	pos -= g_dataStart;
	if(g_byteType == 1)
	{
		var high = (pos >> 8 ) & 255;
		g_data.push(high);
	}
	var low = pos & 255;
	g_data.push(low);
	setData(g_data.length,DataType.Int, g_indexPos);
	setData(low,DataType.Char,g_lastValPos1);
	if(g_data.length > 1)
		setData(g_data[g_data.length - 2],DataType.Char,g_lastValPos0);
	AddLog("data:h:"+high+" low:"+low);
}
function SendFinished()
{
	AddLog(" sendfinish");
	if(g_fileEntry == null)
	{
		var bb = new Blob([new Uint8Array(g_data)]);
		var f = new FileReader();
 		f.onload = function(e) {
			var str = e.target.result;
			document.getElementById("tinput").style.visibility='visible';
			document.getElementById("tinput").value = str; 
			setData(1,DataType.Pixel,g_sendFinishedPos );
  		};
  		f.readAsText(bb);
	}
	else
	{
		g_fileEntry.createWriter(function(fileWriter) {
			AddLog(" getwriter");
			fileWriter.onwriteend = function(){
				setData(1,DataType.Pixel,g_sendFinishedPos );
				AddLog(" savefinished");
				document.getElementById("infos").innerHTML = "finished";
				clearInterval(g_intervalTimer);
			}
			fileWriter.write(new Blob([new Uint8Array(g_data)],{type: 'application/zip'}));
    		},errorHandler);
	}
	AddLog(" sendfinished");
}
function errorHandler(e) {
	AddLog(e);
}
function AddLog(text)
{
	//document.getElementById("infos").innerHTML += text;
}
function RefreshInfo()
{
	document.getElementById("infos").innerHTML = "index:"+g_data.length+" back:"+ g_backNum;
}
function RefreshState()
{
	document.getElementById("statistics").innerHTML = (g_data.length - g_lastIntervalIndex) / 1000 + "kb/s";
	g_lastIntervalIndex = g_data.length ;
}