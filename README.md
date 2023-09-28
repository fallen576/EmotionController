# EmotionController
Control RGB light on an Arduino via emotion recognition.

### Backend
ASP.Net Core MVC Web Application, C++ Arduino Project

### Frontend
Vanilla JavaScript using the [face-api library](https://justadudewhohacks.github.io/face-api.js/docs/index.html) which is implemented on top of the tensorflow.js core API.

## Project Flow
You start the project and will be taken to /static/index.html where you will see three buttons
![Alt text](https://github.com/fallen576/EmotionController/blob/main/wwwroot/images/homepage.png "Image 1")

When you click begin it will turn on your webcam. To start the emotion detection click Begin Emotion Detection
![Alt text](https://github.com/fallen576/EmotionController/blob/main/wwwroot/images/elon.png "Image 2")

Elon looks pretty happy, so I would expect the software to pick that up. Click End Emotion Detection and Send to Arduino to see the results:
![Alt text](https://github.com/fallen576/EmotionController/blob/main/wwwroot/images/arduino_board.jpeg "Image 3")

The light will be a variety of colors depening on the top emotion displayed. If you are neutral or the software determines you were displaying an emotion that I did not code for then you will get a little light show.
