import face_recognition
import os

from cv2 import *

from flask import Flask
app = Flask(__name__)

@app.route('/')
def hello_world():
    # initialize the camera
    cam = VideoCapture(0)   
    s, img = cam.read()
    if s:
        imshow("cam-test",img)
        destroyWindow("cam-test")
        imwrite("./images/unknowface.png",img)

    unknown = face_recognition.load_image_file('./images/unknowface.png')
    
    output = "[ "

    for filename in os.listdir('./images/knownfaces'):
        image2 = face_recognition.load_image_file('./images/knownfaces/' + filename)
        image_encoding2 = face_recognition.face_encodings(image2)[0]
        for unknown_encoding in face_recognition.face_encodings(unknown):
            results = face_recognition.compare_faces([unknown_encoding], image_encoding2)
            if(results[0]):
                output = output + "{ \"id\":" + filename[:-4] + " } ,"

    output = output[: -1]
    output = output + "]"
    return output

    
    face_locations = face_recognition.face_locations(image)
    print(face_locations)
    print(len(face_locations))

if __name__ == '__main__':
    app.run()


