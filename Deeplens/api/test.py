import os
import json
import boto3


dynamodb = boto3.resource('dynamodb')
table = dynamodb.Table('labels')

def lambda_handler(event, context):
    response = table.get_item(
        Key={
            'id': "1"
        }
    )
    
    
    #test = json.loads(response)
    test = response['Item']['payload']
    return test