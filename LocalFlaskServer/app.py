from flask import Flask, request
import sys
import random

app = Flask(__name__)

@app.route("/")
def principal():
    puntos = []
    poolSize = request.args.get('size', default=10, type=int)

    # loop para generar carritos internos
    for i in range(poolSize):
        puntos.append({"id": i, "x": random.uniform(-10, 10), "y": 0, "z": random.uniform(-10, 10)});

    return {"cars": puntos}