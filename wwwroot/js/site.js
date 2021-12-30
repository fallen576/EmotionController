$(document).ready(function() {

    const video = document.getElementById("video");

    Promise.all([
    faceapi.nets.tinyFaceDetector.loadFromUri('/static'),//tiny_face_detector_model-weights_manifest.json
        faceapi.nets.faceLandmark68Net.loadFromUri('/static'),
        faceapi.nets.faceRecognitionNet.loadFromUri('/static'),
        faceapi.nets.faceExpressionNet.loadFromUri('/static')
    ])//.then(startVideo)

    function startVideo() {
        navigator.getUserMedia(
            { video: {} },
            stream => video.srcObject = stream,
            err => console.error(err)
        )
    }
    
    // video.addEventListener('play', () => {
    //     const canvas = faceapi.createCanvasFromMedia(video)
    //     document.body.append(canvas)
    //     const displaySize = { width: video.width, height: video.height }
    //     faceapi.matchDimensions(canvas, displaySize)
    //     setInterval(async () => {
    //         const detections = await faceapi.detectSingleFace(video, new faceapi.TinyFaceDetectorOptions())/*.withFaceLandmarks()*/.withFaceExpressions()
    //         const resizedDetections = faceapi.resizeResults(detections, displaySize)
    //         canvas.getContext('2d').clearRect(0, 0, canvas.width, canvas.height)
    //         faceapi.draw.drawDetections(canvas, resizedDetections)
    //         //faceapi.draw.drawFaceLandmarks(canvas, resizedDetections)
    //         faceapi.draw.drawFaceExpressions(canvas, resizedDetections)
    //         console.log(detections.expressions);
    //     }, 2000)
    //   })
})
let customInterval;
let emotionData = [];


function startVideo() {
    navigator.getUserMedia(
        { video: {} },
        stream => video.srcObject = stream,
        err => console.error(err)
    )
}

function send() {
    $.ajax({
        type: "POST",
        contentType:"application/json",
        url: "/api/calc/emotion",
        data: emotionData
    });
}

function track(on) {
    const canvas = faceapi.createCanvasFromMedia(video)
    if (on) {
        $("#begin").prop("disabled",true);
        $("#end").prop("disabled",false);
        running = true;
        document.body.append(canvas)
        const displaySize = { width: video.width, height: video.height }
        faceapi.matchDimensions(canvas, displaySize)
        customInterval = setInterval(async () => {
            const detections = await faceapi.detectSingleFace(video, new faceapi.TinyFaceDetectorOptions())/*.withFaceLandmarks()*/.withFaceExpressions()
            const resizedDetections = faceapi.resizeResults(detections, displaySize)
            canvas.getContext('2d').clearRect(0, 0, canvas.width, canvas.height)
            faceapi.draw.drawDetections(canvas, resizedDetections)
            //faceapi.draw.drawFaceLandmarks(canvas, resizedDetections)
            faceapi.draw.drawFaceExpressions(canvas, resizedDetections)
            console.log(detections.expressions);
            emotionData.push(detections.expressions);
        }, 2000)
    }
    else {
        $("#begin").prop("disabled",false);
        $("#end").prop("disabled",true);
        window.clearInterval(customInterval);
        document.getElementsByTagName("canvas")[0].remove();
        emotionData = [];
    }
}