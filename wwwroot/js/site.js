const video = $('#video');

Promise.all([
  faceapi.nets.tinyFaceDetector.loadFromUri('/js'),//tiny_face_detector_model-weights_manifest.json
  faceapi.nets.faceLandmark68Net.loadFromUri('/js/faceapi/models'),
  faceapi.nets.faceRecognitionNet.loadFromUri('/js/faceapi/models'),
  faceapi.nets.faceExpressionNet.loadFromUri('/js/faceapi/models')
]).then(startVideo)

function startVideo() {
  navigator.getUserMedia(
    { video: {} },
    stream => video.srcObject = stream,
    err => console.error(err)
  )
}