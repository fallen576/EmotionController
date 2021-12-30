const video = $('#video');

Promise.all([
  faceapi.nets.tinyFaceDetector.loadFromUri('/static'),//tiny_face_detector_model-weights_manifest.json
    faceapi.nets.faceLandmark68Net.loadFromUri('/static'),
    faceapi.nets.faceRecognitionNet.loadFromUri('/static'),
    faceapi.nets.faceExpressionNet.loadFromUri('/static')
]).then(startVideo)

function startVideo() {
  navigator.getUserMedia(
    { video: {} },
    stream => video.srcObject = stream,
    err => console.error(err)
  )
}