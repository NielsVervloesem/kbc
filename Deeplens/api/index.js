import os
import json
import boto3

client = boto3.client('iot-data', region_name='us-east-1')

iotTopic = '$aws/things/{}/infer'.format(os.environ['AWS_IOT_THING_NAME'])

def lambda_handler(event, context):
    response = client.publish(
        topic='$aws/things/deeplens_CM8cSEEQRGSQhN9xBbCx3w/infer',
        qos=1,
        payload=json.dumps({"yee":"haw"})
    )
    return
